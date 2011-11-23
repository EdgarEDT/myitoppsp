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
using System.IO;
using System.Xml;
using System.Reflection;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// ĸ��
    /// </summary>
    public partial class UCDevicePWKG : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDevicePWKG()
        {
            InitializeComponent();            
        }        
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            if (ID!=null)
            {
                con = " Type='" + ID + "'";
            }
            if (ID == "54")
            {
                con = " Type='54'";
            }
            if (ID == "55")
            {
                con = " Type='55'";
            }
            if (ID == "56")
            {
                con = " Type='56'";
            }
            if (ID == "57")
            {
                con = " Type='57'";
            }
            if (ID == "58")
            {
                con = " Type='58'";
            }
            if (ID == "59")
            {
                con = " Type='59'";
            }
            if (ID == "61")
            {
                con = " Type='61'";
            }
            base.Init();         
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            if (ID != null)
            {
                con = " and Type='" + ID + "'";
            }
            if (ID == "54")
            {
                con = " and Type='54'";
            }
            if (ID == "55")
            {
                con = " and Type='55'";
            }
            if (ID == "56")
            {
                con = " and Type='56'";
            }
            if (ID == "57")
            {
                con = " and Type='57'";
            }
            if (ID == "58")
            {
                con = " and Type='58'";
            }
            if (ID == "59")
            {
                con = " and Type='59'";
            }
            base.SelDevices();
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {            
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "���";
            column.FieldName = "EleID";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "Name";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
 
            //column.Caption = "������";
            //column.FieldName = "Flag";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column.OptionsColumn.AllowEdit = false;
           

            column = gridView1.Columns.Add();
            column.Caption = "���ѹ��KV��";
            column.FieldName = "RateVolt";
            column.Width = 150;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Ͷ��ʱ��";
            column.FieldName = "OperationYear";
            column.Width = 150;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return ID;
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
        public override void Add() {
            frmPWKGdlg dlg = new frmPWKGdlg();
            if (ID == "63" || ID == "64")
            {
                dlg.SetEnable();
            }
            dlg.Name = "";
            dlg.ProjectID = this.ProjectID;
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);            
            XmlNode node = xml.SelectSingleNode("devicetype/*[@id='" + ID + "']");
            if (ID!=null)
            {
                dlg.Text = node.Attributes["name"].Value.ToString() + "��Ϣ";
            }
            if(ID=="55"){
                dlg.Text = "����վ��Ϣ";
            }
            if (ID == "56")
            {
                dlg.Text = "��������Ϣ";
            }
            if (ID == "57")
            {
                dlg.Text = "���Ͽ�����Ϣ";
            }
            if (ID == "58")
            {
                dlg.Text = "���·�֧����Ϣ";
            }
            if (ID == "59")
            {
                dlg.Text = "���ɿ�����Ϣ";
            }
            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                PSPDEV dev = dlg.DeviceMx;
                dev.Type = ID;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
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
            frmPWKGdlg dlg = new frmPWKGdlg();
            if (ID == "63" || ID == "64")
            {
                dlg.SetEnable();
            }
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
             
              
                dlg.DeviceMx = dev;
                dlg.ProjectID = dev.ProjectID;
                Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
                //Assembly.GetExecutingAssembly().GetManifestResourceStream
                XmlDocument xml = new XmlDocument();
                xml.Load(fs);
                XmlNode node = xml.SelectSingleNode("devicetype/*[@id='" + ID + "']");
                if (node != null)
                {
                    dlg.Text = node.Attributes["name"].Value.ToString()+"��Ϣ";
                }
                if (ID == "55")
                {
                    dlg.Text = "����վ��Ϣ";
                }
                if (ID == "56")
                {
                    dlg.Text = "��������Ϣ";
                }
                if (ID == "57")
                {
                    dlg.Text = "���Ͽ�����Ϣ";
                }
                if (ID == "58")
                {
                    dlg.Text = "���·�֧����Ϣ";
                }
                if (ID == "59")
                {
                    dlg.Text = "���ɿ�����Ϣ";
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //���¼�¼
                    dev = dlg.DeviceMx;
                    dev.ProjectID = this.ProjectID;
                    dev.Type = ID;
                    UCDeviceBase.DataService.Update<PSPDEV>(dev);
                  
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        
        #endregion
    }
}

