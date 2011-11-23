using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.History
{
    public partial class FormFqHistory_add : DevExpress.XtraEditors.XtraForm
    {
        private string typeTitle = string.Empty;
        IList<string> Arealist = null;
        DataTable AreaDT = new DataTable();
        public List<string> Addlist = new List<string>();
        public string TypeTitle
        {
            get { return typeTitle; }
            set { typeTitle = value; }
        }

        public FormFqHistory_add()
        {
            InitializeComponent();
        }

        private void FormFqHistory_add_Load(object sender, EventArgs e)
        {
            textEdit1.Text = TypeTitle;
            AddAreadata();
            checkEdit1.Checked = false;
        }
        //添加分区数据
        private void AddAreadata()
        {
            string ProjectID = Itop.Client.MIS.ProgUID;
            string connstr = " ProjectID='" + ProjectID + "'";
            try
            {
                Arealist =Common.Services.BaseService.GetList<string>("SelectPS_Table_AreaWH_Title", connstr);
            }
            catch (Exception e)
            {
                throw;
            }
            AreaDT.Columns.Add("AreaName",typeof(String));
            AreaDT.Columns.Add("Check",typeof(bool));

            for (int i = 0; i < Arealist.Count; i++)
            {
                DataRow  temprow=AreaDT.NewRow();
                temprow["AreaName"] = Arealist[i].ToString();
                temprow["Check"] = false;
                AreaDT.Rows.Add(temprow);
            }
            gridControl1.DataSource = AreaDT;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (!checkEdit1.Checked)
            {
                if (textEdit1.Text == string.Empty)
                {
                    Itop.Common.MsgBox.Show("请输入名称！");
                    return;
                }

                if (textEdit1.Text == typeTitle)
                {
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    typeTitle = textEdit1.Text;
                    Addlist.Clear();
                    Addlist.Add(textEdit1.Text.Trim());
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                Addlist.Clear();
                foreach (DataRow temprow in AreaDT.Rows)
                {
                    if ((bool)temprow["Check"])
                    {
                        Addlist.Add(temprow["AreaName"].ToString());
                    }
                }
                if (Addlist.Count==0)
                {
                    MessageBox.Show("您未选择分区！");
                    DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
              
            }
            
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        //选择发生改变
        private void checkEdit1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit1.Checked)
            {
                if (Arealist.Count==0)
                {
                    MessageBox.Show("数据库中没有分区名称，请在分区管理中添加！");
                    checkEdit1.Checked = false;
                    return;
                }
                gridControl1.Visible = true;
                checkEdit2.Visible = true;
                checkEdit3.Visible = true;
                System.Drawing.Size tempsize = new Size();
                tempsize.Width = 348;
                tempsize.Height = 350;
                this.Size = tempsize;
                Point temppoint = new Point();
                temppoint.X = 22;
                temppoint.Y = 269;
                checkEdit1.Location = temppoint;
                temppoint.X = 130;
                temppoint.Y = 262;
                simpleButton1.Location = temppoint;
                temppoint.X = 227;
                temppoint.Y = 262;
                simpleButton2.Location = temppoint;

            }
            else
            {
                checkEdit2.Visible = false;
                checkEdit3.Visible = false;
                gridControl1.Visible = false;
                System.Drawing.Size tempsize = new Size();
                tempsize.Width = 348;
                tempsize.Height = 230;
                this.Size = tempsize;
                Point temppoint = new Point();
                temppoint.X = 22;
                temppoint.Y = 144;
                checkEdit1.Location = temppoint;
                temppoint.X = 130;
                temppoint.Y = 137;
                simpleButton1.Location = temppoint;
                temppoint.X = 227;
                temppoint.Y = 137;
                simpleButton2.Location = temppoint;

            }
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit2.Checked)
            {
                checkEdit3.Checked = false;
                for (int i = 0; i < AreaDT.Rows.Count; i++)
                {
                    AreaDT.Rows[i]["Check"] = true;
                }
                gridView1.RefreshData();
            }
        }

        private void checkEdit3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEdit3.Checked)
            {
                checkEdit2.Checked = false;
                for (int i = 0; i < AreaDT.Rows.Count; i++)
                {
                    AreaDT.Rows[i]["Check"] = false;
                }
                gridView1.RefreshData();
            }
        }

    }
}