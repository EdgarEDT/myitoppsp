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
    /// ��Դ
    /// </summary>
    public partial class UCDeviceDY : Itop.TLPSP.DEVICE.UCDeviceBase
    {
        public UCDeviceDY()
        {
            InitializeComponent();
        }

        #region ��ʼ����
        /// <summary>
        /// �豸��ʼ��
        /// </summary>
        public override void Init()
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND UID NOT IN (SELECT DeviceSUID FROM PSP_ELCDEVICE WHERE ProjectSUID ='" + this.ProjectID + "')";

            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'�滮','��״')");

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

            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
           
            List<PSP_PowerSubstation_Info> delsum = new List<PSP_PowerSubstation_Info>();
            foreach (PSP_PowerSubstation_Info dev in list)
            {

                con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='" + dev.UID + "' ";
                IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
                if (list1.Count > 0)
                    delsum.Add(dev);

            }
            for (int m = 0; m < delsum.Count; m++)
            {
                list.Remove(delsum[m]);
            }
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'�滮','��״')");
            gridControl1.DataSource = datatable1;
        }
        public override void proInit(string year)
        {
            datatable1 = null;
            con = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND  UID IN (SELECT PSPDEV.SVGUID FROM PSPDEV, PSP_ELCDEVICE WHERE  PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + year + "')";
            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            //if (!string.IsNullOrEmpty(year))
            //{
            //    List<PSP_PowerSubstation_Info> listremove = new List<PSP_PowerSubstation_Info>();
            //    for (int i = 0; i < list.Count; i++)
            //    {
            //        if (((PSP_PowerSubstation_Info)list[i]).S3.Length != 4)
            //        {
            //            listremove.Add((PSP_PowerSubstation_Info)list[i]);
            //        }
            //        else
            //        {
            //            if (Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S3) > Convert.ToDouble(year))
            //            {
            //                listremove.Add((PSP_PowerSubstation_Info)list[i]);
            //            }
            //            if (((PSP_PowerSubstation_Info)list[i]).S30.Length == 4)
            //            {
            //                if (Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S30) < Convert.ToDouble(year))
            //                {
            //                    listremove.Add((PSP_PowerSubstation_Info)list[i]);
            //                }
            //            }
            //        }
            //    }
            //    for (int i = 0; i < listremove.Count; i++)
            //    {
            //        list.Remove(listremove[i]);
            //    }
            //}

            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'�滮','��״')");

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

            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);

            //List<PSP_PowerSubstation_Info> delsum = new List<PSP_PowerSubstation_Info>();
            //foreach (PSP_PowerSubstation_Info dev in list)
            //{

            //    con = "WHERE ProjectSUID = '" + this.ProjectID + "'AND DeviceSUID='" + dev.UID + "' ";
            //    IList list1 = DataService.GetList("SelectPSP_ElcDeviceByCondition", con);
            //    if (list1.Count > 0)
            //        delsum.Add(dev);

            //}
            //for (int m = 0; m < delsum.Count; m++)
            //{
            //    list.Remove(delsum[m]);
            //}
            List<PSP_PowerSubstation_Info> delsum = new List<PSP_PowerSubstation_Info>();
            bool psiflag = false;
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    psiflag = false;
                    foreach (PSP_PowerSubstation_Info psi in listUID)
                    {
                        if (psi.UID == ((PSP_PowerSubstation_Info)list[i]).UID)
                        {
                            psiflag = true;
                        }
                    }
                    if (!psiflag)
                    {
                        delsum.Add((PSP_PowerSubstation_Info)list[i]);
                    }
                }
            }
            catch (Exception e)
            { }
            datatable1 = Itop.Common.DataConverter.ToDataTable(delsum, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'�滮','��״')");
            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// ר�����������Ż���ʾ
        /// </summary>
        public override void WjghInit()
        {
            datatable1 = null;
            con = "AreaID = '" + Itop.Client.MIS.ProgUID + "'AND UID IN(select DeviceSUID from PSP_GprogElevice where type='��Դ' and L2='0'AND GprojUID='"+wjghuid+"')";
            IList list = DataService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'�滮','��״')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// ���ݲ�ѯ����������ѡ���豸
        /// </summary>
        public override void SelDevices()
        {
            datatable1 = null;
            con = strCon;
            IList list = DataService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            datatable1 = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_PowerSubstation_Info));
            datatable1.Columns.Add("flag_", typeof(string), "IIF(flag=2,'�滮','��״')");

            gridControl1.DataSource = datatable1;
        }
        /// <summary>
        /// �����豸��ʾ��
        /// </summary>
        public override void InitColumns()
        {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "�糧����";
            column.FieldName = "Title";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��ѹ�ȼ�";
            column.FieldName = "S1";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������";
            column.FieldName = "S2";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "S18";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;

            column = gridView1.Columns.Add();
            column.Caption = "Ͷ�����";
            column.FieldName = "S3";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�������";
            column.FieldName = "S30";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������";
            column.FieldName = "S9";
            column.Width = 100;
            column.VisibleIndex =7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�糧����";
            column.FieldName = "S10";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "������";
            column.FieldName = "S11";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "��������Сʱ��";
            column.FieldName = "S12";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "���õ���";
            column.FieldName = "S13";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�Ƿ�ͳ��";
            column.FieldName = "S14";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "�����Ա�";
            column.FieldName = "S8";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "ͣ�����";
            //column.FieldName = "L12";
            //column.Width = 100;
            //column.VisibleIndex = 5;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "״̬";
            //column.FieldName = "flag_";
            //column.Width = 100;
            //column.VisibleIndex = 6;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "��������";
            //column.FieldName = "S5";
            //column.Width = 100;
            //column.VisibleIndex = 7;
            //column.OptionsColumn.AllowEdit = false;

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

            column.ColumnEdit = repositoryItemHyperLinkEdit1;
           
            
           
            
        }
        private void repositoryItemHyperLinkEdit1_Click(object sender, EventArgs e)
        {
            int ihand = gridView1.FocusedRowHandle;
            if (ihand < 0)
                return;
            DataRow dr = gridView1.GetDataRow(ihand);
            PSP_PowerSubstation_Info pj = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(dr);
            double rl = 0;
            int bts = 0;
            frmDeviceManager_children frmc = new frmDeviceManager_children();
            frmc.ParentObj = pj;
            string[] types = new string[] { "01", "02", "04" };
            frmc.childrendevice(types);
            if (frmc.DialogResult == DialogResult.OK)
            {
                string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='02'and SvgUID='" + pj.UID + "'";
                IList<PSPDEV> list = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                foreach (PSPDEV pd in list)
                {
                    if (!string.IsNullOrEmpty(pd.OperationYear) && !string.IsNullOrEmpty(pd.Date2) && pd.Date2.Length == 4 && !string.IsNullOrEmpty(pj.S30) && !string.IsNullOrEmpty(pj.S29))
                    {
                        if (Convert.ToInt32(pd.OperationYear) >= Convert.ToInt32(pj.S29) && Convert.ToInt32(pd.Date2) <= Convert.ToInt32(pj.S30))
                        {
                            rl += (double)pd.Burthen;
                            bts++;
                        }
                    }
                    else
                    {
                        rl += (double)pd.Burthen;
                        bts++;
                    }
                }
                pj.S2 = rl.ToString();
                dr["S2"] = rl.ToString();
                UCDeviceBase.DataService.Update<PSP_PowerSubstation_Info>(pj);
            }
        }
        #endregion
        public override string GetClassName()
        {
            return "PSP_PowerSubstation_Info";
        }
        public override string GetType()
        {
            return "11";
        }
        public override void UpdateIn(DataTable table)
        {
            for (int i = 0; i < table.Rows.Count; i++)
            {
                if (table.Rows[i][0].ToString().IndexOf("�ϼ�") > 0 || table.Rows[i][1].ToString().IndexOf("�ϼ�") > 0)
                    continue;
                PSP_PowerSubstation_Info area = new PSP_PowerSubstation_Info();
                area.UID += "|" + ProjectID;
                area.AreaID = ProjectID;
                area.CreateDate = DateTime.Now;
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
                                else if (type == typeof(DateTime))
                                {

                                }
                            }
                        }
                    }
                    catch { MessageBox.Show(string.Format("��{0}��{1}�в���������", i.ToString(), col.Caption)); }
                }
                DataService.Create<PSP_PowerSubstation_Info>(area);

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
                    return Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(row);
                }
                return base.SelectedDevice;
            }
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
                        PSP_PowerSubstation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(dr);
                        UCDeviceBase.DataService.Delete<PSP_PowerSubstation_Info>(dev);
                        //ɾ���ڶ�������

                        string delcon = "SvgUID='" + dr["UID"].ToString() + "'and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
                        DataService.Update("DeletePSPDEVbywhere", delcon);
                    }
                }
                dat.Clear();
                gridView1.GridControl.DataSource = dat;
            }


        }
        public override void Add()
        {
            frmDYdlg dlg = new frmDYdlg();
            dlg.ProjectID = Itop.Client.MIS.ProgUID;
            dlg.czButtonVisble(true);
            dlg.Name = "";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                //���Ӽ�¼ 
                PSP_PowerSubstation_Info dev = dlg.DeviceMx;
                dev.AreaID = Itop.Client.MIS.ProgUID;
                if (dlg.bcflag)  //˵���Ѿ�����
                {
                    DataService.Update<PSP_PowerSubstation_Info>(dev);
                }
                else
                DataService.Create<PSP_PowerSubstation_Info>(dev);
                DataRow row = datatable1.NewRow();
                Itop.Common.DataConverter.ObjectToRow(dev, row);
                datatable1.Rows.Add(row);
            }
        }
        public override void Delete()
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {

                PSP_PowerSubstation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(row);
                DialogResult dr = Itop.Common.MsgBox.ShowYesNo("�Ƿ�ȷ��ɾ����" + dev.Title + "��?");
                if (dr == DialogResult.Yes)
                {
                    DataService.Delete<PSP_PowerSubstation_Info>(dev);
                    ((DataTable)gridControl1.DataSource).Rows.Remove(row);
                    //ɾ���ڶ�������

                    string delcon = "SvgUID='" + dev.UID + "'and ProjectID = '" + Itop.Client.MIS.ProgUID + "'";
                    DataService.Update("DeletePSPDEVbywhere", delcon);
                }
            }
        }
        public override void Edit()
        {
            frmDYdlg dlg = new frmDYdlg();
            dlg.czButtonVisble(true);
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                PSP_PowerSubstation_Info dev = Itop.Common.DataConverter.RowToObject<PSP_PowerSubstation_Info>(row);
                dlg.DeviceMx = dev;
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    //���¼�¼
                    dev = dlg.DeviceMx;
                    dev.AreaID = this.ProjectID;
                    DataService.Update<PSP_PowerSubstation_Info>(dev);
                    Itop.Common.DataConverter.ObjectToRow(dev, row);
                }
            }
        }
        public override void Save()
        {
        }
        public override void Print()
        {
        }
        #endregion
    }
}

