using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.Client.Chen
{
    public partial class FrmBalance_Calc : FormBase
    {

        string dy = "";
        /// <summary>
        /// 获取电压对象
        /// </summary>
        public string DY
        {
            get { return dy; }
            set { dy = value; }
        }

        string type = "";
        /// <summary>
        /// 获取type对象
        /// </summary>
        public string TYPE
        {
            get { return type; }
            set { type = value; }
        }

        string flag = "";
        /// <summary>
        /// 获取flag对象
        /// </summary>
        public string FLAG
        {
            get { return flag; }
            set { flag = value; }
        }
        string formtitle = "";
        /// <summary>
        /// 获取type对象
        /// </summary>
        public string FormTitle
        {
            get { return formtitle; }
            set { formtitle = value; }
        }

        string ctrtitle = "";
        /// <summary>
        /// 获取flag对象
        /// </summary>
        public string CtrTitle
        {
            get { return ctrtitle; }
            set { ctrtitle = value; }
        }

        double sum = 0.0;
        /// <summary>
        /// 获取sum对象
        /// </summary>
        public double SUM
        {
            get { return sum; }
            set { sum = value; }
        }
        private bool AddRight = false;
        private bool EditRight = false;
        private bool DeleteRight = false;
        private bool PrintRight = false;
        public bool ADdRight
        {
            get { return AddRight; }
            set { AddRight = value; }
        }
        public bool EDitRight
        {
            get { return EditRight; }
            set { EditRight = value; }
        }
        public bool DEleteRight
        {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }
        public bool PRintRight
        {
            get { return PrintRight; }
            set { PrintRight = value; }
        }
        public FrmBalance_Calc()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance_Calc1.AddObject();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance_Calc1.UpdateObject();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance_Calc1.DeleteObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance_Calc1.PrintPreview();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
            this.Close();
        }
        private void Hidbar()
        {
            if (!AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            }
            if (!EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.ctrlPSP_VolumeBalance_Calc1.AllowUpdate = false;
            }
            if (!DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            }
            if (!PrintRight)
            {
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            }

        }
        private void FrmBalance_Calc_Load(object sender, EventArgs e)
        {
            this.Text = formtitle;
            this.ctrlPSP_VolumeBalance_Calc1.GridView.GroupPanelText = ctrtitle;
            this.ctrlPSP_VolumeBalance_Calc1.TYPE = type;
            this.ctrlPSP_VolumeBalance_Calc1.FLAG = flag;
            this.ctrlPSP_VolumeBalance_Calc1.DY = dy;
            if (type == "1")
            {
                this.ctrlPSP_VolumeBalance_Calc1.GridView.Columns["LX1"].Visible = false;
                this.ctrlPSP_VolumeBalance_Calc1.GridView.Columns["LX2"].Visible = false;
                this.ctrlPSP_VolumeBalance_Calc1.GridView.Columns["Col2"].Visible = true;
                this.ctrlPSP_VolumeBalance_Calc1.GridView.Columns["Col2"].VisibleIndex = 3;
                this.ctrlPSP_VolumeBalance_Calc1.GridView.Columns["CreateTime"].VisibleIndex = 4;
            }
            this.ctrlPSP_VolumeBalance_Calc1.RefreshData();
            this.ctrlPSP_VolumeBalance_Calc1.FormTitle = formtitle;
            this.ctrlPSP_VolumeBalance_Calc1.CtrTitle = ctrtitle;
            Hidbar();
        }

        private void FrmBalance_Calc_FormClosing(object sender, FormClosingEventArgs e)
        {
            sum = ctrlPSP_VolumeBalance_Calc1.SUM;
        }
    }
}