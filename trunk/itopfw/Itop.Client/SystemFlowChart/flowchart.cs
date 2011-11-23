using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.SystemFlowChart
{
    public partial class flowchart : UserControl
    {
        public flowchart()
        {
            InitializeComponent();
        }
        public void SetUrl(string url)
        {
            webBrowser1.Navigate(Application.StartupPath + "\\flowchart\\" + url);
            this.webBrowser1.Refresh();
        }

        

        private void webBrowser1_NewWindow(object sender, CancelEventArgs e)
        {
            //string tempusee = ((WebBrowser)sender).StatusText;
            //tempusee = tempusee.Substring(tempusee.LastIndexOf('/') + 1, tempusee.Length - tempusee.LastIndexOf('/') - 1);
            //MessageBox.Show(tempusee);

            ////if (tempusee == "www.abc.com")
            ////{
            ////    MessageBox.Show("abc");

            ////}
            ////if (tempusee == "www.123.com")
            ////{
            ////    MessageBox.Show("123");

            ////}
            //e.Cancel = true;
        }

        private void webBrowser1_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (!e.Url.ToString().Contains("htm"))
            {
                e.Cancel = true;
                string tempusee = e.Url.ToString();
                tempusee = tempusee.Substring(tempusee.LastIndexOf('/') + 1, tempusee.Length - tempusee.LastIndexOf('/') - 1);
                MessageBox.Show(tempusee);
            }
            
        }
       
    }
}
