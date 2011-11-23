using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Common;
using Itop.Client.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmLayerGradeInPut : FormBase
    {
        //protected LayerGrade _obj = null;
        public frmLayerGradeInPut()
        {
            InitializeComponent();           
        }
        public frmLayerGradeInPut(string svgDataUid)
        {
            InitializeComponent();
            InitData(svgDataUid);
        }
        public string TextInPut
        {
            get
            {
                return this.textBox1.Text;
            }
            set
            {
                this.textBox1.Text = value;
            }
        }
        public void InitData(string svgDataUid)
        {
            IList ilist = new ArrayList();
            DataTable dataTable = new DataTable();
            LayerGrade obj = new LayerGrade();
            obj.SvgDataUid = svgDataUid;
            ilist = Services.BaseService.GetList("SelectLayerGradeListBySvgDataUid", obj);
            //ilist = Services.BaseService.GetList<PspType>();
            dataTable = DataConverter.ToDataTable(ilist, typeof(LayerGrade));
            pi.Properties.DataSource = dataTable;
            dataTable.Rows.Add(DataConverter.ObjectToRow("", dataTable.NewRow()));
        }
        public string ParentID
        {
            get
            {    
                return pi.EditValue.ToString();
            }    
            set 
            {
                pi.EditValue = value;                
            }
        }
        public bool textBoxEnabled
        {
            get
            {
                return textBox1.Enabled;
            }
            set
            {
                textBox1.Enabled = value;
            }
        }

        private void simpleButtonOK_Click(object sender, EventArgs e)
        {
            if(textBox1.Text==""){
                MessageBox.Show("分级名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
              int year=Convert.ToInt32(textBox1.Text.Substring(0,4));
            }
            catch{
                MessageBox.Show("分级名称必须包含年份信息。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

    }
}