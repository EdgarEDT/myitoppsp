using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Table;
using System.Collections;
namespace Itop.TLPSP.DEVICE
{
    public partial class FrmAutofh : DevExpress.XtraEditors.XtraForm
    {
        public FrmAutofh()
        {
            InitializeComponent();
        }
        private Dictionary<string,Ps_Table_220Result> dic220=new Dictionary<string,Ps_Table_220Result>();
        public  Dictionary<string,Ps_Table_220Result> Dic220
        {
            get { return dic220; }
            set { dic220 = value; }
        }
        private Dictionary<string, Ps_Table_110Result> dic110 = new Dictionary<string, Ps_Table_110Result>();
        public Dictionary<string, Ps_Table_110Result> Dic110
        {
            get { return dic110; }
            set { dic110 = value; }
        }
        private double tsl;
        public double TSL
        {
            get { return (double)spinEdit1.Value; }
            set { spinEdit1.Value = (decimal)value; }
        }
        private Dictionary<string, IList<PSP_Substation_Info>> sub220list = new Dictionary<string, IList<PSP_Substation_Info>>();
        public Dictionary<string, IList<PSP_Substation_Info>> Sub220list
        {
            get { return sub220list; }
            set { sub220list = value; }
        }
        private Dictionary<string, IList<PSP_Substation_Info>> sub110list = new Dictionary<string, IList<PSP_Substation_Info>>();
        public Dictionary<string, IList<PSP_Substation_Info>> Sub110list
        {
            get { return sub110list; }
            set { sub110list = value; }
        }
        private void initcombox()
        {
            string ss = " ProjectID='" + Itop.Client.MIS.ProgUID + "' ";
            IList<PS_Table_AreaWH> listq = UCDeviceBase.DataService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", ss);
            this.InitionData(this.comboBoxEdit4, "Title", "Title", "请选择", "地区", listq);

        }
       private string belongyear;
        public string BelongYear
        {
            get { return belongyear; }
            set { belongyear = value; }
        }
        private List<string> listarea=new List<string>();
        public List<string> ListArea
        {
            get { return listarea; }
            set { listarea = value; }
        }
        private void InitionData(DevExpress.XtraEditors.CheckedComboBoxEdit comboBox, string displayMember, string valueMember, string nullTest, string cnStr, object post)
        {
            comboBox.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            comboBox.Properties.DataSource = post;
            comboBox.Properties.DisplayMember = displayMember;
            comboBox.Properties.ValueMember = valueMember;
            comboBox.Properties.NullText = nullTest;
            comboBox.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(valueMember, "ID", 15, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(displayMember, cnStr)});
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

           
            string areaname=string.Empty;
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in this.comboBoxEdit4.Properties.Items)
            {
                if (item.CheckState == CheckState.Checked)
                {
                    areaname = "Title like '%全社会最高负荷' and ParentID in ( select ID from Ps_Table_220Result where Title='" + item.Value.ToString() + "'and ProjectID='" + Itop.Client.MIS.ProgUID + "')";
                    IList<Ps_Table_220Result> list=UCDeviceBase.DataService.GetList<Ps_Table_220Result>("SelectPs_Table_220ResultByConn",areaname);
                    if (list.Count>0)
                    {
                        dic220.Add(item.Value.ToString(),list[0]);
                    }
                    areaname = "Title like '%全社会最高负荷' and ParentID in ( select ID from Ps_Table_110Result where Title='" + item.Value.ToString() + "'and ProjectID='" + Itop.Client.MIS.ProgUID + "')";
                     IList<Ps_Table_110Result> list1=UCDeviceBase.DataService.GetList<Ps_Table_110Result>("SelectPs_Table_110ResultByConn",areaname);
                    if (list1.Count>0)
                    {
                        dic110.Add(item.Value.ToString(),list1[0]);
                    }
                    if (string.IsNullOrEmpty(belongyear))
                    {
                        areaname = " AreaID='" + Itop.Client.MIS.ProgUID + "' and AreaName='" + item.Value.ToString() + "' and L1=220 and S4 not in('专变')  ";
                    }
                    else
                        areaname = " AreaID='" + Itop.Client.MIS.ProgUID + "' and AreaName='" + item.Value.ToString() + "' and L1=220 and S4 not in('专变') and cast(S2 as int)<= " + Convert.ToInt32(BelongYear);
                    IList<PSP_Substation_Info> list2 = UCDeviceBase.DataService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", areaname);
                    if (list2.Count>0)
                    {
                        Sub220list.Add(item.Value.ToString(), list2);
                    }
                    if (string.IsNullOrEmpty(belongyear))
                    {
                        areaname = " AreaID='" + Itop.Client.MIS.ProgUID + "' and AreaName='" + item.Value.ToString() + "' and L1=110 and S4 not in('专变')  ";
                    }
                    else
                        areaname = " AreaID='" + Itop.Client.MIS.ProgUID + "' and AreaName='" + item.Value.ToString() + "' and L1=110 and S4 not in('专变') and cast(S2 as int)<= " + Convert.ToInt32(BelongYear);
                    IList<PSP_Substation_Info> list3 = UCDeviceBase.DataService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", areaname);
                    if (list3.Count > 0)
                    {
                        Sub110list.Add(item.Value.ToString(), list2);
                    }
                    listarea.Add(item.Value.ToString());
                }
            }

            this.DialogResult = DialogResult.OK;
        }

        private void FrmAutofh_Load(object sender, EventArgs e)
        {
            initcombox();
        }

    }
}