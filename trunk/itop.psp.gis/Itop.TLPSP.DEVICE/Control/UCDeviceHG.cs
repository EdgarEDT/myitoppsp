using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Collections;
using Itop.Client.Projects;
using Itop.Domain.Graphics;
namespace Itop.TLPSP.DEVICE
{
    public partial class UCDeviceHG : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceHG()
        {
            InitializeComponent();
        }
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init()
        {
            con = " Type='15'";
            base.Init();
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            con = "and Type='15'";
            base.SelDevices();
        }
        /// <summary>
        ///��·ͼ���ݲ�ѯ���
        /// </summary>
        public override void SelshortDevices()
        {
            con = " and Type='15'";
            base.SelshortDevices();
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns()
        {

            GridColumn column = gridView1.Columns.Add();
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "Number";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "�ڵ�����";
            //column.FieldName = "NodeType";
            //column.Width = 100;
            //column.VisibleIndex = 3;
            //column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������·1";
            column.FieldName = "HuganLine1";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������·2";
            column.FieldName = "HuganLine2";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��׼��ѹ";
            column.FieldName = "ReferenceVolt";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���ип�";
            column.FieldName = "HuganTQ1";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��λ";
            column.FieldName = "UnitFlag";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        public override string GetClassName()
        {
            return "PSPDEV";
        }
        public override string GetType()
        {
            return "15";
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
        public override void Add()
        {
            frmHGdlg dlg = new frmHGdlg();
            //dlg.Name = "";
            dlg.ProjectID = this.ProjectID;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //���Ӽ�¼ 
                PSPDEV dev = dlg.DeviceMx;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
                DataRow row = datatable1.NewRow();
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
        public override void Delete()
        {
            //ɾ����¼     
            base.Delete();
        }
        public override void Edit()
        {
            frmHGdlg dlg = new frmHGdlg();
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
                dlg.ProjectID = dev.ProjectID;
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
