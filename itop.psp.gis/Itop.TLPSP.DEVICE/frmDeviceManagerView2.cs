using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Xml;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraTreeList.Columns;
using Itop.Domain.Graphics;
using Itop.TLPSP.DEVICE.Mysql;
using Itop.Client.Common;
using Itop.Client;

namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 设备管理
    /// </summary>
    public partial class frmDeviceManagerView2 : Itop.Client.Base.FormModuleBase
    {
        public frmDeviceManagerView2()
        {
            InitializeComponent();
            Init();
            InitDeviceType();
        }
        #region 初始化
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
           // bar.AddItems(new DevExpress.XtraBars.BarItem[] { barButtonItemclose });
            
            barQuery.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barPrint.Caption = "导出";
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            barAdd.Caption = "查询";
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barClose.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            treeList1.FocusedNode = null;
        }
        protected void Init() {
            this.Text = "设备参数管理";
            this.WindowState = FormWindowState.Maximized;
            if(!string.IsNullOrEmpty(this.smmprog.ProgName))
            this.Text = this.smmprog.ProgName;
        }
        /// <summary>
        /// 初始设备分类
        /// </summary>
        //private void InitDeviceType() {
        //    Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
        //    //Assembly.GetExecutingAssembly().GetManifestResourceStream
        //    XmlDocument xml = new XmlDocument();
        //    xml.Load(fs);
        //    XmlNodeList nodes= xml.GetElementsByTagName("device");
        //    DataTable table =new DataTable();
        //    table.Columns.Add("id",typeof(string));
        //    table.Columns.Add("name",typeof(string));
        //    table.Columns.Add("class",typeof(string));
        //    table.Columns.Add("ParentID", typeof(string));
        //    TreeListColumn column = new TreeListColumn();
        //    column.Caption="设备分类";
        //    column.FieldName="name";
        //    column.VisibleIndex = 0;
        //    column.Width = 180;
        //    column.OptionsColumn.AllowEdit = false;
        //    column.OptionsColumn.AllowSort = false;
        //    this.treeList1.Columns.AddRange(new TreeListColumn[] {
        //    column});
        //    //foreach (XmlNode node in nodes)
        //    //{
        //    //    DataRow row = table.NewRow();
        //    //    row["id"] = node.Attributes["id"].Value;
        //    //    row["name"] = node.Attributes["name"].Value;
        //    //    row["class"] = node.Attributes["class"].Value;
        //    //    table.Rows.Add(row);
        //    //}
        //    DataRow row = table.NewRow();
        //    row["name"] = "变电站";
        //    row["id"] = "20";
        //    row["class"] = "Itop.TLPSP.DEVICE.UCDeviceBDZ";
        //    row["ParentID"] = "-1";
        //    table.Rows.Add(row);

        //    string sql_1 = " AreaID='"+MIS.ProgUID+"' order by L1";
        //    IList<PSP_Substation_Info> ls1= Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere",sql_1);
        //    for (int n = 0; n < ls1.Count;n++ )
        //    {
        //        DataRow _row = table.NewRow();
        //        _row["name"] = ls1[n].Title;
        //        _row["id"] = ls1[n].UID;
        //        _row["ParentID"] = "20";
        //        _row["class"] = "";
        //        table.Rows.Add(_row);
        //        DataRow row_1 = table.NewRow();
        //        row_1["name"] = "母线";
        //        row_1["id"] = ls1[n].UID + Convert.ToString(n);
        //        row_1["ParentID"] = ls1[n].UID;
        //        row_1["class"] = "Itop.TLPSP.DEVICE.UCDeviceMX";
        //        table.Rows.Add(row_1);
        //        DataRow row_4 = table.NewRow();
        //        row_4["name"] = "断路器";
        //        row_4["id"] = "06" + Convert.ToString(n);
        //        row_4["class"] = "Itop.TLPSP.DEVICE.UCDeviceDLQ";
        //        row_4["ParentID"] = ls1[n].UID;
        //        table.Rows.Add(row_4);
        //        string sql_2 = " where type='01' and SvgUID='" + ls1[n].UID + "' and ProjectID='"+MIS.ProgUID+"' order by RateVolt";
        //        IList<PSPDEV> ls2 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql_2);
        //        for (int n2 = 0; n2 < ls2.Count; n2++)
        //        {
        //            DataRow _row2 = table.NewRow();
        //            _row2["name"] = ls2[n2].Name;
        //            _row2["id"] = ls2[n2].SUID;
        //            _row2["ParentID"] = ls1[n].UID + Convert.ToString(n); ;
        //            _row2["class"] = "";
        //            table.Rows.Add(_row2);

        //            DataRow row_5 = table.NewRow();
        //            row_5["name"] = "线路";
        //            row_5["id"] = Convert.ToString(n)+Convert.ToString(n2) +"05";
        //            row_5["class"] = "Itop.TLPSP.DEVICE.UCDeviceXL";
        //            row_5["ParentID"] = ls2[n2].SUID;
        //            table.Rows.Add(row_5);
        //            DataRow row_10 = table.NewRow();
        //            row_10["name"] = "串联电容器";
        //            row_10["id"] =  Convert.ToString(n)+Convert.ToString(n2)+"08";
        //            row_10["class"] = "Itop.TLPSP.DEVICE.UCDeviceCLDR";
        //            row_10["ParentID"] = Convert.ToString(n) + Convert.ToString(n2) + "05";
        //            table.Rows.Add(row_10);
        //            DataRow row_11 = table.NewRow();
        //            row_11["name"] = "串联电抗器";
        //            row_11["id"] = Convert.ToString(n)+Convert.ToString(n2)+ "10";
        //            row_11["class"] = "Itop.TLPSP.DEVICE.UCDeviceCLDK";
        //            row_11["ParentID"] = Convert.ToString(n) + Convert.ToString(n2) + "05";
        //            table.Rows.Add(row_11);

        //            DataRow row_2 = table.NewRow();
        //            row_2["name"] = "两绕组变压器";
        //            row_2["id"] = "02";
        //            row_2["class"] = "Itop.TLPSP.DEVICE.UCDeviceBYQ2";
        //            row_2["ParentID"] = ls2[n2].SUID;
        //            table.Rows.Add(row_2);
        //            DataRow row_3 = table.NewRow();
        //            row_3["name"] = "三绕组变压器";
        //            row_3["id"] = "03";
        //            row_3["class"] = "Itop.TLPSP.DEVICE.UCDeviceBYQ3";
        //            row_3["ParentID"] = ls2[n2].SUID;
        //            table.Rows.Add(row_3);
                  
        //            DataRow row_7 = table.NewRow();
        //            row_7["name"] = "并联电容器";
        //            row_7["id"] = "09";
        //            row_7["class"] = "Itop.TLPSP.DEVICE.UCDeviceBLDR";
        //            row_7["ParentID"] = ls2[n2].SUID;
        //            table.Rows.Add(row_7);
        //            DataRow row_6 = table.NewRow();
        //            row_6["name"] = "并联电抗器";
        //            row_6["id"] = "11";
        //            row_6["class"] = "Itop.TLPSP.DEVICE.UCDeviceBLDK";
        //            row_6["ParentID"] = ls2[n2].SUID;
        //            table.Rows.Add(row_6);
        //            //DataRow row_8 = table.NewRow();
        //            //row_8["name"] = "1/2母联开关";
        //            //row_8["id"] = "13";
        //            //row_8["class"] = "Itop.TLPSP.DEVICE.UCDeviceML";
        //            //row_8["ParentID"] = ls2[n2].SUID;
        //            //table.Rows.Add(row_8);
        //            //DataRow row_9 = table.NewRow();
        //            //row_9["name"] = "3/2母联开关";
        //            //row_9["id"] = "14";
        //            //row_9["class"] = "Itop.TLPSP.DEVICE.UCDeviceML2";
        //            //row_9["ParentID"] = ls2[n2].SUID;
        //            //table.Rows.Add(row_9);

        //        }
        //    }

        //    DataRow row2 = table.NewRow();
        //    row2["name"] = "电源";
        //    row2["id"] = "30";
        //    row2["class"] = "Itop.TLPSP.DEVICE.UCDeviceDY";
        //    row2["ParentID"] = "-1";
        //    table.Rows.Add(row2);
        //    string sql_8 = " AreaID='" + MIS.ProgUID + "' order by S1";
        //    IList<PSP_PowerSubstation_Info> ls8 = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", sql_8);
        //        for (int n2 = 0; n2 < ls8.Count; n2++)
        //        {
        //            DataRow _row2 = table.NewRow();
        //            _row2["name"] = ls8[n2].Title;
        //            _row2["id"] = ls8[n2].UID;
        //            _row2["ParentID"] = "30" ;
        //            _row2["class"] = "";
        //            table.Rows.Add(_row2);
        //            DataRow row_5 = table.NewRow();
        //            row_5["name"] = "发电机";
        //            row_5["id"] = "04";
        //            row_5["class"] = "Itop.TLPSP.DEVICE.UCDeviceFDJ";
        //            row_5["ParentID"] = ls8[n2].UID;
        //            table.Rows.Add(row_5);
        //        }

        //    DataRow row3 = table.NewRow();
        //    row3["name"] = "线路";
        //    row3["id"] = "05";
        //    row3["class"] = "Itop.TLPSP.DEVICE.UCDeviceXL";
        //    row3["ParentID"] = "-1";
        //    table.Rows.Add(row3);

        //    DataRow row4 = table.NewRow();
        //    row4["name"] = "配电室";
        //    row4["id"] = "50";
        //    row4["class"] = "Itop.TLPSP.DEVICE.UCDevicePWB";
        //    row4["ParentID"] = "-1";
        //    table.Rows.Add(row4);
        //    DataRow row5 = table.NewRow();
        //    row5["name"] = "箱式变";
        //    row5["id"] = "51";
        //    row5["class"] = "Itop.TLPSP.DEVICE.UCDevicePWB";
        //    row5["ParentID"] = "-1";
        //    table.Rows.Add(row5);
        //    DataRow row6 = table.NewRow();
        //    row6["name"] = "柱上变";
        //    row6["id"] = "52";
        //    row6["class"] = "Itop.TLPSP.DEVICE.UCDevicePWB";
        //    row6["ParentID"] = "-1";
        //    table.Rows.Add(row6);

        //    DataRow row60 = table.NewRow();
        //    row60["name"] = "开闭所";
        //    row60["id"] = "54";
        //    row60["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
        //    row60["ParentID"] = "-1";
        //    table.Rows.Add(row60);

        //    //DataRow row7 = table.NewRow();
        //    //row7["name"] = "开关站";
        //    //row7["id"] = "55";
        //    //row7["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
        //    //row7["ParentID"] = "-1";
        //    //table.Rows.Add(row7);
        //    DataRow row8 = table.NewRow();
        //    row8["name"] = "环网柜";
        //    row8["id"] = "56";
        //    row8["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
        //    row8["ParentID"] = "-1";
        //    table.Rows.Add(row8);
        //    DataRow row9 = table.NewRow();
        //    row9["name"] = "柱上开关";
        //    row9["id"] = "57";
        //    row9["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
        //    row9["ParentID"] = "-1";
        //    table.Rows.Add(row9);
        //    DataRow row10 = table.NewRow();
        //    row10["name"] = "电缆分支箱";
        //    row10["id"] = "58";
        //    row10["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
        //    row10["ParentID"] = "-1";
        //    table.Rows.Add(row10);
        //    //DataRow row11 = table.NewRow();
        //    //row11["name"] = "负荷开关";
        //    //row11["id"] = "59";
        //    //row11["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
        //    //row11["ParentID"] = "-1";
        //    //table.Rows.Add(row11);
        //    //treeList1.DataSource = table;
        //    treeList1.DataSource = null;
        //    gridControl1.DataSource = table;
            
        //}
        private void InitDeviceType()
        {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            //Assembly.GetExecutingAssembly().GetManifestResourceStream
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            table.Columns.Add("ParentID", typeof(string));
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            this.treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            //foreach (XmlNode node in nodes)
            //{
            //    DataRow row = table.NewRow();
            //    row["id"] = node.Attributes["id"].Value;
            //    row["name"] = node.Attributes["name"].Value;
            //    row["class"] = node.Attributes["class"].Value;
            //    table.Rows.Add(row);
            //}
            DataRow row = table.NewRow();
            row["name"] = "变电站";
            row["id"] = "20";
            row["class"] = "Itop.TLPSP.DEVICE.UCDeviceBDZ";
            row["ParentID"] = "-1";
            table.Rows.Add(row);

            string sql_1 = " AreaID='" + MIS.ProgUID + "' order by L1";
            IList<PSP_Substation_Info> ls1 = Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere", sql_1);
            for (int n = 0; n < ls1.Count; n++)
            {
                DataRow _row = table.NewRow();
                _row["name"] = ls1[n].Title;
                _row["id"] = ls1[n].UID;
                _row["ParentID"] = "20";
                _row["class"] = "";
                table.Rows.Add(_row);
                DataRow row_1 = table.NewRow();
                row_1["name"] = "母线";
                row_1["id"] = ls1[n].UID + Convert.ToString(n);
                row_1["ParentID"] = ls1[n].UID;
                row_1["class"] = "Itop.TLPSP.DEVICE.UCDeviceMX";
                table.Rows.Add(row_1);
                DataRow row_4 = table.NewRow();
                row_4["name"] = "断路器";
                row_4["id"] = "06" + Convert.ToString(n);
                row_4["class"] = "Itop.TLPSP.DEVICE.UCDeviceDLQ";
                row_4["ParentID"] = ls1[n].UID;
                table.Rows.Add(row_4);
                string sql_2 = " where type='01' and SvgUID='" + ls1[n].UID + "' and ProjectID='" + MIS.ProgUID + "' order by RateVolt";
                IList<PSPDEV> ls2 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql_2);
                for (int n2 = 0; n2 < ls2.Count; n2++)
                {
                    DataRow _row2 = table.NewRow();
                    _row2["name"] = ls2[n2].Name;
                    _row2["id"] = ls2[n2].SUID;
                    _row2["ParentID"] = ls1[n].UID + Convert.ToString(n); ;
                    _row2["class"] = "";
                    table.Rows.Add(_row2);

                    DataRow row_5 = table.NewRow();
                    row_5["name"] = "线路";
                    row_5["id"] = Convert.ToString(n) + Convert.ToString(n2) + "05";
                    row_5["class"] = "Itop.TLPSP.DEVICE.UCDeviceXL";
                    row_5["ParentID"] = ls2[n2].SUID;
                    table.Rows.Add(row_5);
                    DataRow row_10 = table.NewRow();
                    row_10["name"] = "串联电容器";
                    row_10["id"] = Convert.ToString(n) + Convert.ToString(n2) + "08";
                    row_10["class"] = "Itop.TLPSP.DEVICE.UCDeviceCLDR";
                    row_10["ParentID"] = Convert.ToString(n) + Convert.ToString(n2) + "05";
                    table.Rows.Add(row_10);
                    DataRow row_11 = table.NewRow();
                    row_11["name"] = "串联电抗器";
                    row_11["id"] = Convert.ToString(n) + Convert.ToString(n2) + "10";
                    row_11["class"] = "Itop.TLPSP.DEVICE.UCDeviceCLDK";
                    row_11["ParentID"] = Convert.ToString(n) + Convert.ToString(n2) + "05";
                    table.Rows.Add(row_11);

                    DataRow row_2 = table.NewRow();
                    row_2["name"] = "两绕组变压器";
                    row_2["id"] = Convert.ToString(n) + Convert.ToString(n2) + "02";
                    row_2["class"] = "Itop.TLPSP.DEVICE.UCDeviceBYQ2";
                    row_2["ParentID"] = ls2[n2].SUID;
                    table.Rows.Add(row_2);
                    DataRow row_3 = table.NewRow();
                    row_3["name"] = "三绕组变压器";
                    row_3["id"] = Convert.ToString(n) + Convert.ToString(n2) + "03" ;
                    row_3["class"] = "Itop.TLPSP.DEVICE.UCDeviceBYQ3";
                    row_3["ParentID"] = ls2[n2].SUID;
                    table.Rows.Add(row_3);

                    DataRow row_7 = table.NewRow();
                    row_7["name"] = "并联电容器";
                    row_7["id"] = Convert.ToString(n) + Convert.ToString(n2) + "09" ;
                    row_7["class"] = "Itop.TLPSP.DEVICE.UCDeviceBLDR";
                    row_7["ParentID"] = ls2[n2].SUID;
                    table.Rows.Add(row_7);
                    DataRow row_6 = table.NewRow();
                    row_6["name"] = "并联电抗器";
                    row_6["id"] = Convert.ToString(n) + Convert.ToString(n2) + "11" ;
                    row_6["class"] = "Itop.TLPSP.DEVICE.UCDeviceBLDK";
                    row_6["ParentID"] = ls2[n2].SUID;
                    table.Rows.Add(row_6);
                    //DataRow row_8 = table.NewRow();
                    //row_8["name"] = "1/2母联开关";
                    //row_8["id"] = "13";
                    //row_8["class"] = "Itop.TLPSP.DEVICE.UCDeviceML";
                    //row_8["ParentID"] = ls2[n2].SUID;
                    //table.Rows.Add(row_8);
                    //DataRow row_9 = table.NewRow();
                    //row_9["name"] = "3/2母联开关";
                    //row_9["id"] = "14";
                    //row_9["class"] = "Itop.TLPSP.DEVICE.UCDeviceML2";
                    //row_9["ParentID"] = ls2[n2].SUID;
                    //table.Rows.Add(row_9);

                }
            }

            DataRow row2 = table.NewRow();
            row2["name"] = "电源";
            row2["id"] = "30";
            row2["class"] = "Itop.TLPSP.DEVICE.UCDeviceDY";
            row2["ParentID"] = "-1";
            table.Rows.Add(row2);
            string sql_8 = " AreaID='" + MIS.ProgUID + "' order by S1";
            IList<PSP_PowerSubstation_Info> ls8 = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", sql_8);
            for (int n2 = 0; n2 < ls8.Count; n2++)
            {
                DataRow _row2 = table.NewRow();
                _row2["name"] = ls8[n2].Title;
                _row2["id"] = ls8[n2].UID;
                _row2["ParentID"] = "30";
                _row2["class"] = "";
                table.Rows.Add(_row2);
                DataRow row_5 = table.NewRow();
                row_5["name"] = "发电机";
                row_5["id"] =  Convert.ToString(n2)+"04";
                row_5["class"] = "Itop.TLPSP.DEVICE.UCDeviceFDJ";
                row_5["ParentID"] = ls8[n2].UID;
                table.Rows.Add(row_5);
            }

            DataRow row3 = table.NewRow();
            row3["name"] = "线路";
            row3["id"] = "05";
            row3["class"] = "Itop.TLPSP.DEVICE.UCDeviceXL";
            row3["ParentID"] = "-1";
            table.Rows.Add(row3);

            DataRow row4 = table.NewRow();
            row4["name"] = "配电室";
            row4["id"] = "50";
            row4["class"] = "Itop.TLPSP.DEVICE.UCDevicePWB";
            row4["ParentID"] = "-1";
            table.Rows.Add(row4);
            DataRow row5 = table.NewRow();
            row5["name"] = "箱式变";
            row5["id"] = "51";
            row5["class"] = "Itop.TLPSP.DEVICE.UCDevicePWB";
            row5["ParentID"] = "-1";
            table.Rows.Add(row5);
            DataRow row6 = table.NewRow();
            row6["name"] = "柱上变";
            row6["id"] = "52";
            row6["class"] = "Itop.TLPSP.DEVICE.UCDevicePWB";
            row6["ParentID"] = "-1";
            table.Rows.Add(row6);

            DataRow row60 = table.NewRow();
            row60["name"] = "开闭所";
            row60["id"] = "54";
            row60["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
            row60["ParentID"] = "-1";
            table.Rows.Add(row60);

            //DataRow row7 = table.NewRow();
            //row7["name"] = "开关站";
            //row7["id"] = "55";
            //row7["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
            //row7["ParentID"] = "-1";
            //table.Rows.Add(row7);
            DataRow row8 = table.NewRow();
            row8["name"] = "环网柜";
            row8["id"] = "56";
            row8["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
            row8["ParentID"] = "-1";
            table.Rows.Add(row8);
            DataRow row9 = table.NewRow();
            row9["name"] = "柱上开关";
            row9["id"] = "57";
            row9["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
            row9["ParentID"] = "-1";
            table.Rows.Add(row9);
            DataRow row10 = table.NewRow();
            row10["name"] = "电缆分支箱";
            row10["id"] = "58";
            row10["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
            row10["ParentID"] = "-1";
            table.Rows.Add(row10);
            //DataRow row11 = table.NewRow();
            //row11["name"] = "负荷开关";
            //row11["id"] = "59";
            //row11["class"] = "Itop.TLPSP.DEVICE.UCDevicePWKG";
            //row11["ParentID"] = "-1";
            //table.Rows.Add(row11);
            treeList1.DataSource = table;
           

        }
 
        #endregion

        #region 私有方法
        /// <summary>
        /// 实例化类接口
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        private UCDeviceBase createInstance(string classname) {
            return Assembly.GetExecutingAssembly().CreateInstance(classname, false) as UCDeviceBase;
        }
        private void showDevice(UCDeviceBase device) {
            if (device == null) return;
            device.Dock = DockStyle.Fill;
            splitContainerControl1.Panel2.Controls.Add(device);
        }
        #endregion
        private List<T> convertdirecttolist<T>(Dictionary<string, T> col)
        {
            List<T> ss = new List<T>();
            foreach (KeyValuePair<string, T> keyvalue in col)
            {
                ss.Add(keyvalue.Value);
            }
            return ss;
        }
        #region 记录操作
        protected override void Add() {
            //if (curDevice != null)
            //{

            //}
            TreeListNode node = treeList1.FocusedNode;
            if (node == null)
            {
                MessageBox.Show("请选择设备类型以后再进行检索", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                XtraSelectfrm selecfrm = new XtraSelectfrm();
                selecfrm.type = curDevice.GetType();
                selecfrm.ShowDialog();
                if (selecfrm.DialogResult == DialogResult.OK)
                {
                    if (curDevice is UCDeviceBDZ)
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND L1 ='" + selecfrm.devicevolt + "'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            DataTable datatable = new DataTable();
                            datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSP_Substation_Info>(selecfrm.vistsubstationflag), typeof(PSP_Substation_Info));
                            curDevice.gridControl1.DataSource = datatable;
                        }

                    }
                    else if (curDevice is UCDeviceDY)
                    {
                        if (selecfrm.getselecindex == 0)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND Title like'%" + selecfrm.DeviceName + "%'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 1)
                        {
                            curDevice.strCon = " AreaID = '" + Itop.Client.MIS.ProgUID + "' AND S1 ='" + selecfrm.devicevolt + "'";
                            curDevice.SelDevices();
                        }
                        else if (selecfrm.getselecindex == 2)
                        {
                            DataTable datatable = new DataTable();
                            datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSP_PowerSubstation_Info>(selecfrm.vistpowerflag), typeof(PSP_PowerSubstation_Info));
                            curDevice.gridControl1.DataSource = datatable;
                        }
                    }
                    else
                    {
                        if (selecfrm.getselecindex != 2)
                        {
                            //if (shortflag)
                            //{
                            //    GetDevice.strCon = ",psp_elcdevice where psp_elcdevice.devicesuid = pspdev.suid and psp_elcdevice.projectsuid = '" + ProjectSuid + "'" + selecfrm.Sqlcondition;
                            //}
                            //else
                            selecfrm._stype = curDevice.GetType();
                            curDevice.strCon = " where 1=1 " + selecfrm.Sqlcondition;
                            curDevice.SelDevices();
                        }
                        else
                        {
                            if (curDevice.GetType() == "01")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbusflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "05")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistlineflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "02")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans2flag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "03")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.visttrans3flag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "04")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfdjflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "06")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistdlqflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "09")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldrflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "10")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistcldkflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "11")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistbldkflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "12")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistfhflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "13")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistmlflag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                            else if (curDevice.GetType() == "14")
                            {
                                DataTable datatable = new DataTable();
                                datatable = Itop.Common.DataConverter.ToDataTable(convertdirecttolist<PSPDEV>(selecfrm.vistml2flag), typeof(PSPDEV));
                                curDevice.gridControl1.DataSource = datatable;
                            }
                        }

                    }

                }
            }
        }
        protected override void Edit() {
            if (curDevice != null)
                curDevice.Edit();
        }
        protected override void Del() {
            if (curDevice != null)
                curDevice.Delete();
        }
        protected override void Print() {
            try
            {
                DeviceHelper.ExportToExcelOld(curDevice.gridControl1, "", "");
            }
            catch { }
        }
        
        #endregion

        #region 字段
        UCDeviceBase curDevice;
        Dictionary<string, UCDeviceBase> devicTypes = new Dictionary<string, UCDeviceBase>();

        #endregion

        private void treeList1_MouseClick(object sender, MouseEventArgs e) {
            if (e.Button == MouseButtons.Right) return;
            TreeListNode node = treeList1.FocusedNode;
            if (node == null) return;
            string strID = node["id"].ToString();   
            string dtype = node["class"].ToString();
            string pid = node["ParentID"].ToString();
            string sname = " ";
            if (node.ParentNode != null)
            {
                 sname = node.ParentNode["name"].ToString();
            }
           
            if (dtype == "") return;
            UCDeviceBase device = null;
            if (devicTypes.ContainsKey(dtype)) {
                device = devicTypes[dtype];
                try {
                    device.Show();
                } catch { }
            }else
            {
                device = createInstance(dtype);
                device.ProjectID = Itop.Client.MIS.ProgUID;
                devicTypes.Add(dtype, device);
                showDevice(device);
            }
            
            if (curDevice != null　&& curDevice!=device) curDevice.Hide();
            curDevice = device;
            if (curDevice != null)
            {
                curDevice.ID = strID;
                if (dtype == "Itop.TLPSP.DEVICE.UCDeviceMX")
                {
                    curDevice.strCon = " where 1=1 and SvgUID='" + pid + "' and ";
                }
                else if (dtype=="Itop.TLPSP.DEVICE.UCDeviceCLDR" || dtype=="Itop.TLPSP.DEVICE.UCDeviceCLDK"  || dtype == "Itop.TLPSP.DEVICE.UCDeviceML" || dtype == "Itop.TLPSP.DEVICE.UCDeviceML2" || dtype == "Itop.TLPSP.DEVICE.UCDeviceBLDR" || dtype == "Itop.TLPSP.DEVICE.UCDeviceBLDK" || dtype == "Itop.TLPSP.DEVICE.UCDeviceXL" || dtype == "Itop.TLPSP.DEVICE.UCDeviceBYQ2" || dtype == "Itop.TLPSP.DEVICE.UCDeviceBYQ3" )
                {
                    curDevice.strCon = " where 1=1 and (IName='" + sname + "' or JName='" + sname + "') and ";
                }
                else if (dtype == "Itop.TLPSP.DEVICE.UCDeviceFDJ")
                {
                    curDevice.strCon = " where 1=1 and SubstationEleID='" + pid + "' and ";
                }
                else if (dtype == "Itop.TLPSP.DEVICE.UCDeviceDLQ")
                {
                    curDevice.strCon = " where 1=1 and HuganLine1='" + sname + "' and ";
                }
                else 
                {
                    curDevice.strCon = " where 1=1 and ";
                }
                curDevice.Init();
            }
        }

        private void barButtonItemOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                DeviceHelper.ExportToExcelOld(curDevice.gridControl1, "", "");
            }
            catch { }
        }
        
        private void barButtonItemIn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.treeList1.FocusedNode!=null)
            {
                IList<string> filedList = new List<string>();
                IList<string> capList = new List<string>();
                for (int i = 0; i < curDevice.gridView1.Columns.Count; i++)
                {
                    capList.Add(curDevice.gridView1.Columns[i].Caption);
                    filedList.Add(curDevice.gridView1.Columns[i].FieldName);
                }
                OpenFileDialog op = new OpenFileDialog();
                op.Filter = "Excel文件(*.xls)|*.xls";
                if (op.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        DataTable table = DeviceHelper.GetExcel(op.FileName, filedList, capList);
                        curDevice.UpdateIn(table);
                        
                    }
                    catch (Exception a) { MessageBox.Show(a.Message); }
                    curDevice.Init();
                }
            }
            else
                MessageBox.Show("没有选择设备（如果没有请添加一个）", "导入EXCEL", MessageBoxButtons.OK);
        }
        private void barButtonItemDel_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Del();
        }
        private void barButtonItemclose_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void AllDele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curDevice.Alldel();
        }
        private void UpdateNumber_ItemClick(object sender,DevExpress.XtraBars.ItemClickEventArgs e)
        {
            curDevice.UpdateNumber();
        }

        private void barImportPsasp_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            frmImport dlg = new frmImport();
            dlg.Show(this);
            
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           
        }

        
    }
}