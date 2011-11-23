using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ItopVector.Core.Document;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmPrintF : FormBase
    {
        public string strzt;
        public string strgs;
        public PrintInfo pri;
        bool IsCreate = true;

        public frmPrintF()
        {
            InitializeComponent();
            pri = new PrintInfo();
        }

        public void Init(string uid,string svguid)
        {
            pri.UID = uid;
            pri.SvgUID = svguid;
            pri =(PrintInfo) Services.BaseService.GetObject("SelectPrintInfoByKey",pri);
            if (pri != null)
            {
                IsCreate = false;
                
            }
            else
            {
                IsCreate = true;
                pri = new PrintInfo();
                pri.UID = uid;
                pri.SvgUID = svguid;
            }
            textEdit1.DataBindings.Add("Text", pri, "Col1");
            textEdit2.DataBindings.Add("Text", pri, "Col2");
            textEdit3.DataBindings.Add("Text", pri, "Col3");
            textEdit4.DataBindings.Add("Text", pri, "Col4");
            textEdit5.DataBindings.Add("Text", pri, "Col5");
            textEdit6.DataBindings.Add("Text", pri, "Col6");
            textEdit7.DataBindings.Add("Text", pri, "Col7");
            textEdit8.DataBindings.Add("Text", pri, "Col8");
            textEdit9.DataBindings.Add("Text", pri, "Col9");
            textEdit10.DataBindings.Add("Text", pri, "Col10");
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (IsCreate)
            {
                Services.BaseService.Create<PrintInfo>(pri);
            }
            else
            {
                Services.BaseService.Update<PrintInfo>(pri);
            }
            strzt = zt.Text;    
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}