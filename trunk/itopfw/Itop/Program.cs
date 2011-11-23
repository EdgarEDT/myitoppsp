

using System;
using System.Collections.Generic;
using System.Windows.Forms;

using System.Runtime.Remoting;

using Itop.Client;
using Itop.Client.Login;
using Itop.Common;
using Itop.Client.Option;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Itop {
    static class Program {
        /// <summary>
        /// Itop的入口函数
        /// </summary>
        #region API
        [DllImport("librcode2.dll", EntryPoint = "VerifyRCode")]
        private static extern int VerifyRCode(
            string lpszProductID,
            string lpszRCode
            );
        #endregion/// 

        private static bool Regstate()
        {
            //bool state = true;
            //string AppSysID = Itop.Common.ConfigurationHelper.GetValue("AppSysID");
            //RegistryKey rk = Registry.Users.CreateSubKey(".DEFAULT\\Software\\Itopsoft");
            //if (rk.GetValue(AppSysID) == null)
            //{
            //    state = false;
            //}
            //if (rk.GetValue(AppSysID) != null)
            //{
            //    if (VerifyRCode(AppSysID, rk.GetValue(AppSysID).ToString()) == 0)
            //    {
            //        state = false;
            //    }
            //    else
            //    {
            //        state = true;
            //    }
            //}


            //return state;
            return true;
        }




        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            if (!Regstate())
            {
                Forms.frmReg fg = new Forms.frmReg();
                fg.ShowDialog();
                return;
            }



            // 读取Remoting配置文件
            string fileName = Application.StartupPath + @"\Itop.exe.config";
            if (!System.IO.File.Exists(fileName)) {
                Itop.Common.MsgBox.Show("配置文件不存在，系统无法启动");
                return;
            }

            try {
                RemotingConfiguration.Configure(fileName, false);
            } catch {
                MsgBox.Show("配置文件被破坏，请与软件服务商联系");
                return;
            }

            //try {
               
                Application.ApplicationExit += new EventHandler(Application_ApplicationExit);

                // 如果登录成功，则进入主界面
                UserLoginCommand login = new UserLoginCommand();
                if (login.Execute()) {
                    //LoadSkin();//加载皮肤方案
                    MIS.MainForm = new MainForm();


                    if (!(MIS.MainForm as MainForm).IsClose)
                    {
                        LoadSkin();//加载皮肤方案
                        Application.Run(MIS.MainForm);
                    }
                    
                }
            //} catch (System.Net.Sockets.SocketException) {
            //    MsgBox.Show("无法连接服务器，请稍候重试");
            //    Application.Exit();
            //} catch (Exception ex) {
            //    MsgBox.Show(string.Format("系统出现意外的错误\n\n错误信息：{0}", ex.Message));
            //    Application.Exit();
            //}

        }

        static void Application_ApplicationExit(object sender, EventArgs e) {
            SaveSkin();//保存皮肤方案
        }
        
        private static void SaveSkin() {

            DevExpress.LookAndFeel.UserLookAndFeel skin = DevExpress.LookAndFeel.UserLookAndFeel.Default;
            try {
                Settings.SetValue("Style", skin.Style.ToString());
                Settings.SetValue("SkinName", skin.SkinName);
                Settings.SetValue("UseWindowsXPTheme", skin.UseWindowsXPTheme.ToString());
                Settings.SetValue("UseDefaultLookAndFeel", skin.UseDefaultLookAndFeel.ToString());
            } catch { }

        }
        private static void LoadSkin() {

            DevExpress.LookAndFeel.UserLookAndFeel skin = DevExpress.LookAndFeel.UserLookAndFeel.Default;
            DevExpress.LookAndFeel.LookAndFeelStyle Style;
            skin.SetSkinStyle("");
            string styleName, skinName, xpTheme, useDefault;
            Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            DevExpress.UserSkins.OfficeSkins.Register();
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            //DevExpress.Utils.Localization.AccLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressUtilsLocalizationCHS();
            DevExpress.XtraBars.Localization.BarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraBarsLocalizationCHS();
            //DevExpress.XtraCharts.Localization.ChartLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraChartsLocalizationCHS();
            DevExpress.XtraEditors.Controls.Localizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraEditorsLocalizationCHS();
            DevExpress.XtraGrid.Localization.GridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraGridLocalizationCHS();
            DevExpress.XtraLayout.Localization.LayoutLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraLayoutLocalizationCHS();
            DevExpress.XtraNavBar.NavBarLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraNavBarLocalizationCHS();
            //DevExpress.XtraPivotGrid.Localization.PivotGridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPivotGridLocalizationCHS();
            DevExpress.XtraPrinting.Localization.PreviewLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraPrintingLocalizationCHS();
            DevExpress.XtraReports.Localization.ReportLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraReportsLocalizationCHS();
            //DevExpress.XtraRichTextEdit.Localization.XtraRichTextEditLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraRichTextEditLocalizationCHS();
            //DevExpress.XtraRichEdit.Localization.XtraRichEditLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraRichEditLocalizationCHS();
            DevExpress.XtraScheduler.Localization.SchedulerLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraSchedulerLocalizationCHS();
            DevExpress.XtraScheduler.Localization.SchedulerExtensionsLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraSchedulerExtensionsLocalizationCHS();
            DevExpress.XtraSpellChecker.Localization.SpellCheckerLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraSpellCheckerLocalizationCHS();
            DevExpress.XtraTreeList.Localization.TreeListLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraTreeListLocalizationCHS();
            //DevExpress.XtraVerticalGrid.Localization.VGridLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraVerticalGridLocalizationCHS();
            //DevExpress.XtraWizard.Localization.WizardLocalizer.Active = new DevExpress.LocalizationCHS.DevExpressXtraWizardLocalizationCHS();
            //frmLogin dlg = new frmLogin(); 

            try
            {

                styleName = Settings.GetValue("Style");
                skinName = Settings.GetValue("SkinName");
                xpTheme = Settings.GetValue("UseWindowsXPTheme");
                useDefault = Settings.GetValue("UseDefaultLookAndFeel");

                //styleName = "Skin";
                //skinName = "black";
                //xpTheme = "false";
                //useDefault = "true";
                if (styleName == string.Empty) styleName = skin.Style.ToString();
                if (skinName == string.Empty) skinName = skin.SkinName;
                if (xpTheme == string.Empty) xpTheme = skin.UseWindowsXPTheme.ToString();
                if (useDefault == string.Empty) useDefault = skin.UseDefaultLookAndFeel.ToString();


                Style = (DevExpress.LookAndFeel.LookAndFeelStyle)Enum.Parse(typeof(DevExpress.LookAndFeel.LookAndFeelStyle), styleName, false);

                //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetStyle(Style, bool.Parse(xpTheme), bool.Parse(useDefault), skinName);
                DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);

            }
            catch { }
           
        }
    }
}