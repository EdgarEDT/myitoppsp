using System;
using System.Collections.Generic;
using System.Text;
using Itop.Domain.Graphics;
using System.Windows.Forms;
using System.Data;
using System.IO;
using DevExpress.XtraGrid;
using Itop.Common;
using System.Xml;
using System.Collections;
using FarPoint.Win.Spread;
using System.Reflection;
namespace Itop.TLPSP.DEVICE
{
    /// <summary>
    /// 设备接口类，外部调用设备属性对话框使用此类。
    /// </summary>
    public class DeviceHelper
    {

        public static string uid = "";


        public static string layerid = "";


        public static string eleid = "";
        public static string bdzwhere = "";
        public static string xlwhere = "";
        public static XmlElement xml1;

        public static IList<glebeProperty> glist = null;
        public static string GetBdzNameByID(string bdzid)
        {
            //显示所在位置的名称
            object obj = DeviceHelper.GetDevice<PSP_Substation_Info>(bdzid);
            string name = string.Empty;
            if (obj != null)
            {
                name = ((PSP_Substation_Info)obj).Title;
            }
            else
            {
                obj = DeviceHelper.GetDevice<PSP_PowerSubstation_Info>(bdzid);
                if (obj != null)
                {
                    name = ((PSP_PowerSubstation_Info)obj).Title;
                }
            }
            return name;
        }
        /// <summary>
        /// 显示设备属性对话框
        /// </summary>
        /// <param name="type"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static bool ShowDeviceDlg(DeviceType type, string uid)
        {

            return ShowDeviceDlg(type, uid, false);
        }
        /// <summary>
        /// 显示设备属性对话框
        /// </summary>
        /// <param name="type"></param>
        /// <param name="uid"></param>
        /// <param name="isread">属性是否只读</param>
        /// <returns></returns>
        public static bool ShowDeviceDlg(DeviceType type, string uid, bool isread)
        {


            string stype = ((int)type).ToString("00");
            return ShowDeviceDlg(stype, uid, isread);
        }
        public static string StartYear = "";
        public static bool ShowDeviceDlg(string stype, string uid, bool isread)
        {
            object dev = null;

            if (stype == "20")
            {
                dev = GetDevice<PSP_Substation_Info>(uid);
            }
            else if (stype == "30")
            {

                dev = GetDevice<PSP_PowerSubstation_Info>(uid);
            }
            else
            {
                dev = GetDevice<PSPDEV>(uid);
            }
            if (dev == null) return false;
            switch (stype)
            {
                case "20"://变电站

                    frmBDZdlg dlg = new frmBDZdlg();
                    dlg.StartYear = StartYear;
                    dlg.DeviceMx = dev as PSP_Substation_Info;
                
                    dlg.IsRead = isread;
                    dlg.IsonlyRead = true;
                    if (dlg.ShowDialog() == DialogResult.OK && !isread)
                    {
                        UCDeviceBase.DataService.Update<PSP_Substation_Info>(dlg.DeviceMx);
                        dev = dlg.DeviceMx;
                    }
                    else
                    {
                        dev = null;
                    }
                    break;

                case "30"://电源
                    frmDYdlg dlg21 = new frmDYdlg();
                    dlg21.DeviceMx = dev as PSP_PowerSubstation_Info;
                    dlg21.IsRead = isread;
                    dlg21.IsonlyRead = true;
                    dlg21.StartYear = StartYear;
                    if (dlg21.ShowDialog() == DialogResult.OK && !isread)
                    {
                        UCDeviceBase.DataService.Update<PSP_PowerSubstation_Info>(dlg21.DeviceMx);
                        dev = dlg21.DeviceMx;

                    }
                    else
                    {
                        dev = null;
                    }
                    break;
                case "01":
                    frmMXdlg dlg1 = new frmMXdlg();
                    dlg1.DeviceMx = dev as PSPDEV;
                    
                    // dlg1.ShowDialog();
                    if (dlg1.ShowDialog() == DialogResult.OK && !isread)
                    {
                        UCDeviceBase.DataService.Update<PSPDEV>(dlg1.DeviceMx);
                    }
                    break;
                case "02":
                    frmBYQ2dlg dlg2 = new frmBYQ2dlg();
                    dlg2.DeviceMx = dev as PSPDEV;
                    dlg2.ShowDialog();
                    break;
                case "03":
                    frmBYQ3dlg dlg3 = new frmBYQ3dlg();
                    dlg3.DeviceMx = dev as PSPDEV;
                    dlg3.ShowDialog();
                    break;
                case "04":
                    frmFDJdlg dlg4 = new frmFDJdlg();
                    dlg4.DeviceMx = dev as PSPDEV;
                    dlg4.ShowDialog();
                    break;
                case "05":
                    frmXLdlg dlg5 = new frmXLdlg();
                    dlg5.DeviceMx = dev as PSPDEV;
                    dlg5.glist = glist;
                    dlg5.isread = true;
                    DialogResult rst = dlg5.ShowDialog();
                    if (rst == DialogResult.OK && !isread)
                    {
                        UCDeviceBase.DataService.Update<PSPDEV>(dlg5.DeviceMx);
                    }
                    break;
                case "06":
                    frmDLQdlg dlg6 = new frmDLQdlg();
                    dlg6.DeviceMx = dev as PSPDEV;
                    dlg6.ShowDialog();
                    break;
                case "07":
                    frmKGdlg dlg7 = new frmKGdlg();
                    dlg7.DeviceMx = dev as PSPDEV;
                    dlg7.ShowDialog();
                    break;
                case "08":
                    frmCLDRdlg dlg8 = new frmCLDRdlg();
                    dlg8.DeviceMx = dev as PSPDEV;
                    dlg8.ShowDialog();
                    break;
                case "09":
                    frmBLDRdlg dlg9 = new frmBLDRdlg();
                    dlg9.DeviceMx = dev as PSPDEV;
                    dlg9.ShowDialog();
                    break;
                case "10":
                    frmCLDKdlg dlg10 = new frmCLDKdlg();
                    dlg10.DeviceMx = dev as PSPDEV;
                    dlg10.ShowDialog();
                    break;
                case "11":
                    frmBLDKdlg dlg11 = new frmBLDKdlg();
                    dlg11.DeviceMx = dev as PSPDEV;
                    dlg11.ShowDialog();
                    break;
                case "12":
                    frmFHdlg dlg12 = new frmFHdlg();
                    dlg12.DeviceMx = dev as PSPDEV;
                    dlg12.ShowDialog();
                    break;
                case "13":
                    frmMLdlg dlg13 = new frmMLdlg();
                    dlg13.DeviceMx = dev as PSPDEV;
                    dlg13.ShowDialog();
                    break;
                case "14":
                    frmML2dlg dlg14 = new frmML2dlg();
                    dlg14.DeviceMx = dev as PSPDEV;
                    dlg14.ShowDialog();
                    break;
                case "15":
                    frmHGdlg dlg15 = new frmHGdlg();
                    dlg15.DeviceMx = dev as PSPDEV;
                    dlg15.ShowDialog();
                    break;
                case "50":
                case "51":
                case "52":
                    frmPWdlg dlg18 = new frmPWdlg();
                    dlg18.DeviceMx = dev as PSPDEV;
                    dlg18.ShowDialog();
                    break;
                case "54":
                case "56":
                case "57":
                case "58":
                case "59":
                    frmPWKGdlg dlg17 = new frmPWKGdlg();
                    dlg17.DeviceMx = dev as PSPDEV;
                    dlg17.ShowDialog();
                    break;
                case "61":
                case "62":
                case "63":
                case "64":
                case "65":
                    frmPWKGdlg dlg88 = new frmPWKGdlg();
                    dlg88.DeviceMx = dev as PSPDEV;
                    dlg88.ShowDialog();
                    break;
                case "70":
                    frmZXdlg dlg19 = new frmZXdlg();
                    dlg19.DeviceMx = dev as PSPDEV;
                    if (dlg19.ShowDialog() == DialogResult.OK && !isread)
                    {
                        UCDeviceBase.DataService.Update<PSPDEV>(dlg19.DeviceMx);
                    }
                    break;
                case "72":
                    frmBYQTWOdlg dlg20 = new frmBYQTWOdlg();
                    dlg20.DeviceMx = dev as PSPDEV;
                    dlg20.ShowDialog();
                    break;
                case "73":
                    frmDXdlg DX = new frmDXdlg();
                    DX.DeviceMx = dev as PSPDEV;
                    DX.isread = true;
                    DX.ShowDialog();
                    break;
                case "75":
                    frmLUXdlg LUX = new frmLUXdlg();
                    LUX.DeviceMx = dev as PSPDEV;
                    LUX.isread = true;
                   LUX.ShowDialog();
                    break;
                case "71":
                    frmRDQdlg dlg22 = new frmRDQdlg();
                    dlg22.DeviceMx = dev as PSPDEV;
                    dlg22.ShowDialog();
                    break;
                case "76":

                    break;
            }
            return dev != null;
        }
        public static object SelectDevice(string type)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.InitDeviceType(type);
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                return dlg.GetSelectedDevice()["device"];
            }
            return null;
        }

        public static bool pspflag = false;
        public static bool Wjghflag = false;
        public static string wjghuid = "";
        public static object SelectDevice(string type, string projectID)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.ProjectID = projectID;
            dlg.pspflag = pspflag;
            dlg.wjghflag = Wjghflag;
            dlg.wjghuid = wjghuid;
            dlg.subflag = subflag;
            dlg.InitDeviceType(type);
            dlg.BdzWhere = bdzwhere;
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                return dlg.GetSelectedDevice()["device"];
            }
            return null;
        }
        public static object SelectDevice(string type, string projectID, IList<object> list)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.ProjectID = projectID;
            dlg.pspflag = pspflag;
            dlg.wjghflag = Wjghflag;
            dlg.wjghuid = wjghuid;
            dlg.ListUID = list;
            dlg.InitDeviceType(type);
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                return dlg.GetSelectedDevice()["device"];
            }
            return null;
        }
        public static object SelectDevice(string projectID, params string[] type)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.ProjectID = projectID;
            dlg.pspflag = pspflag;
            dlg.wjghflag = Wjghflag;
            dlg.wjghuid = wjghuid;
            dlg.BdzWhere = bdzwhere;
            dlg.XlWhere = xlwhere;
            
            dlg.InitDeviceType(type);
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                return dlg.GetSelectedDevice()["device"];
            }
            return null;
        }
        public static bool subflag = true;
        public static DialogResult SelectDeviceDLG(string projectID, params string[] type)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.ProjectID = projectID;
            dlg.pspflag = pspflag;
            dlg.wjghflag = Wjghflag;
            dlg.wjghuid = wjghuid;
            dlg.BdzWhere = bdzwhere;
            dlg.XlWhere = xlwhere;
           
            dlg.InitDeviceType(type);
            return dlg.ShowDialog();

        }
        public static object SelectDevice(DeviceType type, string projectID)
        {
            return SelectDevice(((int)type).ToString("00"), projectID);
        }
        /// <summary>
        /// 获取设备对象
        /// </summary>
        /// <typeparam name="type"></typeparam>
        /// <param name="uid"></param>
        /// <returns></returns>
        //根据短路的元件类型 type项目projectSUID和卷项目projectid来获得这个项目的所有元件
        public static object SelectProjectDevice(string type, string projectSUID, string projectid, List<eleclass> selshortelement)
        {
            frmDeviceSelect dlg = new frmDeviceSelect();
            dlg.shortflag = true;
            dlg.ProjectSuid = projectSUID;
            dlg.shortselelement = selshortelement;
            dlg.ProjectID = projectid;
            dlg.InitDeviceType(type);
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                return dlg.GetSelectedDevice()["device"];
            }
            return null;
        }
        /// <summary>
        /// 获取设备对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static T GetDevice<T>(string uid)
        {

            return UCDeviceBase.DataService.GetOneByKey<T>(uid);
        }

        /// <summary>
        /// 导入EXCEL
        /// </summary>
        /// <param name="filepach"></param>
        /// <param name="filedList"></param>
        /// <param name="capList"></param>
        /// <returns></returns>
        public static DataTable GetExcel(string filepach, IList<string> filedList, IList<string> capList)
        {
            string str;
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();


            IList<string> fie = new List<string>();
            IList<int> yxzl = new List<int>();//有效的那几列

            int m = 1;

            for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(fpSpread1.Sheets[0].Cells[0, j].Text))
                {
                    fie.Add(filedList[capList.IndexOf(fpSpread1.Sheets[0].Cells[0, j].Text)]);
                    yxzl.Add(j);
                }
            }

            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            for (int i = m; i <= fpSpread1.Sheets[0].GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {

                DataRow dr = dt.NewRow();
               // for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                int n = 0;
                foreach (int j in yxzl)
                {
                    dr[fie[n]] = fpSpread1.Sheets[0].Cells[i, j].Text;
                    n++;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
       
        /// <summary>
        /// 导入EXCEL
        /// </summary>
        /// <param name="filepach"></param>
        /// <param name="filedList"></param>
        /// <param name="capList"></param>
        /// <returns></returns>
        public static DataTable GetExcelmainandchildren(string filepach, IList<string> filedList, IList<string> capList,string sheetname,DataTable dt1)
        {

            string str;
            FarPoint.Win.Spread.FpSpread fpSpread1 = new FarPoint.Win.Spread.FpSpread();

            try
            {
                fpSpread1.OpenExcel(filepach);
            }
            catch
            {
                string filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName(filepach);
                File.Copy(filepach, filepath1);
                fpSpread1.OpenExcel(filepath1);
                File.Delete(filepath1);
            }
            DataTable dt = new DataTable();


            IList<string> fie = new List<string>();
            IList<int> yxzl = new List<int>();//有效的那几列
            SheetView sheetview = fpSpread1.Sheets.Find(sheetname);
            if (sheetview==null)
            {
                return dt;
            }
            int m = 1;

            for (int j = 1; j < sheetview.GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
            {
                if (capList.Contains(sheetview.Cells[0, j].Text))
                {
                    fie.Add(filedList[capList.IndexOf(sheetview.Cells[0, j].Text)]);
                    yxzl.Add(j);
                }
            }
            
            for (int k = 0; k < fie.Count; k++)
            {
                dt.Columns.Add(fie[k]);
            }
            dt.Columns.Add("SvgUID");
            for (int i = m; i <= sheetview.GetLastNonEmptyRow(FarPoint.Win.Spread.NonEmptyItemFlag.Data); i++)
            {
                string datsourctitle = sheetview.Cells[i, 0].Text;
                DataRow dr = dt.NewRow();
                // for (int j = 0; j < fpSpread1.Sheets[0].GetLastNonEmptyColumn(FarPoint.Win.Spread.NonEmptyItemFlag.Data) + 1; j++)
                DataRow[] drsource = dt1.Select("Title='" + datsourctitle + "'");
                if (drsource.Length!=1)
                {
                    continue;
                }
                dr["SvgUID"] = drsource[0]["UID"];
                int n = 0;
                foreach (int j in yxzl)
                {
                    dr[fie[n]] = sheetview.Cells[i, j].Text;
                    n++;
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }

        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="title"></param>
        /// <param name="dw"></param>
        public static void ExportToExcelOld(GridControl gridControl, string title, string dw)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;
                try
                {
                    gridControl.ExportToExcelOld(fname);


                    FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                    fps.OpenExcel(fname);
                    SheetView sv = fps.Sheets[0];
                    //for (int j = 1; j < sv.NonEmptyColumnCount; j++)
                    //{
                    //    for (int k = 0; k < sv.NonEmptyRowCount; k++)
                    //    {
                    //        sv.Cells[k, j].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                    //    }

                    //}
                    fps.SaveExcel(fname);
                    // 定义要使用的Excel 组件接口
                    // 定义Application 对象,此对象表示整个Excel 程序
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // 定义Workbook对象,此对象代表工作薄
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();

                    workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    for (int i = 1; i <= workBook.Worksheets.Count; i++)
                    {

                        ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                        //取消保护工作表
                        ws.Unprotect(Missing.Value);
                        //有数据的行数
                        int row = ws.UsedRange.Rows.Count;
                        //有数据的列数
                        int col = ws.UsedRange.Columns.Count;
                        //创建一个区域
                        range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                        //设区域内的单元格自动换行
                        range.Select();
                        range.NumberFormatLocal = "G/通用格式";

                        //保护工作表
                        ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    //保存工作簿
                    workBook.Save();
                    //关闭工作簿
                    excelApp.Workbooks.Close();
                    // CreateTitle(fname, title, dw);
                    if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);

                }
                catch(Exception ee)
                {
                    MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                    return;
                }
            }
        }
        /// <summary>
        /// 导出EXCEL
        /// </summary>
        /// <param name="gridControl"></param>
        /// <param name="title"></param>
        /// <param name="dw"></param>
        public static void ExportToExcelandchildren(GridControl gridControl, string title, string dw)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;
                try
                {
                    gridControl.ExportToExcelOld(fname);


                    FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                    fps.OpenExcel(fname);
                    SheetView sv = fps.Sheets[0];
                    
                    //for (int j = 1; j < sv.NonEmptyColumnCount; j++)
                    //{
                    //    for (int k = 0; k < sv.NonEmptyRowCount; k++)
                    //    {
                    //        sv.Cells[k, j].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                    //    }

                    //}
                    //输出子表的数据
                    //输出母线
                    ExportChildEXCEL(gridControl.DataSource as DataTable,new UCDeviceMX(),fps,"母线","01");

                    //输出两绕组
                    ExportChildEXCEL(gridControl.DataSource as DataTable, new UCDeviceBYQ2(), fps, "两绕组变压器", "02");
                    //输出三绕组变压器
                    ExportChildEXCEL(gridControl.DataSource as DataTable, new UCDeviceBYQ3(), fps, "三绕组变压器", "03");
                    //输出负荷
                    ExportChildEXCEL(gridControl.DataSource as DataTable, new UCDeviceFH(), fps, "负荷", "12");
                    //输出发电机
                    ExportChildEXCEL(gridControl.DataSource as DataTable, new UCDeviceFDJ(), fps, "发电机", "04");

                    fps.SaveExcel(fname);
                    //// 定义要使用的Excel 组件接口
                    //// 定义Application 对象,此对象表示整个Excel 程序
                    //Microsoft.Office.Interop.Excel.Application excelApp = null;
                    //// 定义Workbook对象,此对象代表工作薄
                    //Microsoft.Office.Interop.Excel.Workbook workBook;
                    //// 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                    //Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    //Microsoft.Office.Interop.Excel.Range range = null;
                    //excelApp = new Microsoft.Office.Interop.Excel.Application();

                    //workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    //for (int i = 1; i <= workBook.Worksheets.Count; i++)
                    //{

                    //    ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                    //    //取消保护工作表
                    //    ws.Unprotect(Missing.Value);
                    //    //有数据的行数
                    //    int row = ws.UsedRange.Rows.Count;
                    //    //有数据的列数
                    //    int col = ws.UsedRange.Columns.Count;
                    //    //创建一个区域
                    //    range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                    //    //设区域内的单元格自动换行
                    //    range.Select();
                    //    range.NumberFormatLocal = "G/通用格式";

                    //    //保护工作表
                    //    ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    //}
                    ////保存工作簿
                    //workBook.Save();
                    ////关闭工作簿
                    //excelApp.Workbooks.Close();
                    // CreateTitle(fname, title, dw);
                    if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);

                }
                catch (Exception ee)
                {
                    MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                    return;
                }
            }
        }
        public static void ExportChildEXCEL(DataTable dt, UCDeviceBase ub,FarPoint.Win.Spread.FpSpread fpSpread1, string sheetname,string type)
        {
            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
           
          
           
            Dictionary<string, IList<PSPDEV>> bdzandchidren = new Dictionary<string, IList<PSPDEV>>();
            List<PSPDEV> zsjcol = new List<PSPDEV>();
            for (int i = 0; i < ub.gridView1.Columns.Count; i++)
            {

                capList.Add(ub.gridView1.Columns[i].Caption);
                filedList.Add(ub.gridView1.Columns[i].FieldName);
            }
            FarPoint.Win.Spread.SheetView sheetview = fpSpread1.Sheets.Find(sheetname);
            if (sheetview == null)
            {
                sheetview = new FarPoint.Win.Spread.SheetView();
                sheetview.SheetName = sheetname;
                fpSpread1.Sheets.Add(sheetview);
            }
            else
            {
                sheetview.RowCount = 0;
                sheetview.ColumnCount = 0;
            }
            sheetview.ColumnCount = capList.Count + 1;
            sheetview.RowCount = 1000;
           fpSpread1.ActiveSheet = sheetview;
            //读取数据
            int cn = 0;
            foreach (DataRow dr in dt.Rows)
            {
                cn++;
              string  strCon = " where 1=1 and SvgUID='" + dr["UID"].ToString() + "'and Type='"+type+"'";
              IList<PSPDEV> list1 = UCDeviceBase.DataService.GetList<PSPDEV>("SelectPSPDEVByCondition", strCon);


              if (!bdzandchidren.ContainsKey(dr["Title"].ToString()))
              {
                  bdzandchidren.Add(dr["Title"].ToString(), list1);
              }
              else
              {
                  bdzandchidren.Add(dr["Title"].ToString()+cn.ToString(), list1);
              }
            }
            //加表头
            sheetview.Cells[0, 0].Text = "变电站";
            for (int i = 0; i < capList.Count;i++ )
            {
                sheetview.Cells[0, i + 1].Text = capList[i];
            }
            int m = 1;
            //添加内容
            foreach (KeyValuePair<string,IList<PSPDEV>> kv in bdzandchidren )
            {
                foreach (PSPDEV pd in kv.Value)
                {
                    sheetview.Cells[m, 0].Text = kv.Key;
                    for (int i = 0; i < filedList.Count;i++ )
                    {
                        if (pd.GetType().GetProperty(filedList[i]).GetValue(pd, null)!=null)
                        {
                            sheetview.Cells[m, i + 1].Text = pd.GetType().GetProperty(filedList[i]).GetValue(pd, null).ToString();
                        }
                       
                    }
                    m++;
                }
            }
        }

        public static void CopyTemplate(Template_PSPDEV source,PSPDEV target)
        {
            Type t = typeof(PSPDEV);
            foreach (PropertyInfo p in t.GetProperties())
            {
                if (p.Name != "KName" && p.Name != "Name" && p.Name != "TemplateType" && p.Name != "AreaID" && p.Name != "SUID" && p.Name != "EleID" && p.Name != "SvgUID" && p.Name != "Type" && p.Name != "FirstNode" && p.Name != "LastNode" && p.Name != "IName" && p.Name != "JName" && p.Name != "KSwitchStatus" && p.Name != "Date1" && p.Name != "Date2" && p.Name != "OperationYear" && p.Name != "Number")
                {
                    if (source.GetType().GetProperty(p.Name)!=null)
                    {
                        p.SetValue(target, source.GetType().GetProperty(p.Name).GetValue(source, null), null);
                    }
                   
                }
               
            }
        }
    }
  public struct rresult
  {
      public PSPDEV deviceid;  //各段故障设备的节点
      public double gzl ;              //对应的故障率
      public double tysj ;          //每次故障对应的停运时间
      public double ntysj;    //年停运时间

  }
    public struct yjandjd
    {
        private PSPDEV yj;
        private PSPDEV firstjd;
        private PSPDEV lastjd;
        public yjandjd(PSPDEV _yj,PSPDEV _fir,PSPDEV _last)
        {
            yj = _yj;
            firstjd = _fir;
            lastjd = _last;
        }
        public PSPDEV YJ
        {
            get { return yj; }
            set { yj = value; }
        }
        public PSPDEV FirstNode
        {
            get { return firstjd; }
            set { firstjd = value; }
        }
        public PSPDEV LastNode
        {
            get { return lastjd; }
            set {lastjd = value; }
        }
    }
}
