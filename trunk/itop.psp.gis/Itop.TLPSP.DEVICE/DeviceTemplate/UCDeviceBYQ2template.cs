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

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// ĸ��
    /// </summary>
    public partial class UCDeviceBYQ2template : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceBYQ2template()
        {
            InitializeComponent();            
        }        
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            datatable1 = null;
            con = "type='02'";

            IList list = DataService.GetList("SelectTemplate_PSPDEVByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(Template_PSPDEV));
            
            gridControl1.DataSource = datatable1;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false; 
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='02'";
            base.SelDevices();
        }
        /// <summary>
        ///��·ͼ���ݲ�ѯ���
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='02'";
            base.SelshortDevices();
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {            
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "TemplateType";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "���";
            //column.FieldName = "Number";
            //column.Width = 100;
            //column.VisibleIndex = 2;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "��ѹ��ĸ��";
            //column.FieldName = "IName";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "��ѹ��ĸ��";
            //column.FieldName = "JName";
            //column.Width = 100;
            //column.VisibleIndex = 4;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "LineR";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�翹";
            column.FieldName = "LineTQ";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "LineGNDC";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�絼";
            column.FieldName = "G";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���";
            column.FieldName = "K";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��·����";
            column.FieldName = "Burthen";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ�����ӷ�ʽ";
            column.FieldName = "LineLevel";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ�����ӷ�ʽ";
            column.FieldName = "LineType";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���ĵ�ӵص翹";
            column.FieldName = "BigTQ";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���ĵ�ӵص���";
            column.FieldName = "SmallTQ";
            column.Width = 100;
            column.VisibleIndex = 14;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ����ѹ";
            column.FieldName = "Vi0";
            column.Width = 100;
            column.VisibleIndex = 15;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ����ѹ";
            column.FieldName = "Vj0";
            column.Width = 100;
            column.VisibleIndex = 16;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ���׼��ѹ";
            column.FieldName = "Vib";
            column.Width = 100;
            column.VisibleIndex = 17;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ���׼��ѹ";
            column.FieldName = "Vjb";
            column.Width = 100;
            column.VisibleIndex = 18;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����״̬";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 19;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��λ";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 20;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "Ͷ�����";
            //column.FieldName = "OperationYear";
            //column.Width = 100;
            //column.VisibleIndex = 21;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "��ѹ�����ӿ���";
            //column.FieldName = "ISwitch";
            //column.Width = 100;
            //column.VisibleIndex = 22;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "��ѹ�����ӿ���";
            //column.FieldName = "JSwitch";
            //column.Width = 100;
            //column.VisibleIndex = 23;
            //column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "Template_PSPDEV";
        }
        public override string GetType()
        {
            return "02";
        }
        #region ��¼����
        /// <summary>
        ///��ѹ����ĩ�ڵ�Ž��и���
        /// </summary>
        public override void UpdateNumber()
        {
            try
            {
                if (updatenumberflag)
                {
                    con = "where projectid='" + Itop.Client.MIS.ProgUID + "' and Type='02'";

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
                                dev.Vi0 = psp.RateVolt;
                                dev.Vib = psp.ReferenceVolt;
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
                                dev.Vj0 = psp.RateVolt;
                                dev.Vjb = psp.ReferenceVolt;
                            }
                        }
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("������ɣ�");
                }
                else
                {
                    con = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + projectdeviceid + "' and pspdev.Type='02'";

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
                                dev.Vi0 = psp.RateVolt;
                                dev.Vib = psp.ReferenceVolt;
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
                                dev.Vj0 = psp.RateVolt;
                                dev.Vjb = psp.ReferenceVolt;
                            }
                        }
                        UCDeviceBase.DataService.Update<PSPDEV>(dev);
                    }
                    Init();
                    MessageBox.Show("������ɣ�");
                }
               
            }
            catch (System.Exception ex)
            {
            	
            }
           
        }
        public override object SelectedDevice
        {
            get
            {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);

                if (row != null)
                {
                    return Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
                }
                return base.SelectedDevice;
            }
        }
        public override void Add() {
            frmBYQ2templatedlg dlg = new frmBYQ2templatedlg();
            dlg.Name = "";
            dlg.ProjectSUID = this.ProjectID;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                Template_PSPDEV dev = dlg.DeviceMx;
               // dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create<Template_PSPDEV>(dev);
                DataRow row=datatable1.NewRow();
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
                datatable1.Rows.Add(row); 
            }
        }
        public override void Delete() {
            //ɾ����¼     
            //ɾ����¼     
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Template_PSPDEV dev = Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
                if (Itop.Common.MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ����" + dev.TemplateType + "��?") == DialogResult.Yes)
                {
                    UCDeviceBase.DataService.Delete<Template_PSPDEV>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public override void Edit() {
            frmBYQ2templatedlg dlg = new frmBYQ2templatedlg(); 
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                Template_PSPDEV dev = Itop.Common.DataConverter.RowToObject<Template_PSPDEV>(row);
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
                    UCDeviceBase.DataService.Update<Template_PSPDEV>(dev);
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
                    //
                    //string sql = "S1='" + dev.Name + "'and S4='�������ѹ��'";
                    //IList<Psp_Attachtable> ilist = UCDeviceBase.DataService.GetList<Psp_Attachtable>("SelectPsp_AttachtableByCont", sql);
                    //if (ilist.Count>0)
                    //{
                    //    Psp_Attachtable pdr = ilist[0];
                    //    pdr.ZHI =(double) dev.Burthen;
                    //    pdr.S3 = dev.OperationYear;
                    //    pdr.startYear = dev.Date1;
                    //    pdr.endYear= dev.Date2;
                    //    UCDeviceBase.DataService.Update<Psp_Attachtable>(pdr);
                    //}
                }
            }
        }
        
        #endregion
    }
}

