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
    public partial class UCDeviceZX : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceZX()
        {
            InitializeComponent();            
        }        
        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {

            con = " Type='70'";

            base.Init();         
        }
        /// <summary>
        /// �����豸
        /// </summary>
        public override void SelDevices()
        {
            con = " Type='70'";
            base.SelDevices();
        }
        //���������·��ID
        private string parentid;
        public string ParentID
        {
            get
            {
                return parentid;
            }
            set
            {
                parentid = value;
            }
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {            
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "���";
            column.FieldName = "Number";
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
            return "70";
        }
        public int GetrowCount()
        {
            return gridView1.RowCount;
        }
        public PSPDEV GetLastObj()
        {
          
            DataRow row = gridView1.GetDataRow(gridView1.RowCount-1);

            if (row != null)
            {
                return Itop.Common.DataConverter.RowToObject<PSPDEV>(row);
            }
           else
            {
                return null;
            }
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
            frmZXdlg dlg = new frmZXdlg();
            dlg.Name = "";
            dlg.ProjectID = this.ProjectID;
            
            if (!string.IsNullOrEmpty(parentid))
            {
                dlg.AreaID = parentid;
            }
            dlg.Number = GetrowCount();  //���
           
            
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);            
            XmlNode node = xml.SelectSingleNode("devicetype/*[@id='" + ID + "']");

            dlg.Text = "֧����Ϣ";
          
            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                PSPDEV dev = dlg.DeviceMx;
                dev.Type = ID;
                dev.ProjectID = this.ProjectID;
                UCDeviceBase.DataService.Create("InsertPSPDEV", dev);
                //������Ӧ�����߶�
                PSPDEV parentobj = new PSPDEV();
                parentobj.SUID = dev.AreaID;
                parentobj = Services.BaseService.GetOneByKey<PSPDEV>(parentobj);
                 PSPDEV firstnodeobj =null;
                if (parentobj!=null)
                {
                    string sql = "where AreaID='" + parentobj.SUID + "'and Type='70' and projectid='" + ProjectID + "'order by number";
                    IList<PSPDEV> list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                    if (list1.Count>1)
                    {
                        firstnodeobj = list1[list1.Count - 2];
                    }
                  
                }
              
                if (firstnodeobj!=null&&parentobj!=null)
                {
                    PSPDEV pv = new PSPDEV();
                    pv.Name = parentobj.Name + "_��·��" + (dev.Number).ToString();
                    pv.AreaID = parentid;
                    pv.Type = "74";
                    pv.FirstNode = firstnodeobj.Number;
                    pv.LastNode = dev.Number;
                    pv.IName = firstnodeobj.SUID;
                    pv.JName = dev.SUID;
                    pv.LineType = parentobj.LineType;
                    pv.RateVolt=parentobj.RateVolt;
                    pv.LineLength = 1;
                    pv.ProjectID = ProjectID;
                    if (!string.IsNullOrEmpty(parentobj.LineType))
                    {
                        WireCategory rc = new WireCategory();
                        rc.WireLevel = parentobj.RateVolt.ToString();
                        rc.WireType = parentobj.LineType;
                        rc.Type = "40";
                        rc = (WireCategory)UCDeviceBase.DataService.GetObject("SelectWireCategoryByKeyANDWireLevel", rc);
                        if (rc != null)
                        {
                            pv.HuganTQ3 = rc.gzl*pv.LineLength;
                            pv.HuganTQ4 = rc.xftime;
                        }
                    }
                    Services.BaseService.Create<PSPDEV>(pv);

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
            frmZXdlg dlg = new frmZXdlg();          
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
                dlg.Text = "֧����Ϣ";
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

