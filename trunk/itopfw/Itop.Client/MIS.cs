
using System;
using System.Collections.Generic;
using System.Text;

using System.Data;
using System.Data.Common;
using System.Windows.Forms;

using Itop.Common;
using Itop.Domain;
using Itop.Server.Interface;
using Itop.Server.Interface.AppProp;
using Itop.Server.Interface.Login;
using Itop.Server.Interface.SysLog;
using Itop.Server.Interface.Rights;

using Microsoft.Practices.EnterpriseLibrary.Logging;
using System.Collections;
using Itop.Domain.RightManager;
using System.Runtime.Remoting;

namespace Itop.Client {
    /// <summary>
    /// 主模块
    /// </summary>
    static public class MIS {
        static MIS() {
            // 注册服务
            Itop.Client.Service.ServiceBus.RegisterService<Itop.Client.Service.IMis>(new MisService());
        }
        private static string username = "";
        
        static private Dictionary<string, string> m_props = new Dictionary<string, string>();

        static private string GetUserName() {
            if (UserNumber == string.Empty)
                return string.Empty;

            string result = string.Empty;
            ILoginAction loginAction = RemotingHelper.GetRemotingService<ILoginAction>();
            if (loginAction != null)
                result = loginAction.GetUserName(UserInfo, UserNumber);

            return result;
        }
        /// <summary>
        /// 是否有新版本
        /// </summary>
        /// <param name="propName"></param>
        /// <returns></returns>
        static public bool HasNewVersion() {
            string serverProtocol, serverAddress, serverPort;
            string file = Application.StartupPath+"\\Itop.update.exe";
            serverProtocol = ConfigurationHelper.GetValue(file, "serverProtocol", "");
            serverAddress = ConfigurationHelper.GetValue(file, "serverAddress", "");
            serverPort = ConfigurationHelper.GetValue(file, "serverPort", "");

            string serverUrl = string.Format("{0}://{1}:{2}/", serverProtocol, serverAddress, serverPort);
            IAppPropAction appPropAction = null;
            
                foreach (WellKnownClientTypeEntry entry in RemotingConfiguration.GetRegisteredWellKnownClientTypes())
                    if (entry.ObjectType == typeof(IAppPropAction))
                        appPropAction = (IAppPropAction)Activator.GetObject(typeof(IAppPropAction), serverUrl + entry.ObjectUrl);
          
            if (appPropAction == null) return false;


            try {
                //获取数据库版本日期
                string ver1 = appPropAction.GetAppProperty("AppLastDate");
                //获取程序版本
                string ver2 = ConfigurationHelper.GetValue("UpdateDate");

                string sys1 = appPropAction.GetAppProperty("AppSysID");

                string sys2 = ConfigurationHelper.GetValue("AppSysID");
                if (string.IsNullOrEmpty(sys2)) {
                    ConfigurationHelper.SetValue("AppSysID", sys1);
                    sys2 = sys1;
                }
                
                return ver1.CompareTo(ver2) > 0&& !string.IsNullOrEmpty(sys1)&& sys1.CompareTo(sys2)==0 ? true : false;
            } catch { return false; }           

        }
        /// <summary>
        /// 获得系统参数
        /// </summary>
        /// <param name="propName">参数名称</param>
        /// <returns>参数值</returns>
        static public string Property(string propName) {
            if (m_props.ContainsKey(propName)) {
                return m_props[propName];
            } else {
                IAppPropAction appPropAction = RemotingHelper.GetRemotingService<IAppPropAction>();
                string result = appPropAction.GetAppProperty(propName);
                m_props.Add(propName, result);
                return result;
            }
        }

        private const string ApplicationCaptionPropName = "AppCaption";

        /// <summary>
        /// 应用程序的标题
        /// </summary>
        static public string ApplicationCaption {
            get { return Property(ApplicationCaptionPropName); }
        }

        private static string m_token = string.Empty;

        /// <summary>
        /// 登录令牌
        /// </summary>
        public static string Token {
            get { return m_token; }
            set { m_token = value; }
        }

        private static string m_userNumber = string.Empty;

        /// <summary>
        /// 工号
        /// </summary>
        public static string UserNumber {
            get { return m_userNumber; }
            set {
                m_userNumber = value;
                if (MainFormInterface != null) {
                    MainFormInterface.SetStatusLabel();
                }
            }
        }
        public static string ProgUID;
        public static string ProgName;
        public static string ProgUserID;

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName {
            get {
                if (username == "")
                {
                    username = GetUserName();
                }
                return username;
            }
        }
        public static string UserLastLogon
        {
            get
            {
                return SmmprogService.GetOneByKey<Smmuser>(UserNumber).Lastlogon;
            }
        }
        /// <summary>
        /// 已经打开的模块的主窗口
        /// </summary>
        private static Dictionary<string, Form> m_formsOpened = new Dictionary<string, Form>();

        /// <summary>
        /// 判断窗体是否已经被打开
        /// </summary>
        /// <param name="actionId">模块的Id，来源于表MainMenu.ActionId</param>
        /// <returns>true：已经打开，false：没有打开</returns>
        static public bool FormIsOpened(string actionId) {
            return m_formsOpened.ContainsKey(actionId);
        }

        /// <summary>
        /// 根据actionId获得其窗体
        /// </summary>
        /// <param name="actionId">模块的Id，来源于表MainMenu.ActionId</param>
        /// <returns>如果窗体没有打开过，则返回null，否则返回该窗体</returns>
        static public Form GetOpenedForm(string actionId) {
            return (FormIsOpened(actionId) ? m_formsOpened[actionId] : null);
        }

        /// <summary>
        /// 通知AppServer删除列表中的已经打开的窗体
        /// </summary>
        /// <param name="actionId">模块的Id，来源于表MainMenu.ActionId</param>
        static public void RemoveOpenedForm(string actionId) {
            if (!FormIsOpened(actionId)) {
                return;
            }

            m_formsOpened.Remove(actionId);
        }

        static public UserInfo UserInfo {
            get { return new UserInfo(Token, UserNumber); }
        }
        



        static private Form m_mainForm = null;

        /// <summary>
        /// 主界面
        /// </summary>
        static public Form MainForm {
            get { return m_mainForm; }
            set { m_mainForm = value; }
        }

        private static IMainForm m_mainFormInterface;

        /// <summary>
        /// 主窗体接口
        /// </summary>
        public static IMainForm MainFormInterface {
            get { return m_mainFormInterface; }
            set { m_mainFormInterface = value; }
        }



        private static IFrmConsole m_IFrmConsole;

        /// <summary>
        /// 主窗体接口
        /// </summary>
        public static IFrmConsole MFrmConsole
        {
            get { return m_IFrmConsole; }
            set { m_IFrmConsole = value; }
        }





        public static T OpenMDIChildForm<T>(string id) where T : Form, new() {
            return OpenMDIChildForm<T>(id, string.Empty);


        }

        public static T OpenMDIChildForm<T>(string id, string formCaption) where T : Form, new() {
            T form;
            if (m_formsOpened.ContainsKey(id)) {
                form = (T)m_formsOpened[id];
            } else {
                form = new T();
                if (formCaption != string.Empty) {
                    form.Text = formCaption;
                }
                m_formsOpened.Add(id, form);
            }

            form.MdiParent = MIS.MainForm;
            form.WindowState = FormWindowState.Maximized;
            form.BringToFront();
            form.Show();

            return form;
        }

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="moduleName">模块名称</param>
        /// <param name="info">日志内容</param>
        public static void WriteApplicationLog(string moduleName, string info) {
            ISysLogAction sl = RemotingHelper.GetRemotingService<ISysLogAction>();
            sl.WriteLog(UserInfo, moduleName, info);
        }

        /// <summary>
        /// 当发生Exception的时候，记录log
        /// </summary>
        /// <param name="info"></param>
        public static void WriteExceptionLog(string info) {
            LogEntry log = new LogEntry();
            log.Message = info;
            Logger.Write(log);
        }


        /// <summary>
        /// 判断是否具有某个权限
        /// </summary>
        /// <param name="rightId">权限Id</param>
        /// <returns></returns>
        public static bool HasRight(int rightId) {
            IRightsAction ra = RemotingHelper.GetRemotingService<IRightsAction>();
            return ra.HasRight(UserInfo, rightId);
        }

        /// <summary>
        /// 判断是否具有某个权限
        /// </summary>
        /// <param name="actionId">Action Id</param>
        /// <returns></returns>
        public static bool HasRight(string actionId) {
            IRightsAction ra = RemotingHelper.GetRemotingService<IRightsAction>();
            return ra.HasRight(UserInfo, actionId);
        }



        /// <summary>
        /// 返回权限对象
        /// </summary>
        /// <param name="actionId">Action Id</param>
        /// <returns></returns>
        /// 
        private static IBaseService smmprogService;

        public static IBaseService SmmprogService
        {
            get
            {
                if (smmprogService == null)
                {
                    smmprogService = RemotingHelper.GetRemotingService<IBaseService>();
                }
                if (smmprogService == null) MsgBox.Show("IBaseService服务没有注册");
                return smmprogService;
            }
        }


        //获取卷的操作权限
        public static VsmdgroupProg GetProgRight(string progid, string projectUID)
        {
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            
            smdgroup.Progid = progid;
            IList<Smugroup> listUsergroup = smmprogService.GetList<Smugroup>("SelectSmugroupByWhere", "Userid='" + MIS.UserNumber + "'");
            if (listUsergroup.Count > 0)
            {
                smdgroup.Groupno = listUsergroup[0].Groupno;
            }
            //如果是系统管员
            if (smdgroup.Groupno == "SystemManage" && MIS.UserNumber.ToLower() == "admin")
            {
                smdgroup.run = "1";
                smdgroup.ins = "1";
                smdgroup.upd = "1";
                smdgroup.del = "1";
                smdgroup.qry = "1";
                smdgroup.pro = "1";
                smdgroup.prn = "1";
                return smdgroup;
            }
            smdgroup.ProjectUID = projectUID;
            VsmdgroupProg sm = new VsmdgroupProg();
            IList list = SmmprogService.GetList("SelectSmdgroupList", smdgroup);
            if (list.Count > 0)
            {
                sm = (VsmdgroupProg)list[0];
            }
            return sm;
        }

        public static VsmdgroupProg GetProgRight(string progid, string projectUID,string userNumber)
        {
            VsmdgroupProg smdgroup = new VsmdgroupProg();
            smdgroup.Progid = progid;
            smdgroup.Groupno = userNumber;
            smdgroup.ProjectUID = projectUID;
            VsmdgroupProg sm = new VsmdgroupProg();
            IList list = SmmprogService.GetList("SelectSmdgroupList", smdgroup);
            if (list.Count > 0)
            {
                sm = (VsmdgroupProg)list[0];
            }
            return sm;
        }

        public static void SaveLog(string moduleName,string czName)
        {
            SMMLOG smmlog = new SMMLOG();
            smmlog.USERID = MIS.UserName;
            smmlog.RQ = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss");
            smmlog.CZPROG = moduleName;
            smmlog.CZNOTES = czName;
            smmlog.CZCOMPUTE = System.Net.Dns.GetHostName();
            smmlog.CZIP = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName())[0].ToString();
            SmmprogService.Create<SMMLOG>(smmlog);         
        }
        public static ImageList GetImageList(int size) {
            return Itop.Client.Resources.ImageListRes.GetimageListAll(size);
        }
        //去除系统自用图标和ico图标的所有图标
        public static ImageList GetImageList(int size ,string str)
        {
            return Itop.Client.Resources.ImageListRes.GetimageListAll(size,str);
        }
    }
}
