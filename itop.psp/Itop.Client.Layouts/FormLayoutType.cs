using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Domain.Layouts;
using Itop.Client.Common;
using DevExpress.Utils;
using System.Diagnostics;
using System.IO;
using Itop.Common;
using Itop.Client.Base;
namespace Itop.Client.Layouts
{
    public partial class FormLayoutType : FormBase
    {


        private string type = "";

        public string Type
        {
            set { type = value; }
            get { return type; }
        
        }

        public FormLayoutType()
        {
            InitializeComponent();
        }

        FrmRtfCategory frc = null;
        public FrmRtfCategory FRC
        {
            get { return frc; }
            set { frc = value; }
        }

        FrmLayoutContents flc = null;
        public FrmLayoutContents FLC
        {
            get { return flc; }
            set { flc = value; }
        }

        bool issave = false;



        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void FormLayoutType_Load(object sender, EventArgs e)
        {
            LayoutType lt = Services.BaseService.GetOneByKey<LayoutType>(type);
            WaitDialogForm wait = null;
            try
            {
                wait = new WaitDialogForm("", "正在下载数据, 请稍候...");
                if (lt!=null)
                {

                    dsoFramerWordControl1.FileDataGzip = lt.ExcelData;
                }
                else
                {
                    dsoFramerWordControl1.FileNew();
                }
                wait.Close();
            }
            catch (Exception ex) { wait.Close(); MessageBox.Show(ex.Message); }


            dsoFramerWordControl1.OnFileSaved += new EventHandler(dsoFramerWordControl1_OnFileSaved);
        }

        void dsoFramerWordControl1_OnFileSaved(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            WaitDialogForm wait = null;

            LayoutType lt = Services.BaseService.GetOneByKey<LayoutType>(type);
            try
            {
                wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                if (lt != null)
                {
                    
                    lt.ExcelData = dsoFramerWordControl1.FileDataGzip;
                    Services.BaseService.Update<LayoutType>(lt);
                }
                else
                {
                    lt = new LayoutType();
                    lt.UID = type;
                    lt.ExcelData = dsoFramerWordControl1.FileDataGzip;
                    Services.BaseService.Create<LayoutType>(lt);
                }
                issave = true;

                if (frc != null)
                    frc.InitModule(type);

                if (flc != null)
                    flc.InitModule(type);
                wait.Close();
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                wait.Close();
                return;


            }
        
        
        
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //SaveData();
            this.Close();
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                dsoFramerWordControl1.DoPageSetupDialog();
            }
            catch { }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //SaveData();
        }

        private void FormLayoutType_FormClosing(object sender, FormClosingEventArgs e)
        {
            //SaveData();


        }
    }
}