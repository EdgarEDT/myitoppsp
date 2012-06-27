using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Reflection;
using System.IO;
using System.Xml;
using DevExpress.XtraTreeList.Columns;

namespace Itop.TLPSP.DEVICE
{
    public enum DeviceType
    {
        BDZ =20,
        DY  =30,
        MX  =1,//母线
        BYQ2=2,//两绕组变压器
        BYQ3=3,//三绕组变压器
        FDJ=4,//发电机
        XL=5,//线路
        DLQ=6,//断路器
        KG=7,//开关
        CLDR=8,//串联电容器
        BLDR=9,//并联电容器
        CLDK=10,//串联电抗器
        BLDK=11,//并联电抗器
        FH=12,//负荷
        ML=13,//1/2母联开关
        ML2=14,//3/2母联开关
        HG=15, //线路互感
        PDS=50,
        XSB=51,
        ZSB=52,
        KBS=54,
        KGZ=55,
        HWG=56,
        ZSKG=57,
        FZX=58,
        GT=70,
        PDXL=73,
        LUX=75
    }
   
    public class DeviceTypeHelper{
        public static DataTable GetDeviceTypes() {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes= xml.GetElementsByTagName("device");
            DataTable table =new DataTable();
            table.Columns.Add("id",typeof(string));
            table.Columns.Add("name",typeof(string));
            table.Columns.Add("class",typeof(string));
            table.Columns.Add("parentid", typeof(string));
            foreach (XmlNode node in nodes) {
                if (node.Attributes["visible"].Value=="true")
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    row["parentid"] = node.Attributes["parentid"].Value;
                    table.Rows.Add(row);
                }
               
            }
            return table;
        }
        public static DataTable GetDeviceTypes(string[] type)
        {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            table.Columns.Add("parentid", typeof(string));
            foreach (XmlNode node in nodes)
            {
                for (int i = 0; i < type.Length;i++ )
                {
                    if (node.Attributes["id"].Value == type[i])
                    {

                        DataRow row = table.NewRow();
                        row["id"] = node.Attributes["id"].Value;
                        row["name"] = node.Attributes["name"].Value;
                        row["class"] = node.Attributes["class"].Value;
                        row["parentid"] = node.Attributes["parentid"].Value;
                        table.Rows.Add(row);
                    }
                }
               

            }
            return table;
        }

        public static string DeviceClassbyType(string type)
        {
            switch(type)
            {
                case"01":
                    return "Itop.TLPSP.DEVICE.UCDeviceMX";
                case"73":
                    return"Itop.TLPSP.DEVICE.UCDeviceDX";
                case"74":
                    return "Itop.TLPSP.DEVICE.UCDeviceFZX";
                case "70":
                    return "Itop.TLPSP.DEVICE.UCDeviceZX";
                case"06":
                    return "Itop.TLPSP.DEVICE.UCDeviceDLQ";
                case"55":
                    return "Itop.TLPSP.DEVICE.UCDevicePWKG";
                 case"80":
                    return "Itop.TLPSP.DEVICE.UCDevicePWFHZL";
                 case"75":
                    return "Itop.TLPSP.DEVICE.UCDeviceLUX";
                default:
                    return "Itop.TLPSP.DEVICE.UCDeviceMX";

            }
        }
        public static void InitDeviceTypes(DevExpress.XtraTreeList.TreeList treeList1) {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            treeList1.KeyFieldName = "id";
            treeList1.ParentFieldName = "parentid";
            treeList1.DataSource = DeviceTypeHelper.GetDeviceTypes();
        }
        public static void InitDeviceTypes(DevExpress.XtraTreeList.TreeList treeList1,string[] type)
        {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            treeList1.KeyFieldName = "id";
            treeList1.ParentFieldName = "parentid";
            treeList1.DataSource = DeviceTypeHelper.GetDeviceTypes(type);
        }
        public static void initprojectDeviceTypes(DevExpress.XtraTreeList.TreeList treeList1)
        {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            treeList1.KeyFieldName = "id";
            treeList1.ParentFieldName = "parentid";
            treeList1.DataSource = DeviceTypeHelper.GetprojectDeviceTypes();
        }
        public static void initprojectDeviceTypes_SH(DevExpress.XtraTreeList.TreeList treeList1)
        {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            treeList1.KeyFieldName = "keyid";
            treeList1.ParentFieldName = "parentid";
            treeList1.DataSource = DeviceTypeHelper.GetprojectDeviceTypes_SH();
        }
        public static void initprojectDeviceTypes_SH1(DevExpress.XtraTreeList.TreeList treeList1)
        {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "设备分类";
            column.FieldName = "name";
            column.VisibleIndex = 0;
            column.Width = 180;
            column.OptionsColumn.AllowEdit = false;
            column.OptionsColumn.AllowSort = false;
            treeList1.Columns.AddRange(new TreeListColumn[] {
            column});
            treeList1.KeyFieldName = "keyid";
            treeList1.ParentFieldName = "parentid";
            treeList1.DataSource = DeviceTypeHelper.GetprojectDeviceTypes_SH1();
        }

        public static DataTable GetprojectDeviceTypes()
        {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.devicetypes.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            table.Columns.Add("parentid", typeof(string));
            foreach (XmlNode node in nodes)
            {
                if (Convert.ToInt32(node.Attributes["id"].Value)<40)
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    row["parentid"] = node.Attributes["parentid"].Value;
                    table.Rows.Add(row);
                }
               
            }
            return table;
        }
        public static DataTable GetprojectDeviceTypes_SH()
        {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.projectdevicetypes.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            table.Columns.Add("parentid", typeof(string));
            table.Columns.Add("keyid", typeof(string));
            foreach (XmlNode node in nodes)
            {
                if (Convert.ToInt32(node.Attributes["id"].Value) < 40 )
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    row["parentid"] = node.Attributes["parentid"].Value;
                    row["keyid"] = node.Attributes["keyid"].Value;
                    table.Rows.Add(row);
                }

            }
            return table;
        }
        public static DataTable GetprojectDeviceTypes_SH1()
        {
            Stream fs = Assembly.GetExecutingAssembly().GetManifestResourceStream("Itop.TLPSP.DEVICE.projectdevicetypes.xml");
            XmlDocument xml = new XmlDocument();
            xml.Load(fs);
            XmlNodeList nodes = xml.GetElementsByTagName("device");
            DataTable table = new DataTable();
            table.Columns.Add("id", typeof(string));
            table.Columns.Add("name", typeof(string));
            table.Columns.Add("class", typeof(string));
            table.Columns.Add("parentid", typeof(string));
            table.Columns.Add("keyid", typeof(string));
            foreach (XmlNode node in nodes)
            {
                if (Convert.ToInt32(node.Attributes["id"].Value) < 40 && Convert.ToInt32(node.Attributes["id"].Value) !=12 && (string.IsNullOrEmpty(node.Attributes["parentid"].Value) || Convert.ToInt32(node.Attributes["id"].Value) >= 6))
                {
                    DataRow row = table.NewRow();
                    row["id"] = node.Attributes["id"].Value;
                    row["name"] = node.Attributes["name"].Value;
                    row["class"] = node.Attributes["class"].Value;
                    row["parentid"] = node.Attributes["parentid"].Value;
                    row["keyid"] = node.Attributes["keyid"].Value;
                    table.Rows.Add(row);
                }

            }
            return table;
        }
    }
}
