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
        MX  =1,//ĸ��
        BYQ2=2,//�������ѹ��
        BYQ3=3,//�������ѹ��
        FDJ=4,//�����
        XL=5,//��·
        DLQ=6,//��·��
        KG=7,//����
        CLDR=8,//����������
        BLDR=9,//����������
        CLDK=10,//�����翹��
        BLDK=11,//�����翹��
        FH=12,//����
        ML=13,//1/2ĸ������
        ML2=14,//3/2ĸ������
        HG=15, //��·����
        PDS=50,
        XSB=51,
        ZSB=52,
        KBS=54,
        KGZ=55,
        HWG=56,
        ZSKG=57,
        FZX=58,
        GT=70
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
            column.Caption = "�豸����";
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
        public static void initprojectDeviceTypes(DevExpress.XtraTreeList.TreeList treeList1)
        {
            TreeListColumn column = new TreeListColumn();
            column.Caption = "�豸����";
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
    }
}
