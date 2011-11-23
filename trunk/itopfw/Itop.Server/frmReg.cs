using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Itop.Server
{
    public partial class frmReg : Form
    {
        
        [DllImport("librcode2.dll", EntryPoint = "GetHDSNo")]
        private static extern int GetHDSNo(
                                            string lpszProductID,
                                             StringBuilder lpszRetVal
                                            );
        [DllImport("librcode2.dll", EntryPoint = "VerifyRCode")]
        private static extern int VerifyRCode(
                                            string lpszProductID,
                                            string lpszRCode
                                            );


        string AppSysID = "Server";
        public frmReg()
        {
            InitializeComponent();
            //AppSysID = Itop.Common.ConfigurationHelper.GetValue("AppSysID");
            StringBuilder a = new StringBuilder(50);//"                                                             ";
            GetHDSNo(AppSysID, a);
            textBox1.Text = a.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
			Application.Exit();
        }

        private void frmReg_Load(object sender, EventArgs e)
        {
            //string app = "sbxj2007";
            //StringBuilder str = new StringBuilder(50);
            //GetHDSNo(app, str);
            //textBox1.Text = str.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string code = textBox2.Text;
            //if (code.Length < 4)
            //{
            //    msg.Text = "×¢²áÂë´íÎó£¡";
            //    return;
            //}
            //string strkey = code.Substring(0, 2) + code.Substring(code.Length - 2, 2);
            //if(strkey!="sbxj"){
            //    msg.Text = "×¢²áÂë´íÎó£¡";
            //    return;
            //}
            //code = code.Substring(2);
            //code = code.Substring(0,code.Length - 2);
            //int i = VerifyRCode("sbxj2007", code);
            //if(i==0)
            //{
            //    msg.Text = "×¢²áÂë´íÎó£¡";
            //}
            //if(i==1){
            //    RegistryKey key=Registry.Users.CreateSubKey(".DEFAULT\\Software\\Itopsoft\\sbxj");
            //    key.SetValue("regkey", code);
            //    this.Close();
            //}


            int i = VerifyRCode(AppSysID, textBox2.Text);
            if (i == 0)
            {
                msg.Text = "×¢²áÂë´íÎó£¬ÇëÖØÐÂÊäÈë£¡";
            }
            if (i == 1)
            {
                RegistryKey key = Registry.Users.CreateSubKey(".DEFAULT\\Software\\Itopsoft");
                key.SetValue(AppSysID, textBox2.Text);
                this.Close();
            }
        }

        private void frmReg_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}