using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using DevExpress.Utils;

namespace Itop.Client.Forecast
{
    public partial class frmMainDelete : FormBase
    {
        
        public frmMainDelete()
        {
            InitializeComponent();
        }
        bool Flag = false;
        DataTable dt = new DataTable();
        //系统中不能删除的表名
        string[] ObjectTable = new string[] { "Smugroup", "Smmuser", "Smmproject", "Smmprog", "Smmlog", "Smmgroup", "smdgroup", "SAppProps", "glebeType", "LineType", "PS_Table_Area_TYPE", "Ps_HistoryType", "WireCategory" };
        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void frmMainDelete_Load(object sender, EventArgs e)
        {
           
            IList<string> templist = Common.Services.BaseService.GetList<string>("SelectSystemDeleteByconn", "");
            dt.Columns.Add("NUM", typeof(string));
            dt.Columns.Add("tablename", typeof(string));
            dt.Columns.Add("colnum",typeof(string));
            dt.Columns.Add("select", typeof(bool));
            dt.Columns.Add("remark", typeof(string));
            for (int i = 0; i < templist.Count; i++)
            {
                DataRow temprow = dt.NewRow();
                temprow["NUM"] = i.ToString();
                temprow["tablename"] = templist[i].ToString();
                if (IN_StringArry(ObjectTable,templist[i]))
                {
                    temprow["remark"] = "系统表，不能删除！";
                }
                temprow["select"] = false;
                temprow["colnum"] = Common.Services.BaseService.GetObject("SelectSystemDeleteCountbycon", templist[i].ToString()).ToString();
                dt.Rows.Add(temprow);
               
            }
            dataGridView1.DataSource = dt;
            Flag = true;
        }
        //判断数组中是否存在特定值，存在返回true，否测返回false
        private bool IN_StringArry(string[] tempArry, string tempValue)
        {
            bool value=false;
            for (int i = 0; i < tempArry.Length; i++)
            {
                if (tempValue==tempArry[i])
                {
                    value = true;
                    break;
                }
            }
            return value;
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            Flag = false;

            if (checkBox1.Checked == true)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["remark"].ToString() != ""||dt.Rows[i]["colnum"].ToString()=="0")
                    {
                        dt.Rows[i]["select"] = false;
                    }
                    else
                    {
                        dt.Rows[i]["select"] = true;
                    }
                }
                checkBox2.Checked = false;
            }
            Flag = true;
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            Flag = false;
            if (checkBox2.Checked == true)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["remark"].ToString() != "")
                    {
                        dt.Rows[i]["select"] = false;
                    }
                    else
                    {
                        dt.Rows[i]["select"] = false;
                    }
                }
                checkBox1.Checked = false;
            }
            Flag = true;
        }

        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!Flag)
            {
                return;
            }
            if (dataGridView1["remark", e.RowIndex].Value.ToString() != "" || dataGridView1["colnum", e.RowIndex].Value.ToString()=="0")
            {
                e.Cancel = true;
            }
            
        }
        private int SelectRowNum()
        {
            int m = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((bool)dt.Rows[i]["select"])
                {
                    m++;
                }
            }
            return m;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("删除数不可恢复！\n请您谨慎操作！！！","警告！",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                int allnum = SelectRowNum();
                if (allnum==0)
                {
                    MessageBox.Show("您没有选择任何表！！");
                    return;
                } 
                int m = 0;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                   
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                    if ((bool)dt.Rows[i]["select"])
                    {
                        m++;
                        try
                        {
                            dataGridView1.Rows[i].Selected = true;
                            WaitDialogForm wait = new WaitDialogForm("", "删除(" + m * 100 / allnum + "%)");
                            Common.Services.BaseService.Update("DeleteSystemDeleteByconn", dt.Rows[i]["tablename"]);
                            dataGridView1.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.Green;
                            dataGridView1.Rows[i].Selected = false;
                            dt.Rows[i]["colnum"] = (int)Common.Services.BaseService.GetObject("SelectSystemDeleteCountbycon", dt.Rows[i]["tablename"]);
                            dataGridView1.Refresh();
                            wait.Close();
                        }
                        catch (Exception ee)
                        {
                            MessageBox.Show(dt.Rows[i]["tablename"].ToString() + "    表出现问题，不能删除数据！");
                            throw;
                        }
                       
                    }
                }
                MessageBox.Show("已完成删除！");
            }
        }


       
    }
}