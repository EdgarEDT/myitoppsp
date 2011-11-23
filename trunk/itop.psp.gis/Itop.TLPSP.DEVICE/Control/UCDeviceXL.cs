using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using Itop.Client.Projects;
using Itop.Domain.Graphics;
using Itop.Domain.Table;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// ĸ��
    /// </summary>
    public partial class UCDeviceXL : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        
        public UCDeviceXL() {
            InitializeComponent();            
        }        
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            con = " Type='05'";
            base.Init();          
        }
        /// <summary>
        /// ר�����ڳ����е�������ʾ
        /// </summary>
        public override void PspInit()
        {
            datatable1 = null;
            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + this.ProjectID + "' and Type='05'";

            IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));
            gridControl1.DataSource = datatable1;          
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='05'";
            base.SelDevices();
        }
        /// <summary>
        ///��·ͼ���ݲ�ѯ���
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='05'";
            base.SelshortDevices();
        }
        /// <summary>
        ///��·��ĩ�ڵ�Ž��и���
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='05'";

                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                        string iname = dev.IName;
                        if (iname != null)
                        {
                            con = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='01'and name='" + iname + "'";
                            PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp != null)
                            {
                                dev.FirstNode = psp.Number;
                                dev.RateVolt = psp.RateVolt;
                                dev.ReferenceVolt = psp.ReferenceVolt;
                            }
                        }

                        string jname = dev.JName;
                        if (jname != null)
                        {
                            con = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='01'and name='" + jname + "'";
                            PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp != null)
                            {
                                dev.LastNode = psp.Number;
                            }
                        }
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("���³ɹ���");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='05'";

                    IList list = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", con);
                    foreach (PSPDEV dev in list)
                    {
                        string iname = dev.IName;
                        if (iname != null)
                        {
                            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "'and pspdev.type='01'and pspdev.name='" + iname + "'";
                            PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp != null)
                            {
                                dev.FirstNode = psp.Number;
                                dev.RateVolt = psp.RateVolt;
                                dev.ReferenceVolt = psp.ReferenceVolt;
                            }
                        }

                        string jname = dev.JName;
                        if (jname != null)
                        {
                            con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "'and pspdev.type='01'and pspdev.name='" + jname + "'";
                            PSPDEV psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                            if (psp != null)
                            {
                                dev.LastNode = psp.Number;
                            }
                        }
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("���³ɹ���");
                }
                
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {
            string s=" ProjectID='"+Itop.Client.MIS.ProgUID+"'"; 
            IList list=Services.BaseService.GetList("SelectPS_Table_AreaWHByConn",s);
            DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1=new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            //repositoryItemMemoEdit1.Name = "repositoryItemMemoEdit1";
            // 
            // repositoryItemLookUpEdit1
            // 
            repositoryItemLookUpEdit1.AutoHeight = false;
            repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ID", "", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("Title")});
            repositoryItemLookUpEdit1.DisplayMember = "Title";
            repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            repositoryItemLookUpEdit1.NullText = "";
            repositoryItemLookUpEdit1.ValueMember = "ID";
            repositoryItemLookUpEdit1.DataSource = list;

            GridColumn column = gridView1.Columns.Add();
            column.Caption = "��·����";
            column.FieldName = "Name";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��·���";
            column.FieldName = "Number";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "i��ĸ����";
            column.FieldName = "IName";
            column.Width = 100;
            column.VisibleIndex = 3;
            column = gridView1.Columns.Add();
            column.Caption = "j��ĸ����";
            column.FieldName = "JName";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��׼��ѹ";
            column.FieldName = "ReferenceVolt";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���ѹ";
            column.FieldName = "RateVolt";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��·����";
            column.FieldName = "LineLength";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�����ͺ�";
            column.FieldName = "LineType";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "LineR";
            column.Width = 100;
            column.VisibleIndex =9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�翹";
            column.FieldName = "LineTQ";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "B/2";
            column.FieldName = "LineGNDC";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�������";
            column.FieldName = "ZeroR";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����翹";
            column.FieldName = "ZeroTQ";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "B0/2";
            column.FieldName = "ZeroGNDC";
            column.Width = 100;
            column.VisibleIndex = 15;
            column.OptionsColumn.AllowEdit = false;         
            column = gridView1.Columns.Add();
            column.Caption = "�����";
            column.FieldName = "Burthen";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����״̬";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��λ";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Ͷ�����";
            column.FieldName = "OperationYear";
            column.Width = 100;
            column.VisibleIndex = 19;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "һ�β����ӿ���";
            column.FieldName = "ISwitch";
            column.Width = 100;
            column.VisibleIndex = 20;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���β����ӿ���";
            column.FieldName = "JSwitch";
            column.Width = 100;
            column.VisibleIndex = 21;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "DQ";
            column.Width = 100;
            column.VisibleIndex = 22;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "AreaID";
            column.ColumnEdit = repositoryItemLookUpEdit1;
            column.Width = 100;
            column.VisibleIndex = 23;
            column.OptionsColumn.AllowEdit = false;


          
        }
        #endregion
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return "05";
        }
        #region ��¼����
        public override object SelectedDevice
        {
            get
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                if (row != null)
                {
                    return Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
                }
                return base.SelectedDevice;
            }
        }
        /// <summary>
        /// ������·ĸ�߸������ڱ��վ
        /// </summary>
        /// <param name="line"></param>
        public static void UpdateBDZ(PSPDEV line){

            LineInfo lineinfo = UCDeviceBase.DataService.GetOneByKey<LineInfo>(line.SUID);
            if (lineinfo == null) {
                return;
            }
            string iname = line.IName;
            string jname = line.JName;

            string sql = "where (name='" + iname + "' ) and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='05' ORDER BY Number";
            IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            string bdz1 = "";
            string bdz2 = "";
            if (list.Count > 0) {
               bdz1= DeviceHelper.GetBdzNameByID(list[0].SvgUID);
            }
            sql = "where (name='" + jname + "' ) and projectid='" + Itop.Client.MIS.ProgUID + "' and Type='05' ORDER BY Number";
            list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
            if (list.Count > 0) {
               bdz2= DeviceHelper.GetBdzNameByID(list[0].SvgUID);
            }
            lineinfo.ObligateField6 = bdz1;
            lineinfo.ObligateField7 = bdz2;
            UCDeviceBase.DataService.Update<LineInfo>(lineinfo);
        }
        public override void Add() {
            frmXLdlg dlg = new frmXLdlg();
            dlg.ProjectSUID = this.ProjectID;
            dlg.Name = "";
            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                PSPDEV dev = dlg.DeviceMx;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
                UpdateBDZ(dev);
                if (dev.NodeType == "0")
                {
                    dev.NodeType = "ƽ��ڵ�";
                }
                else if (dev.NodeType == "1")
                {
                    dev.NodeType = "PQ�ڵ�";
                }
                else if (dev.NodeType == "2")
                {
                    dev.NodeType = "PV�ڵ�";
                }
                else
                {
                    dev.NodeType = null;
                }
                if (dev.KSwitchStatus == "1")
                {
                    dev.KSwitchStatus = "�˳�����";
                }
                else
                {
                    dev.KSwitchStatus = "Ͷ������";
                }
                if (dev.UnitFlag == "0")
                {
                    dev.UnitFlag = "p.u.";
                }
                else
                {
                    if (dev.Type == "01" || dev.Type == "04" || dev.Type == "12")
                    {
                        dev.UnitFlag = "kV/MW/MVar";
                    }
                    else
                    {
                        dev.UnitFlag = "Ohm/10-6Siem";
                    }
                }
                DataRow row=datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row); 
            }
        }
        public override void Delete() {
            //ɾ����¼     
            base.Delete();
        }
        public override void Edit() {
            frmXLdlg dlg = new frmXLdlg(); 
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
                if (dev.NodeType == "ƽ��ڵ�")
                {
                    dev.NodeType = "0";
                }
                else if (dev.NodeType == "PQ�ڵ�")
                {
                    dev.NodeType = "1";
                }
                else if (dev.NodeType == "PV�ڵ�")
                {
                    dev.NodeType = "2";
                }
                if (dev.KSwitchStatus == "�˳�����")
                {
                    dev.KSwitchStatus = "1";
                }
                else
                {
                    dev.KSwitchStatus = "0";
                }
                if (dev.UnitFlag == "p.u.")
                {
                    dev.UnitFlag = "0";
                }
                else
                {
                    dev.UnitFlag = "1";

                }
                dlg.DeviceMx = dev;
                dlg.ProjectSUID = dev.ProjectID;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //���¼�¼
                    dev = dlg.DeviceMx;
                    dev.ProjectID = this.ProjectID;
                    UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    UpdateBDZ(dev);
                    if (dev.NodeType == "0")
                    {
                        dev.NodeType = "ƽ��ڵ�";
                    }
                    else if (dev.NodeType == "1")
                    {
                        dev.NodeType = "PQ�ڵ�";
                    }
                    else if (dev.NodeType == "2")
                    {
                        dev.NodeType = "PV�ڵ�";
                    }
                    else
                    {
                        dev.NodeType = null;
                    }
                    if (dev.KSwitchStatus == "1")
                    {
                        dev.KSwitchStatus = "�˳�����";
                    }
                    else
                    {
                        dev.KSwitchStatus = "Ͷ������";
                    }
                    if (dev.UnitFlag == "0")
                    {
                        dev.UnitFlag = "p.u.";
                    }
                    else
                    {
                        if (dev.Type == "01" || dev.Type == "04" || dev.Type == "12")
                        {
                            dev.UnitFlag = "kV/MW/MVar";
                        }
                        else
                        {
                            dev.UnitFlag = "Ohm/10-6Siem";
                        }
                    }
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        
        #endregion
    }
}

