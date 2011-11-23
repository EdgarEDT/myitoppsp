using System;
using System.Collections.Generic;
using System.Text;
using TONLI.Report;
using System.Data;
using Itop.Domain.BaseDatas;
using Itop.Client.Common;
using DevExpress.Utils;

namespace Itop.Client.Report
{
    public class OpenReport
    {
        FrmReport fr = new FrmReport();

        private string reportName = "";
        public string ReportName
        {
            set { reportName = value; }
            get { return reportName; }
        }


        private string template = "";
        public string Template
        {
            set { template = value; }
            get { return template; }
        }


        private object datasource = null;
        public object DataSource
        {
            set { datasource = value; }
            get { return datasource; }
        }

        public OpenReport()
        { }

        public void Show()
        {
            if (reportName == "")
                return;

            ReportFormat rss = new ReportFormat();
                
            if(template!="")
                    rss = Services.BaseService.GetOneByKey<ReportFormat>(template);

            WaitDialogForm wait = new WaitDialogForm("", "正在生成报表...");

            try
            {
                fr.DataSource = datasource;
                ReportFormat rf = Services.BaseService.GetOneByKey<ReportFormat>(reportName);
                byte[] bt = null;
                try { 
                    bt = rf.ByteReport; 
                
                    }
                catch { }

                    if (bt == null && template != "")
                        bt = rss.ByteReport;
                fr.ReportByte = bt;

                wait.Close();
                fr.tx1.TextChanged += new EventHandler(tx1_TextChanged);
                fr.ShowDialog();
            }
            catch { wait.Close(); }
        
        }


        private void tx1_TextChanged(object sender, EventArgs e)
        {
            ReportFormat rf = Services.BaseService.GetOneByKey<ReportFormat>(reportName);
            if (rf != null)
            {
                try
                {
                    rf.ByteReport = fr.ReportByte;
                    Services.BaseService.Update<ReportFormat>(rf);
                }
                catch { }
            }
            else
            {
                try
                {
                    rf = new ReportFormat();
                    rf.Title = reportName;
                    rf.ByteReport = fr.ReportByte;
                    Services.BaseService.Create<ReportFormat>(rf);
                }
                catch { }
            }
        }




    }
}
