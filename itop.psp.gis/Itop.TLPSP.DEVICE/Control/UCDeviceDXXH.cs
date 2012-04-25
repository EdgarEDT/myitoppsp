using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using System.Collections;
using DevExpress.XtraGrid.Columns;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// ���վ
    /// </summary>
    public partial class UCDeviceDXXH : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceDXXH() {
            InitializeComponent();
        }

        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            datatable1 = null;
            WireCategory wc = new WireCategory();
            wc.Type = "40";
            IList list = DataService.GetList("SelectWireCategoryList", wc);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(WireCategory));           
           
            gridControl1.DataSource = datatable1;
            
        }
        /// <summary>
        /// ���ݲ�ѯ����������ѡ���豸
        /// </summary>
        public override void SelDevices()
        {
            datatable1 = null;
            WireCategory wc = new WireCategory();
            wc.Type = "40";
            IList list = DataService.GetList("SelectWireCategoryList", wc);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(WireCategory));

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "WireType";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ�ȼ�(kV)";
            column.FieldName = "WireLevel";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "WireR";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�翹";
            column.FieldName = "WireTQ";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            
            column.Caption = "����";
            column.FieldName = "WireGNDC";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�������";
            column.FieldName = "ZeroR";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����翹";
            column.FieldName = "ZeroTQ";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();

            column.Caption = "�������";
            column.FieldName = "ZeroGNDC";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������(kA)";
            column.FieldName = "WireChange";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;

            column = gridView1.Columns.Add();
            column.Caption = "�߾�";
            column.FieldName = "WireLead";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������";
            column.FieldName = "gzl";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�޸�ʱ��";
            column.FieldName = "xftime";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "״̬";
            //column.FieldName = "flag_";
            //column.Width = 100;
            //column.VisibleIndex =7;
            //column.OptionsColumn.AllowEdit = false;
        }
        #endregion
        #region ��¼����
        public override object SelectedDevice {
            get {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                
                if (row != null) {
                    return Itop.Common.DataConverter.RowToObject<WireCategory>(row);
                }
                return base.SelectedDevice;
            }
        }

        public override string GetClassName()
        {
            return "WireCategory";
        }
        public override string GetType()
        {
            return "40";
        }
        public override void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("�ϼ�") > 0 || table.Rows[i][1].ToString().IndexOf("�ϼ�") > 0)
                    continue;
                WireCategory area = new WireCategory();
                area.Type = "40";
                foreach (DataColumn col in table.Columns)
                {
                    if (table.Rows[i][col] != null)
                    {                     
                            Type type = area.GetType().GetProperty(col.ColumnName).PropertyType;//.GetValue(area, null).GetType();
                            if (type == typeof(int))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(table.Rows[i][col].ToString()), null);
                            else if (type == typeof(string))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, table.Rows[i][col], null);
                            else if (type == typeof(decimal))
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, decimal.Parse(table.Rows[i][col].ToString()), null);
                            else
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, double.Parse(table.Rows[i][col].ToString()), null);
                    }
                }
                DataService.Create<WireCategory>(area);

            }
        }
        public override void Add() {
            frmDXXHdlg dlg = new frmDXXHdlg();

            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                WireCategory dev = dlg.DeviceMx;               
                DataService.Create<WireCategory>(dev);
                DataRow row = datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row);
            }
        }
        public override void Delete() {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {

                WireCategory dev = Itop.Common.DataConverter.RowToObject<WireCategory>(row);
                DialogResult dr = Itop.Common.MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ�������ͺţ�"+dev.WireType+"��?");
                if (dr == DialogResult.Yes) {
                    DataService.Delete<WireCategory>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public override void Alldel()
        {
            Itop.Common.MsgBox.Show("����һ��ɾ����������");        


        }
        public override void Edit() {
            frmDXXHdlg dlg = new frmDXXHdlg();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {
                WireCategory dev = Itop.Common.DataConverter.RowToObject<WireCategory>(row);
                dlg.DeviceMx = dev;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    //���¼�¼
                    dev = dlg.DeviceMx;
                    DataService.Update<WireCategory>(dev);
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        public override void Save() {

        }
        public override void Print() {

        }
        #endregion
    }
}

