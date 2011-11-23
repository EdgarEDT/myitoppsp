using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.IO;
using Itop.Server;
using Itop.Server.Interface;
namespace Itop.Client.Table
{
    public partial class FrmTableReportEdit : Itop.Client.Base.FormBase
    {
        public FrmTableReportEdit()
        {
            InitializeComponent();
        }
        public Ps_Table_Report ptr = null;
        private void FrmTableReportEdit_Load(object sender, EventArgs e)
        {
            DataBind();
        }
        private string oldname = "";
        private void DataBind()
        {
            if (ptr!=null)
            {
                txtTableID.DataBindings.Add("EditValue",ptr,"TableID");
                spinNum1.DataBindings.Add("EditValue",ptr,"Num1");
                txtoldtablename.DataBindings.Add("EditValue",ptr,"TableOldName");
                txtNewtableanme.DataBindings.Add("EditValue",ptr,"TableNewName");
                txtyears.DataBindings.Add("EditValue",ptr,"Years");
                meremark.DataBindings.Add("EditValue",ptr,"Remark");
                meremark2.DataBindings.Add("EditValue",ptr,"Remark2");
                picimage.DataBindings.Add("EditValue", ptr, "image1");
                if (ptr.TableNewName.Length!=0)
                {
                    oldname = ptr.TableNewName;
                }
            }
        }
        //确定
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (txtTableID.Text.Length==0)
	        {
                MessageBox.Show("表格标识不能为空");
                return;
	        }
            else
	        {
                string con=" TableID='"+txtTableID.Text+"' and ProjectID='" + MIS.ProgUID + "'";
                IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
                if (templist.Count > 0 && templist[0].TableID != txtTableID.Text)
                {
                    MessageBox.Show("[" + txtTableID.Text + "] 表标识已经存在，不能重复！");
                    return;
                }
	        }
            if (txtNewtableanme.Text.Length==0)
            {
                MessageBox.Show("新表名不能为空");
                return;
            }
            else if (txtNewtableanme.Text!=oldname)
            {
                ptr.TableOldName = oldname;
            }
            if (txtyears.Text.Length!=0)
            {
                bool error = false;
                string[] ary = txtyears.Text.Split('#');
                for (int i = 0; i < ary.Length; i++)
                {
                    int m=0;
                    if (!int.TryParse(ary[i], out m))
	                {
                        error = true;
                        break;
	                }
                   
                }
                if (error)
                {
                    MessageBox.Show("年份数据格式错误，请仔细检查！");
                    return;
                }
            }
            if (picimage.Image==null)
            {
                ptr.image1 = new byte[0];
            }
            
            ptr.image2 = new byte[0];
            DialogResult = DialogResult.OK;
            this.Close();
        }
        //取消
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //编辑图片
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            string filepath = "";
            openFileDialog1.Filter = "图片文件|*.jpeg;*.jpg;*.bmp;*.gif;*.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                filepath = openFileDialog1.FileName;
                FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                Byte[] btye2 = new byte[fs.Length];
                fs.Read(btye2, 0, Convert.ToInt32(fs.Length));
                picimage.Image = Image.FromStream(fs);
                ptr.image1 = btye2;
            }
        }

       
    }
}