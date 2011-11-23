using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Projects;
namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// Summary description for XtraSelectfrm.
    /// </summary>
    public class XtraSelectfrm : DevExpress.XtraEditors.XtraForm
    {
        private GroupControl groupControl1;
        private RadioGroup radioGroup1;
        private SimpleButton simpleButton1;
        private SimpleButton simpleButton2;
        private TextEdit textEdit1;
        private SpinEdit spinEdit1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;
        private ComboBoxEdit comboBoxEdit1;
        private TextEdit textbh;
        private string _sqlcondition;
        public string _stype = "";
        public string Sqlcondition
        {
            get { return _sqlcondition; }
            set { _sqlcondition = value; }
        }
        public string StrNum
        {
            get { return textbh.Text; }
        }
        public XtraSelectfrm()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }
        private void init()
        {
          string  con = " AreaID = '" + Itop.Client.MIS.ProgUID + "'";
          IList list = UCDeviceBase.DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
          foreach (PSP_Substation_Info sb in list)
          {
              if (comboBoxEdit1.Properties.Items.IndexOf(sb.Title) == -1)
              {
                  comboBoxEdit1.Properties.Items.Add(sb.Title);                 
              }
          }

        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            init();
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.spinEdit1 = new DevExpress.XtraEditors.SpinEdit();
            this.textbh = new DevExpress.XtraEditors.TextEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textbh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.comboBoxEdit1);
            this.groupControl1.Controls.Add(this.spinEdit1);
            this.groupControl1.Controls.Add(this.textbh);
            this.groupControl1.Controls.Add(this.textEdit1);
            this.groupControl1.Controls.Add(this.radioGroup1);
            this.groupControl1.Location = new System.Drawing.Point(13, 28);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(267, 199);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "查询条件";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Enabled = false;
            this.comboBoxEdit1.Location = new System.Drawing.Point(113, 104);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(136, 21);
            this.comboBoxEdit1.TabIndex = 7;
            this.comboBoxEdit1.SelectedIndexChanged += new System.EventHandler(this.comboBoxEdit1_SelectedIndexChanged);
            // 
            // spinEdit1
            // 
            this.spinEdit1.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEdit1.Enabled = false;
            this.spinEdit1.Location = new System.Drawing.Point(113, 68);
            this.spinEdit1.Name = "spinEdit1";
            this.spinEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEdit1.Size = new System.Drawing.Size(136, 21);
            this.spinEdit1.TabIndex = 6;
            // 
            // textbh
            // 
            this.textbh.Location = new System.Drawing.Point(112, 137);
            this.textbh.Name = "textbh";
            this.textbh.Size = new System.Drawing.Size(136, 21);
            this.textbh.TabIndex = 5;
            this.textbh.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbh_KeyPress);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(114, 32);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(136, 21);
            this.textEdit1.TabIndex = 5;
            this.textEdit1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textEdit1_KeyPress);
            this.textEdit1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textEdit1_KeyDown);
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "0";
            this.radioGroup1.Location = new System.Drawing.Point(6, 21);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "设备名称检索"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "电压等级检索"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "拓扑关系检索"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("3", "设备编号检索")});
            this.radioGroup1.Size = new System.Drawing.Size(101, 144);
            this.radioGroup1.TabIndex = 4;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButton1.Location = new System.Drawing.Point(37, 246);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 28);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "确定";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton2.Location = new System.Drawing.Point(200, 246);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(75, 28);
            this.simpleButton2.TabIndex = 2;
            this.simpleButton2.Text = "取消";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // XtraSelectfrm
            // 
            this.ClientSize = new System.Drawing.Size(292, 291);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.groupControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XtraSelectfrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "检索设备";
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textbh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.SelectedIndex==0)
            {
                textEdit1.Enabled = true;
                spinEdit1.Enabled = false;
                comboBoxEdit1.Enabled = false;
            }
            if (radioGroup1.SelectedIndex == 1)
            {
                textEdit1.Enabled = false;
                spinEdit1.Enabled = true;
                comboBoxEdit1.Enabled = false;
            }
            if (radioGroup1.SelectedIndex == 2)
            {
                textEdit1.Enabled = false;
                spinEdit1.Enabled = false;
                comboBoxEdit1.Enabled = true;
            }
        }
        public int getselecindex
        {
            get{return radioGroup1.SelectedIndex ;}
        }
        public string DeviceName
        {
            get
            {
                return textEdit1.Text;
            }
        }
        public double devicevolt
        {
            get { return (double)spinEdit1.Value; }
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

               _sqlcondition = null;
               if (radioGroup1.SelectedIndex==0)
               {
                   _sqlcondition += " and name like'%" + textEdit1.Text + "%'";
               }
               if (radioGroup1.SelectedIndex==1)
               {
                   _sqlcondition += "and RateVolt='" + (double)spinEdit1.Value + "'";
               }
               if (radioGroup1.SelectedIndex == 3)
               {
                   if (Convert.ToInt32(_type) > 49)
                   {
                       _sqlcondition += "and EleID='" + textbh.Text + "'";
                   }
                   else
                   {
                       _sqlcondition += "and Number='" + textbh.Text + "'";
                   }
               }
               this.DialogResult = DialogResult.OK;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //生成拓扑关系
            vistlineflag.Clear();
            vistbusflag .Clear();
            vistsubstationflag .Clear();
            vistpowerflag.Clear();
            visttrans2flag.Clear();
            visttrans3flag.Clear();
            vistfdjflag.Clear();
            vistfhflag.Clear();
            vistmlflag.Clear();
            vistml2flag.Clear();
            vistcldkflag.Clear();
            vistcldrflag.Clear();
            vistbldkflag.Clear();
            vistbldrflag.Clear();
            vistdlqflag.Clear();
            string con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title='"+comboBoxEdit1.Text+"'";
            PSP_Substation_Info list =(PSP_Substation_Info) UCDeviceBase.DataService.GetObject("SelectPSP_Substation_InfoListByWhere", con);
            vistsubstationflag.Add(list.Title, list);
            con = "WHERE SvgUID='" + list.UID + "'AND projectid='" + Itop.Client.MIS.ProgUID + "'AND Type='01'";
            IList buslist = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            if (buslist!=null)
            {
                for (int i = 0; i < buslist.Count;i++ )
                {
                    TopolyCheck(type, (PSPDEV)buslist[i]);
                }                
                foreach (KeyValuePair<string, PSPDEV> keyvalue in vistbusflag)
                {
                    if (string.IsNullOrEmpty(keyvalue.Value.SvgUID))
                    {
                        MessageBox.Show(keyvalue.Value.Name + "没有关联变电站！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                       
                    }
                    else
                    {
                        if (list == null) return;
                        list.UID = keyvalue.Value.SvgUID;

                    }
                 list.UID=keyvalue.Value.SvgUID;

                 list = (PSP_Substation_Info)UCDeviceBase.DataService.GetObject("SelectPSP_Substation_InfoByKey", list);
                    if (list!=null)
                    {
                        if (!vistsubstationflag.ContainsKey(list.Title))                       
                            vistsubstationflag.Add(list.Title, list);
                    }
                  
                 PSP_PowerSubstation_Info pp = new PSP_PowerSubstation_Info();
                 pp.UID = keyvalue.Value.SvgUID;
                 pp = (PSP_PowerSubstation_Info)UCDeviceBase.DataService.GetObject("SelectPSP_PowerSubstation_InfoByKey", pp);
                    if (pp!=null)
                    {
                        if (!vistpowerflag.ContainsKey(pp.Title))                      
                            vistpowerflag.Add(pp.Title, pp);
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='11'";
                    IList pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistbldkflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistbldkflag.Add(psp.Name, psp);
                           
                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='09'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistbldrflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistbldrflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='12'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistfhflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistfhflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='04'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistfdjflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistfdjflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='13'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistmlflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistmlflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE JName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='13'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistmlflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistmlflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='06'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistdlqflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistdlqflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE HuganLine1='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='14'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistml2flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistml2flag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE HuganLine2='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='14'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistml2flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistml2flag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='02'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (visttrans2flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            visttrans2flag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE JName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='02'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (visttrans2flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            visttrans2flag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='03'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (visttrans3flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            visttrans3flag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE JName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='03'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (visttrans3flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            visttrans3flag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE KName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='03'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (visttrans3flag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            visttrans3flag.Add(psp.Name, psp);

                        }
                    }
                }
                foreach (KeyValuePair<string, PSPDEV> keyvalue in vistlineflag)
                {
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='08'";
                    IList pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistcldrflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistcldrflag.Add(psp.Name, psp);

                        }
                    }
                    con = " WHERE IName='" + keyvalue.Value.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='10'";
                    pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV psp in pspline1)
                    {
                        if (vistcldkflag.ContainsKey(psp.Name))
                        {
                            continue;
                        }
                        else
                        {
                            vistcldkflag.Add(psp.Name, psp);

                        }
                    }
                }

            }
           
            else
            {
                MessageBox.Show("此变电站没有拓扑关系，请查询数据后再进行检索!");
            }
           
        }
        private string _type;
        public string type
        {
            get { return _type; }
            set { _type = value; }
        }
        public Dictionary<string, PSP_Substation_Info> vistsubstationflag = new Dictionary<string, PSP_Substation_Info>();
        public Dictionary<string, PSP_PowerSubstation_Info> vistpowerflag = new Dictionary<string, PSP_PowerSubstation_Info>();
        public Dictionary<string, PSPDEV> vistbusflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistlineflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistcldrflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistbldrflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistcldkflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistbldkflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistfhflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistfdjflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistmlflag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistml2flag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> visttrans2flag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> visttrans3flag = new Dictionary<string, PSPDEV>();
        public Dictionary<string, PSPDEV> vistdlqflag = new Dictionary<string, PSPDEV>();
        private void TopolyCheck(string type,PSPDEV bus)
        {
            if (vistbusflag.ContainsKey(bus.Name))
            {
                return;
            }
            else
            {              
               vistbusflag.Add(bus.Name, bus);
               //if (type=="05")
               //{
                   string con = " WHERE IName='" + bus.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='05'";
                   IList pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (pspline1 == null || vistlineflag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           vistlineflag.Add(psp.Name,psp);
                          
                           con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode =(PSPDEV) UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if ( pspnextnode==null || vistbusflag.ContainsKey(pspnextnode.Name) )
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type,pspnextnode);
                       }
                   }
                   con = " WHERE JName='" + bus.Name + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='05'";
                   pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (vistlineflag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           vistlineflag.Add(psp.Name, psp);
                          
                           con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                       }
                   }
                   con = "WHERE IName='" + bus.Name + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='02'";
                   pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (visttrans2flag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           visttrans2flag.Add(psp.Name, psp);
                         
                           con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                       }
                   }
                   con = "WHERE JName='" + bus.Name + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='02'";
                   pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (visttrans2flag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           visttrans2flag.Add(psp.Name, psp);
                          
                           con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                       }
                   }
                   con = "WHERE IName='" + bus.Name + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='03'";
                   pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (visttrans3flag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           visttrans3flag.Add(psp.Name, psp);
                          
                           con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                           con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                       }
                   }
                   con = "WHERE JName='" + bus.Name + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='03'";
                   pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (visttrans3flag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           visttrans3flag.Add(psp.Name, psp);
                          
                           con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                           con = " WHERE Name='" + psp.KName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                       }
                   }
                   con = "WHERE KName='" + bus.Name + "'AND ProjectID='" + Itop.Client.MIS.ProgUID + "'AND Type='03'";
                   pspline1 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                   foreach (PSPDEV psp in pspline1)
                   {
                       if (visttrans3flag.ContainsKey(psp.Name))
                       {
                           continue;
                       }
                       else
                       {
                           visttrans3flag.Add(psp.Name, psp);
                        
                           con = " WHERE Name='" + psp.IName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           PSPDEV pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                           con = " WHERE Name='" + psp.JName + "' AND ProjectID = '" + Itop.Client.MIS.ProgUID + "'" + "AND Type='01'";
                           pspnextnode = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                           if (pspnextnode == null || vistbusflag.ContainsKey(pspnextnode.Name))
                           {
                               continue;
                           }
                           else
                               TopolyCheck(type, pspnextnode);
                       }
                   }
               //}
            }
        }

        private void textEdit1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void textEdit1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar ==Convert.ToChar("'"))
            {
                e.Handled = true;
            }

        }

        private void textbh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar("'"))
            {
                e.Handled = true;
            }
        }
    }
}

