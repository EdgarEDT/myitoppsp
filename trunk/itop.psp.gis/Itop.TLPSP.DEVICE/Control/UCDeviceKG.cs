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
    public partial class UCDeviceKG : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceKG() {
            InitializeComponent();            
        }        
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            con = " Type='07'";
            base.Init();         
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='07'";
            base.SelDevices();
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {            
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "���뿪������";
            column.FieldName = "Name";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���뿪�ر��";
            column.FieldName = "Number";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����״̬";
            column.FieldName = "KSwitchStatus";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
           
        }
        #endregion
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return "07";
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
            frmKGdlg dlg = new frmKGdlg();
            dlg.Name = "";
            dlg.ProjectSUID = this.ProjectID;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                PSPDEV dev = dlg.DeviceMx;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
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
            base.Delete();
        }
        public override void Edit() {
            frmKGdlg dlg = new frmKGdlg(); 
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

