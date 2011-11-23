using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Diagnostics;


namespace ItopVectorDraw
{
	/// <summary>
	/// MainApp 的摘要说明。
	/// </summary>
	public class MainApp
	{
		[DllImport("librcode2.dll",EntryPoint="VerifyRCode")] 
		private static extern int VerifyRCode( 
			string lpszProductID, 
			string  lpszRCode
			); 
		
		
		
		public MainApp()
		{
		}
		[STAThread]
		private static void Main()
		{ 
//			try
			{
				//string str =ConfigurationSettings.AppSettings[0];
//				tlreg.regClass r=new regClass();
//				string aa=r.Encode("10000");
                //Process.Start("http://wpa.qq.com/msgrd?V=1&Uin=67180078&Site=给大白兔留言&Menu=yes");
				MainFrame nsvg1 = new MainFrame(); 
//				FrmMapTest frm =new FrmMapTest();
//				frmTest test =new frmTest();
				Application.Run(nsvg1);
			}
//			catch (Exception exception1)
//			{
//			}
		}

	}
}
