using System.Collections;
using System.Data;
using Itop.Domain.Graphics;
using System;
namespace Itop.TLPSP.DEVICE.Mysql
{
    partial class frmImport
    {
        /// <summary>
        /// 并联电容
        /// </summary>
        /// <returns></returns>
        private IEnumerator importBLDR() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "09";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_bldr_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["ID_Name"].ToString();
                dev.Number = Convert.ToInt32(row[0]);
                dev.LineTQ = Convert.ToDouble(row["X1"]);
                dev.KSwitchStatus = row["Valid"].ToString() == "1" ? "0" : "1";
                dev.UnitFlag = row["Unit"].ToString();
                dev.IName = row["Node_Name"].ToString();

                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);

                } catch {
                    //UCDeviceBase.DataService.Update<PSPDEV>(dev); 
                }
                yield return ncurrent++;
            }
        }
        /// <summary>
        /// 负荷
        /// </summary>
        /// <returns></returns>
        private IEnumerator importFH() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "12";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_fh_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["ID_Name"].ToString();
                dev.Number = Convert.ToInt32(row[0]);
                dev.InPutP = Convert.ToDouble(row["Pl"]);
                dev.InPutQ = Convert.ToDouble(row["Ql"]);
                dev.VoltR = Convert.ToDouble(row["V0"]);
                dev.VoltV = Convert.ToDouble(row["Angle"]);

                dev.KSwitchStatus = row["Valid"].ToString() == "1" ? "0" : "1";
                dev.UnitFlag = row["Unit"].ToString();
                dev.IName = row["Node_Name"].ToString();

                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);

                } catch {

                }
                yield return ncurrent++;
            }
        }
        /// <summary>
        /// 发电机
        /// </summary>
        /// <returns></returns>
        private IEnumerator importFDJ() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "04";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_fdj_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["ID_Name"].ToString();
                dev.Number = Convert.ToInt32(row[0]);
                dev.OutP = Convert.ToDouble(row["Pg"]);
                dev.OutQ = Convert.ToDouble(row["Qg"]);
                dev.SjN = Convert.ToDouble(row["Xdp1"]);
                dev.SkN = Convert.ToDouble(row["Xdpp1"]);
                dev.Vimax = Convert.ToDouble(row["Pmax"]);
                dev.Vimin = Convert.ToDouble(row["Pmin"]);
                dev.Vjmax = Convert.ToDouble(row["Qmax"]);
                dev.Vjmin = Convert.ToDouble(row["Qmin"]);

                dev.UnitFlag = row["Unit"].ToString();
                dev.IName = row["Node_Name"].ToString();

                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);

                } catch {
                    //UCDeviceBase.DataService.Update<PSPDEV>(dev); 
                }
                yield return ncurrent++;
            }
        }
        /// <summary>
        /// 三圈变压器
        /// </summary>
        /// <returns></returns>
        private IEnumerator importBYQ3() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "03";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_byq3_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["ID_Name"].ToString();
                dev.Number = Convert.ToInt32(row[0]);
                //dev.LineR = Convert.ToDouble(row["R1"]);
                //dev.LineTQ = Convert.ToDouble(row["X1"]);
                //dev.LineGNDC = Convert.ToDouble(row["gm"]);
                //dev.Burthen = Convert.ToDecimal(row["Rate_MVA"]);
                dev.LineLevel = row["Conn_1"].ToString();
                dev.LineType = row["Conn_2"].ToString();
                dev.LineStatus = row["Conn_3"].ToString();
                dev.StandardCurrent = Convert.ToDouble(row["Tk2"]);
                dev.ZeroTQ = Convert.ToDouble(row["X3"]);
                //dev.BigTQ = Convert.ToDouble(row["R0"]);
                //dev.SmallTQ = Convert.ToDouble(row["X0"]);
                dev.HuganLine2 = row["Break_3"].ToString();
                dev.HuganLine3 = row["Break_1"].ToString();
                dev.HuganLine4 = row["Break_2"].ToString();
                dev.HuganTQ1 = Convert.ToDouble(row["R1"]);
                dev.HuganTQ2 = Convert.ToDouble(row["R2"]);
                dev.HuganTQ3 = Convert.ToDouble(row["R3"]);
                dev.HuganTQ4 = Convert.ToDouble(row["X1"]);
                dev.HuganTQ5 = Convert.ToDouble(row["X2"]);
                dev.BigP = Convert.ToDouble(row["Tk3"]);
                dev.K = Convert.ToDouble(row["Tk1"]);
                //dev.G = Convert.ToDouble(row["Bm"]);
                //dev.KName = row["Name_3"].ToString();
                dev.KSwitchStatus = row["Valid"].ToString() == "1" ? "0" : "1";
                dev.SiN = Convert.ToDouble(row["Rate_MVA1"]);
                dev.SjN = Convert.ToDouble(row["Rate_MVA2"]);
                dev.SkN = Convert.ToDouble(row["Rate_MVA3"]);
                dev.UnitFlag = row["Unit"].ToString();
                dev.IName = row["Name_1"].ToString();
                dev.JName = row["Name_2"].ToString();
                dev.KName = row["Name_3"].ToString();
                dev.ISwitch = row["Break_1"].ToString();
                dev.JSwitch = row["Break_2"].ToString();

                //dev.ZeroGNDC = Convert.ToDouble(row["B0_Half"]);

                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);

                } catch {
                    //UCDeviceBase.DataService.Update<PSPDEV>(dev); 
                }
                yield return ncurrent++;
            }
        }
        /// <summary>
        /// 两圈变压器
        /// </summary>
        /// <returns></returns>
        private IEnumerator importBYQ2() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "02";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_byq2_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["ID_Name"].ToString();
                dev.Number = Convert.ToInt32(row[0]);
                dev.LineR = Convert.ToDouble(row["R1"]);
                dev.LineTQ = Convert.ToDouble(row["X1"]);
                dev.LineGNDC = Convert.ToDouble(row["gm"]);
                dev.Burthen = Convert.ToDecimal(row["Rate_MVA"]);
                dev.LineLevel = row["I_Conn"].ToString();
                dev.LineType = row["J_Conn"].ToString();
                dev.BigTQ = Convert.ToDouble(row["R0"]);
                dev.SmallTQ = Convert.ToDouble(row["X0"]);
                dev.K = Convert.ToDouble(row["Tk"]);
                dev.G = Convert.ToDouble(row["Bm"]);
                //dev.KName = row["Name_3"].ToString();
                dev.KSwitchStatus = row["Valid"].ToString() == "1" ? "0" : "1";
                dev.UnitFlag = row["Unit"].ToString();
                dev.IName = row["I_Name"].ToString();
                dev.JName = row["J_Name"].ToString();
                //dev.ISwitch = row["I_Break"].ToString();
                //dev.JSwitch = row["J_Break"].ToString();
                //dev.ZeroGNDC = Convert.ToDouble(row["B0_Half"]);

                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);

                } catch {
                    //UCDeviceBase.DataService.Update<PSPDEV>(dev); 
                }
                yield return ncurrent++;
            }
        }
        /// <summary>
        /// 母线
        /// </summary>
        private IEnumerator importMX() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "01";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_mx_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["bus_name"].ToString();
                dev.Number = Convert.ToInt32(row[0]);
                Type t = row["SC3_MVA"].GetType();

                dev.Burthen = Convert.ToDecimal(row["SC3_MVA"]);

                dev.ReferenceVolt = (double)row["Base_kV"];
                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);
                    //break;
                } catch (Exception e) {
                    //UCDeviceBase.DataService.Update<PSPDEV>(dev); 
                }
                yield return ncurrent++;
            }
        }
        /// <summary>
        /// 线路
        /// </summary>
        private IEnumerator importXL() {
            DataTable data = gridControl1.DataSource as DataTable;
            PSPDEV dev = new PSPDEV();
            dev.ProjectID = Itop.Client.MIS.ProgUID;
            dev.Type = "05";
            foreach (DataRow row in data.Rows) {
                dev.SUID = "psasp_xl_" + dev.ProjectID.Substring(0, 4) + row[0].ToString();
                dev.Name = row["ID_Name"].ToString();
                dev.LineR = Convert.ToDouble(row["R1"]);
                dev.LineTQ = Convert.ToDouble(row["X1"]);
                dev.LineGNDC = Convert.ToDouble(row["B1_Half"]) * 2;
                dev.ZeroR = Convert.ToDouble(row["R0"]);
                dev.ZeroTQ = Convert.ToDouble(row["X0"]);
                dev.Number = Convert.ToInt32(row[0]);
                dev.KSwitchStatus = row["Valid"].ToString() == "1" ? "0" : "1";
                dev.UnitFlag = row["Unit"].ToString();
                dev.IName = row["I_Name"].ToString();
                dev.JName = row["J_Name"].ToString();
                dev.ISwitch = row["I_Break"].ToString();
                dev.LineType = row["type"].ToString();
                dev.JSwitch = row["J_Break"].ToString();
                dev.ZeroGNDC = Convert.ToDouble(row["B0_Half"]);
                dev.Burthen = Convert.ToDecimal(row["Rate_Ka"]);
                dev.LineLength = Convert.ToDouble(row["Length_Km"]);
                try {
                    UCDeviceBase.DataService.Create<PSPDEV>(dev);

                } catch {
                    //UCDeviceBase.DataService.Update<PSPDEV>(dev); 
                }
                yield return ncurrent++;
            }
        }
    }
}
