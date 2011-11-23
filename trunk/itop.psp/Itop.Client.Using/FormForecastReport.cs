using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Common;
using Itop.Client.Base;
using System.Text.RegularExpressions;
using System.Text;
using System.Runtime.InteropServices;
using System.Data;
using System.Configuration;
using System.Collections.Specialized;
using System.Xml;
using Itop.Domain.Forecast;


namespace Itop.Client.Using
{
    /// <summary>
    /// Summary description for FormForecastReport.
    /// </summary>
    public class FormForecastReport : DevExpress.XtraEditors.XtraForm
    {
        private Label label1;
        private SpinEdit spinEdit1;
        private Label label2;
        private Label label3;
        private SpinEdit spinEdit2;
        private Label label4;
        private SpinEdit spinEdit3;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;

        private bool _isEdit;
        private int _typeFlag;
        private bool _noHistoryYears;
        private string _typeText;
        private string projectUID="";

        public string TypeText
        {
            get { return _typeText; }
            set { _typeText = value; }
        }

        public string ProjectUID
        {
            get { return projectUID; }
            set { projectUID = value; }
        }

        public bool NoHistoryYears
        {
            get { return _noHistoryYears; }
            set { _noHistoryYears = value; }
        }
        private Ps_forecast_list psp_ForecastReport;
        private CheckBox checkBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public class AppConfig : IConfigurationSectionHandler
        {
            static String m_connectionString = String.Empty;
            static Int32 m_userCount = 0;
            public static String ConnectionString
            {
                get
                {
                    return m_connectionString;
                }
            }
            public static Int32 UserCount
            {
                get
                {
                    return m_userCount;
                }
            }

            static String ReadSetting(NameValueCollection nvc, String key, String defaultValue)
            {
                String theValue = nvc[key];
                if (theValue == String.Empty)
                    return defaultValue;

                return theValue;
            }

            public object Create(object parent, object configContext, XmlNode section)
            {
                NameValueCollection settings;

                try
                {
                    NameValueSectionHandler baseHandler = new NameValueSectionHandler();
                    settings = (NameValueCollection)baseHandler.Create(parent, configContext, section);
                }
                catch
                {
                    settings = null;
                }

                if (settings != null)
                {
                    m_connectionString = AppConfig.ReadSetting(settings, "ConnectionString", String.Empty);
                    m_userCount = Convert.ToInt32(AppConfig.ReadSetting(settings, "UserCount", "0"));
                }

                return settings;
            }
        }

        public FormForecastReport()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            _isEdit = false;
            _typeFlag = 1;
            _noHistoryYears = false;
            _typeText = "电量";
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.spinEdit2 = new DevExpress.XtraEditors.SpinEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.spinEdit3 = new DevExpress.XtraEditors.SpinEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "预测名称：";
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit1.Location = new System.Drawing.Point(128, 71);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit1.Properties.IsFloatValue = false;
            this.spinEdit1.Properties.Mask.EditMask = "####";
            this.spinEdit1.Properties.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.spinEdit1.Properties.MinValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit1.Size = new System.Drawing.Size(228, 21);
            this.spinEdit1.TabIndex = 2;
            this.spinEdit1.EditValueChanged += new System.EventHandler(this.spinEdit1_EditValueChanged);
            this.spinEdit1.Enter += new System.EventHandler(this.spinEdit1_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 14);
            this.label2.TabIndex = 2;
            this.label2.Text = "起始年份：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 117);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 14);
            this.label3.TabIndex = 5;
            this.label3.Text = "结束年份：";
            // 
            // spinEdit2
            // 
            this.spinEdit2.EditValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit2.Location = new System.Drawing.Point(128, 114);
            this.spinEdit2.Name = "spinEdit2";
            this.spinEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit2.Properties.IsFloatValue = false;
            this.spinEdit2.Properties.Mask.EditMask = "####";
            this.spinEdit2.Properties.MaxValue = new decimal(new int[] {
            2050,
            0,
            0,
            0});
            this.spinEdit2.Properties.MinValue = new decimal(new int[] {
            1980,
            0,
            0,
            0});
            this.spinEdit2.Size = new System.Drawing.Size(228, 21);
            this.spinEdit2.TabIndex = 3;
            this.spinEdit2.Enter += new System.EventHandler(this.spinEdit2_Enter);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 163);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 14);
            this.label4.TabIndex = 7;
            this.label4.Text = "历史数据年数：";
            this.label4.Visible = false;
            // 
            // spinEdit3
            // 
            this.spinEdit3.EditValue = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.spinEdit3.Location = new System.Drawing.Point(128, 160);
            this.spinEdit3.Name = "spinEdit3";
            this.spinEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinEdit3.Properties.IsFloatValue = false;
            this.spinEdit3.Properties.Mask.EditMask = "##";
            this.spinEdit3.Properties.MaxValue = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.spinEdit3.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spinEdit3.Size = new System.Drawing.Size(228, 21);
            this.spinEdit3.TabIndex = 4;
            this.spinEdit3.Visible = false;
            this.spinEdit3.Enter += new System.EventHandler(this.spinEdit3_Enter);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(181, 167);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 26);
            this.simpleButton1.TabIndex = 5;
            this.simpleButton1.Text = "确定(&O)";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.simpleButton2.Location = new System.Drawing.Point(281, 167);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 26);
            this.simpleButton2.TabIndex = 6;
            this.simpleButton2.Text = "取消(&C)";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(37, 148);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 18);
            this.checkBox1.TabIndex = 9;
            this.checkBox1.Text = "所有人可见";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(128, 31);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(228, 20);
            this.comboBox1.TabIndex = 10;
            // 
            // FormForecastReport
            // 
            this.ClientSize = new System.Drawing.Size(384, 213);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.spinEdit3);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.spinEdit2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.spinEdit1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormForecastReport";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "FormForecastReport";
            this.Load += new System.EventHandler(this.FormForecastReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit3.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
        string Path = Application.StartupPath + "\\Itop.exe.config";

        string LoginUser = "";
    
        public Ps_forecast_list Psp_ForecastReport
        {
            get { return psp_ForecastReport; }
            set { psp_ForecastReport = value; }
        }

        public bool IsEdit
        {
            get { return _isEdit; }
            set { _isEdit = value; }
        }

        public int TypeFlag
        {
            get { return _typeFlag; }
            set { _typeFlag = value; }
        }

        private void FormForecastReport_Load(object sender, EventArgs e)
        {
            Ps_forecast_list report = new Ps_forecast_list();
            report.UserID = MIS.ProgUID; 
            report.Col1 = "1";
            IList listReports = Common.Services.BaseService.GetList("SelectPs_forecast_listByCOL1AndUserID", report);

            for (int i = 0; i < listReports.Count;i++ )
            {
                comboBox1.Items.Add(((Ps_forecast_list)listReports[i]).Title);
            }

            LoginUser = projectUID;// SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");
            if(IsEdit)
            {
                comboBox1.Text = psp_ForecastReport.Title;
                spinEdit1.Value = psp_ForecastReport.StartYear;
                spinEdit2.Value = psp_ForecastReport.EndYear;
                
                //////if (psp_ForecastReport.Col1 == "1")
                //////{
                //////    checkBox1.Checked = true;
                //////}
                //////else
                //////{
                //////    checkBox1.Checked = false;
                
                //////}
                //////if (LoginUser == psp_ForecastReport.UserID)
                //////{
                //////    checkBox1.Visible = true;
                //////}
                //////else
                //////{
                //////    checkBox1.Visible = false;
                //////}











              //  spinEdit3.Value = psp_ForecastReport.HistoryYears;
            }
            else
            {
                //////checkBox1.Checked = true;
                spinEdit1.Value = DateTime.Now.Year;
                spinEdit2.Value = DateTime.Now.Year + 10;
                //comboBox1.Text = "本地区" + spinEdit1.Value + "～" + spinEdit2.Value + "年需" + _typeText + "预测表（方案）";
            }

            //if(_noHistoryYears)
            //{
            //    label4.Visible = spinEdit3.Visible = false;
            //    simpleButton1.Location = new Point(simpleButton1.Location.X, simpleButton1.Location.Y - 60);
            //    simpleButton2.Location = new Point(simpleButton2.Location.X, simpleButton2.Location.Y - 60);
            //    this.Height -= 60;
            //    if (!IsEdit)
            //    {
            //        spinEdit2.Value = DateTime.Now.Year + 5;
            //        textEdit1.Text = "本地区" + spinEdit1.Value.ToString() + "年/" + spinEdit2.Value.ToString() + "年年负荷曲线预测表";
            //    }
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MsgBox.Show("请选择预测名称！");
                return;
            }

            if(_isEdit)//修改
            {

            }
            else//新建
            {
                psp_ForecastReport = new Ps_forecast_list();
                psp_ForecastReport.ID = Guid.NewGuid().ToString().Substring(36 - 12);
                psp_ForecastReport.Col1 = TypeFlag.ToString();
            }
            psp_ForecastReport.Title = comboBox1.Text;
            psp_ForecastReport.StartYear = (int)spinEdit1.Value;
            psp_ForecastReport.EndYear = (int)spinEdit2.Value;
            psp_ForecastReport.UserID = projectUID; 
            
          //  System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();

            
          //  string LoginUser = configurationAppSettings.GetValue("lastLoginUserNumber", typeof(string)).ToString();
          //  psp_ForecastReport.UserID = LoginUser;

          ////  System.Configuration.ConfigurationSettings appreader = new System.Configuration.ConfigurationSettings();
          //  LoginUser = System.Configuration.ConfigurationSettings.AppSettings["lastLoginUserNumber"];


            // LoginUser = SetCfgValue("lastLoginUserNumber", Application.ExecutablePath + ".config");

            psp_ForecastReport.UserID = projectUID; ////LoginUser;

            //if (checkBox1.Checked)
            //    psp_ForecastReport.Col1 = "1";
            //else
            //    psp_ForecastReport.Col1 = "0";
       

            
     //       psp_ForecastReport.HistoryYears = (int)spinEdit3.Value;

            if (psp_ForecastReport.EndYear < psp_ForecastReport.StartYear + 1)
            {
                MsgBox.Show("结束年份应该大于起始年份至少1年！");
                return;
            }

            if (_isEdit)
            {
                try
                {
                    Common.Services.BaseService.Update<Ps_forecast_list>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("修改预测出错！");
                    return;
                }
            }
            else
            {
                try
                {
                   Common.Services.BaseService.Create<Ps_forecast_list>(psp_ForecastReport);
                }
                catch
                {
                    MsgBox.Show("新建预测出错！");
                    return;
                }
            }

            DialogResult = DialogResult.OK;
        }
        ////////public static string SetCfgValue(string AppKey, string FileName)
        ////////{
        ////////    System.Xml.XmlDocument xDoc = new XmlDocument();
        ////////    xDoc.Load(FileName);

        ////////    XmlNode xNode;
        ////////    XmlElement xElemKey;
        ////////    XmlElement xElemValue;

        ////////    xNode = xDoc.SelectSingleNode("//appSettings");

        ////////    xElemKey = (XmlElement)xNode.SelectSingleNode("//add[@key=\"" + AppKey + "\"]");
        ////////    if (xElemKey != null)
        ////////    {
        ////////        string[] str = xElemKey.OuterXml.Split('"');
        ////////        if(str!=null)
        ////////        {
        ////////            if(str.Length>3)
        ////////            {
        ////////                return str[3];
        ////////            }
        ////////            }
        ////////    }
        ////////    return "";
        ////////}

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            if(spinEdit2.Value < spinEdit1.Value)
            {
                spinEdit2.Value = spinEdit1.Value;
            }
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {
        }
        private InputLanguage oldInput = null;
        private void spinEdit1_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void spinEdit2_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void spinEdit3_Enter(object sender, EventArgs e)
        {
            oldInput = InputLanguage.CurrentInputLanguage;
            InputLanguage.CurrentInputLanguage = null;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }
    }
}

