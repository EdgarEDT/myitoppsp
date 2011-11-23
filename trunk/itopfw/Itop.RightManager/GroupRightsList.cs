using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;

namespace Itop.RightManager {
    public partial class GroupRightsList : UserControl {


        DataTable dt;

        public GroupRightsList() {
            InitializeComponent();
            dataGridView1.DataSourceChanged += new EventHandler(dataGridView1_DataSourceChanged);
        }

        void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            dt = dataGridView1.DataSource as DataTable;
            //DataTable table = dataGridView1.DataSource as DataTable;
            dataGridView1.Columns["index"].Visible = false;
            dataGridView1.Columns["parentid"].Visible = false;
            dataGridView1.Columns["Groupno"].Visible = false;
            dataGridView1.Columns["Progid"].Visible = false;
            dataGridView1.Columns["pro"].Visible = false;
            dataGridView1.Columns["filterstring"].Visible = false;
            dataGridView1.Columns["hiddencols"].Visible = false;
            dataGridView1.Columns["spec1"].Visible = false;
            dataGridView1.Columns["spec2"].Visible = false;
            dataGridView1.Columns["spec3"].Visible = false;
            dataGridView1.Columns["ProgModuleType"].Visible = false;
            try
            {
                dataGridView1.Columns["ProjectUID"].Visible = false;
            }
            catch { }
            

        }
        IBaseService recordService;
        //服务
        public IBaseService RecordService {
            get {
                if (recordService == null) {
                    recordService = Itop.Common.RemotingHelper.GetRemotingService<IBaseService>();
                }
                return recordService;
            }
        }


        private void SelectItem(string name, string value)
        {
            foreach (DataRowView drv in dt.DefaultView)
            {
                drv.Row[name] = value;
            }

            ////foreach (DataRow row in dt.Rows)
            ////{
            ////    row[name] = value;
            ////}


        }

        private void SelectItem(string value)
        {
            foreach (DataRowView drw in dt.DefaultView)
            {
                drw.Row["run"] = value;
                drw.Row["ins"] = value;
                drw.Row["upd"] = value;
                drw.Row["del"] = value;
                drw.Row["qry"] = value;
                drw.Row["prn"] = value;
                drw.Row["send"] = value;
                drw.Row["exam"] = value;
                drw.Row["pass"] = value;
            }

            //////foreach (DataRow row in dt.Rows)
            //////{
            //////    row["run"] = value;
            //////    row["ins"] = value;
            //////    row["upd"] = value;
            //////    row["del"] = value;
            //////    row["qry"] = value;
            //////    row["prn"] = value;
            //////}
        }

        private void TSMrun_Click(object sender, EventArgs e)
        {
            SelectItem("run", "1");
        }

        private void TSMadd_Click(object sender, EventArgs e)
        {
            SelectItem("ins", "1");
        }

        private void TSMupd_Click(object sender, EventArgs e)
        {
            SelectItem("upd", "1");
        }

        private void TSMdel_Click(object sender, EventArgs e)
        {
            SelectItem("del", "1");
        }

        private void TSMqry_Click(object sender, EventArgs e)
        {
            SelectItem("qry", "1");
        }

        private void TSMprn_Click(object sender, EventArgs e)
        {
            SelectItem("prn", "1");
        }

        private void TSMall_Click(object sender, EventArgs e)
        {
            SelectItem("1");
        }

        private void TSMnrun_Click(object sender, EventArgs e)
        {
            SelectItem("run", "0");
        }

        private void TSMnadd_Click(object sender, EventArgs e)
        {
            SelectItem("ins", "0");
        }

        private void TSMnupd_Click(object sender, EventArgs e)
        {
            SelectItem("upd", "0");
        }

        private void TSMndel_Click(object sender, EventArgs e)
        {
            SelectItem("del", "0");
        }

        private void TSMnqry_Click(object sender, EventArgs e)
        {
            SelectItem("qry", "0");
        }

        private void TSMnprn_Click(object sender, EventArgs e)
        {
            SelectItem("prn", "0");
        }

        private void TSMnall_Click(object sender, EventArgs e)
        {
            SelectItem("0");
        }

        private void 发送ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectItem("send", "1");
        }

        private void 审查ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectItem("exam", "1");
        }

        private void 审批ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectItem("pass", "1");
        }

        private void 发送ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectItem("send", "0");
        }

        private void 审查ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectItem("exam", "0");
        }

        private void 审批ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectItem("pass", "0");
        }


    }
}
