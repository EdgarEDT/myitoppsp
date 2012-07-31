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
using DevExpress.XtraEditors.Repository;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// ���վ
    /// </summary>
    public partial class UCDeviceBDZ : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceBDZ() {
            InitializeComponent();
        }

        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init() {
            datatable1 = null;
            if (DeviceHelper.bdzwhere != "")
            {
                con = strCon + " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID NOT IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + this.ProjectID + "')";
            }
            else
            {
                con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID NOT IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + this.ProjectID + "')";
            }
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','�滮','��״')");
           
            gridControl1.DataSource = datatable1;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        }
        /// <summary>
        /// ר�����ڳ����е�������ʾ
        /// </summary>
        public override void PspInit()
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);

            //List<PSP_Substation_Info> delsum = new List<PSP_Substation_Info>();
            //foreach (PSP_Substation_Info dev in list)
            //{

            //    con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='"+dev.UID+"' ";
            //    IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
            //    if (list1.Count > 0)
            //        delsum.Add(dev);

            //}

            //for (int m = 0; m < delsum.Count;m++ )
            //{
            //    list.Remove(delsum[m]);
            //}
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','�滮','��״')");

            gridControl1.DataSource = datatable1;
        }
        public override void proInit(string year)
        {
            datatable1 = null;
            
                con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + year + "')";
            
            
                //con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + year + "')";
           
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            //if (!string.IsNullOrEmpty(year))
            //{
            //    List<PSP_Substation_Info> listremove = new List<PSP_Substation_Info>();
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (((PSP_Substation_Info)list[i]).S2.Length != 4)
            //        {
            //            listremove.Add((PSP_Substation_Info)list[i]);
            //        }
            //        else
            //        {
            //            if (Convert.ToDouble(((PSP_Substation_Info)list[i]).S2) > Convert.ToDouble(year))
            //            {
            //                listremove.Add((PSP_Substation_Info)list[i]);
            //            }
            //            if (((PSP_Substation_Info)list[i]).L29.Length == 4)
            //            {
            //                if (Convert.ToDouble(((PSP_Substation_Info)list[i]).L29) < Convert.ToDouble(year))
            //                {
            //                    listremove.Add((PSP_Substation_Info)list[i]);
            //                }
            //            }
            //        }
            //    }
            //    for (int i = 0; i < listremove.Count; i++)
            //    {
            //        list.Remove(listremove[i]);
            //    }
            //}
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','�滮','��״')");

            gridControl1.DataSource = datatable1;
            //��������
            gridView1.Columns["AreaID"].Visible = false;
            gridControl1.UseEmbeddedNavigator = true;
            gridControl1.EmbeddedNavigator.Buttons.Append.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Edit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.Remove.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.EndEdit.Visible = false;
            gridControl1.EmbeddedNavigator.Buttons.CancelEdit.Visible = false;
        }
        public override void PspInit(IList<object> listUID)
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
          

            List<PSP_Substation_Info> delsum = new List<PSP_Substation_Info>();
            //foreach (PSP_Substation_Info dev in list)
            //{

            //    con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='"+dev.UID+"' ";
            //    IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
            //    if (list1.Count > 0)
            //        delsum.Add(dev);
                
            //}
            //list.Remove(listUID);
            bool psiflag = false;
            try
            { 
                for (int i = 0; i < list.Count;i++)
                {
                    psiflag = false;
                    foreach (PSP_Substation_Info psi in listUID)
                    {
                        if (psi.UID == ((PSP_Substation_Info)list[i]).UID)
                        {
                            psiflag = true;                                             
                        }
                    }
                    if (!psiflag)
                    {
                        delsum.Add((PSP_Substation_Info)list[i]);
                    }
                }
            }
            catch (Exception e)
            {}
           // list.Remove(delsum);

            datatable1 = Itop.Common.DataConverter.ToDataTable(delsum, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','�滮','��״')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// ר�����������Ż���ʾ
        /// </summary>
        public override void WjghInit()
        {
            datatable1 = null;
            con = "AreaID = '" + Itop.Client.MIS.ProgUID + "'AND UID IN(select DeviceSUID from PSP_GprogElevice where type='���վ' and L2='0'and GprogUID='"+wjghuid+"')";
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','�滮','��״')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// ���ݲ�ѯ����������ѡ���豸
        /// </summary>
        public override void SelDevices()
        {
            datatable1 = null;
            con = strCon ;
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Substation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag='2','�滮','��״')");
            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns() {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "���վ����";
            column.FieldName = "Title";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ�ȼ�";
            column.FieldName = "L1";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����";
            column.FieldName = "L2";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "L4";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "����̨��";
            column.FieldName = "L3";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������";
            column.FieldName = "L10";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
           
            column = gridView1.Columns.Add();
            column.Caption = "Ͷ�����";
            column.FieldName = "S2";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�������";
            column.FieldName = "L29";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "ͣ�����";
            //column.FieldName = "L12";
            //column.Width = 100;
            //column.VisibleIndex = 5;
            //column.OptionsColumn.AllowEdit = false;
            
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "DQ";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "AreaName";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�޹�����";
            column.FieldName = "L26";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�޹���������";
            column.FieldName = "L27";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�鿴";
            column.FieldName = "AreaID";
            column.Width = 100;
            column.VisibleIndex = 14;
           // column.OptionsColumn.AllowEdit = false;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit1 = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit1.AutoHeight = false;
            repositoryItemHyperLinkEdit1.Caption = "վ������";
            repositoryItemHyperLinkEdit1.Name = "repositoryItemHyperLinkEdit1";
            repositoryItemHyperLinkEdit1.Click += new System.EventHandler(this.repositoryItemHyperLinkEdit1_Click);
            
            column.ColumnEdit =repositoryItemHyperLinkEdit1;

        }
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            int ihand = gridView1.FocusedRowHandle;
            if (ihand < 0)
                return;
            DataRow dr = gridView1.GetDataRow(ihand);
            PSP_Substation_Info pj = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(dr);
            double rl = 0;
            int bts = 0;
            frmDeviceManager_children frmc = new frmDeviceManager_children();
            frmc.ParentObj = pj;
            string[] types = new string[] { "01", "02","03", "12" };
            frmc.childrendevice(types);
            if (frmc.DialogResult == DialogResult.OK)
            {
                string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='03'and SvgUID='" + pj.UID + "'";
                IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                foreach (PSPDEV pd in list)
                {
                    if (!string.IsNullOrEmpty(pd.OperationYear) && !string.IsNullOrEmpty(pd.Date2) && pd.Date2.Length == 4 && !string.IsNullOrEmpty(pj.L29) && !string.IsNullOrEmpty(pj.L28))
                    {
                        if (Convert.ToInt32(pd.OperationYear) >= Convert.ToInt32(pj.L28) && Convert.ToInt32(pd.Date2) <= Convert.ToInt32(pj.L29))
                        {
                            rl += pd.SiN;
                            bts++;
                        }
                    }
                    else
                    {
                        rl += pd.SiN;
                        bts++;
                    }
                }
                pj.L2 = rl;
                pj.L3 = bts;
                dr["L2"] = rl;
                dr["L3"] = bts;
                UCDeviceBase.DataService.Update<PSP_Substation_Info>(pj);
            }
        }
        #endregion
        #region ��¼����
        public override object SelectedDevice {
            get {
                DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                
                if (row != null) {
                    return Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(row);
                }
                return base.SelectedDevice;
            }
        }

        public override string GetClassName()
        {
            return "PSP_Substation_Info";
        }
        public override string GetType()
        {
            return "20";
        }
        public override void Alldel()
        {
            if (Itop.Common.MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ����������") == DialogResult.Yes)
            {
                DataTable dat = gridView1.GridControl.DataSource as DataTable;
                foreach (DataRow dr in dat.Rows)
                {
                    if (dr != null)
                    {
                        PSP_Substation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(dr);
                        UCDeviceBase.DataService.Delete<PSP_Substation_Info>(dev);

                    }
                }
                dat.Clear();
                gridView1.GridControl.DataSource = dat;
            }


        }
        public override void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("�ϼ�") > 0 || table.Rows[i][1].ToString().IndexOf("�ϼ�") > 0)
                    continue;
                PSP_Substation_Info area = new PSP_Substation_Info();
                //area.UID += "|" + ProjectID;
                area.AreaID = ProjectID;
               // area.CreateDate = DateTime.Now;
                
                foreach (DataColumn col in table.Columns)
                {
                    try
                    {
                        if (table.Rows[i][col] != null)
                        {
                            string inserted = table.Rows[i][col].ToString();
                            if (inserted == "�滮" || inserted == "��״")
                                area.GetType().GetProperty("Flag").SetValue(area, inserted == "�滮" ? "2" : "1", null);
                            else
                            {
                                Type type = area.GetType().GetProperty(col.ColumnName).PropertyType;//.GetValue(area, null).GetType();
                                if (type == typeof(int))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, int.Parse(inserted == "" ? "0" : inserted), null);
                                else if (type == typeof(string))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, inserted, null);
                                else if (type == typeof(decimal))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, decimal.Parse(inserted == "" ? "0" : inserted), null);
                                else if (type == typeof(double))
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, double.Parse(inserted == "" ? "0.0" : inserted), null);
                                else if(type==typeof(DateTime))
                                {

                                }
                            }
                        }
                    }
                    catch { MessageBox.Show(string.Format("��{0}��{1}�в���������", i.ToString(), col.Caption)); }
                }
                DataService.Create<PSP_Substation_Info>(area);
                
            }
        }
        public override void Add() {
            frmBDZdlg dlg = new frmBDZdlg();
            dlg.ProjectID = Itop.Client.MIS.ProgUID;
            dlg.Name = "";
            dlg.CsbuttonVisble(true);
            dlg.IsRead = false;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //���Ӽ�¼ 
                PSP_Substation_Info dev = dlg.DeviceMx;
                dev.AreaID = Itop.Client.MIS.ProgUID;
                if (dlg.bcflag)   //�ڵ���豸������ʱ�� �Ѿ������˱���
                {
                    DataService.Update<PSP_Substation_Info>(dev);
                }
                else
                DataService.Create<PSP_Substation_Info>(dev);
                DataRow row = datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row);
            }
        }
        public override void Delete() {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {

                PSP_Substation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(row);
                DialogResult dr = Itop.Common.MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ�����վ��"+dev.Title+"��?");
                if (dr == DialogResult.Yes) {
                    DataService.Delete<PSP_Substation_Info>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                }
            }
        }
        public override void Edit() {
            frmBDZdlg dlg = new frmBDZdlg();
            dlg.CsbuttonVisble(true);
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null) {
                PSP_Substation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_Substation_Info>(row);
                dlg.DeviceMx = dev;
                dlg.IsRead = false;
                if (dlg.ShowDialog() == DialogResult.OK) {
                    //���¼�¼
                    dev = dlg.DeviceMx;
                    dev.AreaID = Itop.Client.MIS.ProgUID;
                    DataService.Update<PSP_Substation_Info>(dev);
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

