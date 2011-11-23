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
    public partial class UCDevicePWB : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDevicePWB()
        {
            InitializeComponent();            
        }        
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            if (ID == "50")
            {
                con = " Type='50'";
            }
            if (ID == "51")
            {
                con = " Type='51'";
            }
            if (ID == "52")
            {
                con = " Type='52'";
            }
            base.Init();         
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            if (ID == "50")
            {
                con = "and Type='50'";
            }
            if (ID == "51")
            {
                con = "and Type='51'";
            }
            if (ID == "52")
            {
                con = "and Type='52'";
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
            column.Caption = "��ѹ��̨��";
            column.FieldName = "Flag";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���ѹ��kV��";
            column.FieldName = "RateVolt";
            column.Width = 150;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������kVA��";
            column.FieldName = "Num2";
            column.Width = 150;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "Ͷ��ʱ��";
            column.FieldName = "OperationYear";
            column.Width = 150;
            column.VisibleIndex = 6;
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
            frmPWdlg dlg = new frmPWdlg();
            dlg.Name = "";
            dlg.ProjectID = this.ProjectID;
            if(ID=="50"){
                dlg.Text = "�������Ϣ";
            }
            if (ID == "51")
            {
                dlg.Text = "��ʽ����Ϣ";
            }
            if (ID == "52")
            {
                dlg.Text = "���ϱ���Ϣ";
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
            frmPWdlg dlg = new frmPWdlg(); 
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSPDEV dev = Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
             
              
                dlg.DeviceMx = dev;
                dlg.ProjectID = dev.ProjectID;
                if (ID == "50")
                {
                    dlg.Text = "�������Ϣ";
                }
                if (ID == "51")
                {
                    dlg.Text = "��ʽ����Ϣ";
                }
                if (ID == "52")
                {
                    dlg.Text = "���ϱ���Ϣ";
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

