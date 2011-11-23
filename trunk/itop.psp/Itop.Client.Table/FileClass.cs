using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using Itop.Common;
using System.IO;
using System.Data;
using System.Reflection;
using Itop.Domain.Stutistic;
using System.Collections;
using FarPoint.Win.Spread;
using Itop.Domain.Stutistics;

namespace Itop.Client.Table
{
    public class FileClass
    {

        public static string uid = "";


        public static void ExportExcel(GridControl gridControl)
        {
            //try
            //{
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                string fname = "";
                saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    fname = saveFileDialog1.FileName;
                    
                    //File.SetAttributes(fname, File.GetAttributes(fname) | FileAttributes.ReadOnly);

                    ////Create   the   file.   
                    //using (FileStream fs = new FileStream(fname, FileMode.OpenOrCreate, FileAccess.Read))
                    //{
                    //    if (!fs.CanWrite)
                    //    {
                    //        MsgBox.Show("�ļ����ܱ������������ļ��Ƿ񱻴�");
                    //        return;
                    //    }
                    //}  
                      //  try   
                      //{   
                      //      File.Move(fname,fname+"1");
                      //}   
                      //catch
                      //{
                      //    MsgBox.Show("�޷�����"+fname+"�����������ļ��������ļ������ļ���������λ�á�");
                      //    return;
                      //}   
                      //finally   
                      //{
                      //    File.Move(fname + "1",fname);
                      //}




                    try
                    {
                        gridControl.ExportToExcelOld(fname);
                       
                        FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                        fps.OpenExcel(fname);
                        SheetView sv = fps.Sheets[0];
                        for (int j = 0; j < sv.NonEmptyRowCount; j++)
                        {
                            for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                            {
                                FarPoint.Win.Spread.CellType.NumberCellType temptype=new FarPoint.Win.Spread.CellType.NumberCellType();
                                sv.Cells[j, k].CellType = temptype;
                  
                            }

                        }
                        fps.SaveExcel(fname);
                        // ����Ҫʹ�õ�Excel ����ӿ�
                        // ����Application ����,�˶����ʾ����Excel ����
                        Microsoft.Office.Interop.Excel.Application excelApp = null;
                        // ����Workbook����,�˶����������
                        Microsoft.Office.Interop.Excel.Workbook workBook;
                        // ����Worksheet ����,�˶����ʾExecel �е�һ�Ź�����
                        Microsoft.Office.Interop.Excel.Worksheet ws = null;
                        Microsoft.Office.Interop.Excel.Range range = null;
                        excelApp = new Microsoft.Office.Interop.Excel.Application();

                        workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                        for (int i = 1; i <= workBook.Worksheets.Count; i++)
                        {

                            ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                            //ȡ������������
                            ws.Unprotect(Missing.Value);
                            //�����ݵ�����
                            int row = ws.UsedRange.Rows.Count;
                            //�����ݵ�����
                            int col = ws.UsedRange.Columns.Count;
                            //����һ������
                            range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                            //�������ڵĵ�Ԫ���Զ�����
                            range.Select();
                            range.NumberFormatLocal="G/ͨ�ø�ʽ";
                          
                            //����������
                            ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                        }
                        //���湤����
                        workBook.Save();
                        //�رչ�����
                        excelApp.Workbooks.Close();


                      
                        if (MsgBox.ShowYesNo("�����ɹ����Ƿ�򿪸��ĵ���") != DialogResult.Yes)
                            return;
                    
                        System.Diagnostics.Process.Start(fname);
                    }
                    catch 
                    {
                        MsgBox.Show("�޷�����"+fname+"�����������ļ��������ļ������ļ���������λ�á�");
                        return;
                    }
                }


                //return true;
            //}
            //catch { }
        }


        
        
        /// <summary>
        /// ������վ�����
        /// </summary>
        /// <param name="DY">��ѹ</param>
        /// <param name="Number">����̨��</param>
        /// <param name="MVA">��̨����</param>
        /// <param name="LineLong">��·���ȣ�KM��</param>
        /// <param name="LineType">�����ͺ�</param>
        /// <param name="Flag">�Ƿ񸽴���·�����Ϣ����true����false</param>
        /// <returns>���ر��վ���</returns>
        public static double ComputerPowerMoney(int DY, double MVA, double LineLong, string LineType, bool Flag)
        {
            double sumvalue = 0;
            double sumvaluedata = 0;
            double sumvalueLine = 0;
            Project_Sum ps = new Project_Sum();

            //ps.T2 = OBJ.L2;
            //ps.T3 = OBJ.L13;
            //ps.T4 = OBJ.L14;
            ps.S5 = "2";
            ps.S1 = DY.ToString();
            Hashtable ha = new Hashtable();
            int t = 0;

            //ps.Type = OBJ.L7;
            IList<Project_Sum>  sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS1andS5", ps);

            foreach(Project_Sum ps1 in sum)
            {
                try
                {
                    int mva = (int)MVA;
                    int t5 = Convert.ToInt32(ps1.T5);
                    int ta = Convert.ToInt32(ps1.T1);
                    if (mva % (t5 * ta) == 0)
                    {
                        ha.Add(t5, ps1.Num);
                        if (t5 > t)
                            t = t5;

                        //sumvaluedata = ps1.Num;
                        //break;
                    }
                }
                catch { }
            }
            try
            {
                if (ha.Count > 0)
                    sumvaluedata = (double)ha[t];
            }
            catch { }



            if (Flag == true)//���վ�µ���·����·��Ϣ
            {
                Project_Sum pps = new Project_Sum();
                pps.S5 = "1";
                pps.S1 = DY.ToString();
                pps.L1 = LineType;
                ////ps.L2 = OBJ.L15;
                ////ps.L3 = OBJ.L16;
                IList<Project_Sum> summ = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", pps);
                if (summ.Count == 1)
                {
                    foreach (Project_Sum pp in summ)
                    {
                        if (pp.Num.ToString() == null || pp.Num.ToString() == "")
                            pp.Num = 0;

                        sumvalueLine = pp.Num * LineLong;
                    }
                }
            }

            return sumvalue = sumvaluedata + sumvalueLine;

        }








        /// <summary>
        /// ������վ�����
        /// </summary>
        /// <param name="DY">��ѹ</param>
        /// <param name="Number">����̨��</param>
        /// <param name="MVA">��̨����</param>
        /// <param name="LineLong">��·���ȣ�KM��</param>
        /// <param name="LineType">�����ͺ�</param>
        /// <param name="Flag">�Ƿ񸽴���·�����Ϣ����true����false</param>
        /// <returns>���ر��վ���</returns>
        public static double ComputerPowerMoney(int DY, int Number, string MVA, double LineLong, string LineType, bool Flag)
        {
            double sumvalue = 0;
            double sumvaluedata = 0;
            double sumvalueLine = 0;
                    Project_Sum ps = new Project_Sum();

                    //ps.T2 = OBJ.L2;
            //ps.T3 = OBJ.L13;
                    //ps.T4 = OBJ.L14;
                    ps.S5 = "2";
                    ps.S1 = DY.ToString();
                    ps.T5 = MVA.ToString();
                    ps.T1 = Number.ToString();
                    //ps.Type = OBJ.L7;

                        Project_Sum sum = (Project_Sum)Common.Services.BaseService.GetObject("SelectProject_SumByvalue3", ps);

                        if (sum != null)
                        {
                            // sumvaluedata = sum.Num*Number/int.Parse(sum.T1);
                            sumvaluedata = sum.Num;

                        }
      

           if (Flag==true)//���վ�µ���·����·��Ϣ
            {
                Project_Sum pps = new Project_Sum();
                pps.S5 = "1";
                pps.S1 = DY.ToString();
                pps.L1 = LineType;
                ////ps.L2 = OBJ.L15;
                ////ps.L3 = OBJ.L16;

                    IList<Project_Sum> summ = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", pps);
                    if (summ.Count == 1)
                    {
                        foreach (Project_Sum pp in summ)
                        {
                            if (pp.Num.ToString() == null || pp.Num.ToString() == "")
                                pp.Num = 0;

                            sumvalueLine = pp.Num * LineLong;
                        }
                    }
         
            }

          return sumvalue = sumvaluedata + sumvalueLine;
 
        }
        /// <summary>
        /// ������·�����
        /// </summary>
        /// <param name="DY">��ѹ</param>
        /// <param name="LineLong">��·���ȣ�KM��</param>
        /// <param name="LineType">�����ͺ�</param>
        /// <returns>������·���</returns>
        public static double ComputerLineMoney(int DY, double LineLong, string LineType)
        {
          
            double sumvalueLine = 0;

                Project_Sum ps = new Project_Sum();
                ps.S5 = "1";
                ps.S1 = DY.ToString();
                ps.L1 = LineType;    
                    IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByLinevalue2", ps);
                    if (sum.Count == 1)
                    {
                        foreach (Project_Sum pp in sum)
                        {
                            if (pp.Num.ToString() == null || pp.Num.ToString() == "")
                                pp.Num = 0;

                            sumvalueLine = pp.Num * LineLong;
                        }
                    }
            return sumvalueLine;

        }


        public static void ExportExcel(string title, string dw, GridControl gc)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fname = saveFileDialog1.FileName;
                    gc.ExportToExcelOld(fname);
                    FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                    fps.OpenExcel(fname);
                    SheetView sv = fps.Sheets[0];

                    int ColumnCount = sv.NonEmptyColumnCount;
                    int RowCount = sv.NonEmptyRowCount;


                    //sv.ColumnCount = ColumnCount;
                    //sv.RowCount = RowCount;

                    sv.AddRows(0, 2);
                    sv.Cells[0, 0].Text = title;
                    sv.Cells[0, 0].Font = new System.Drawing.Font("����", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[0, 0].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sv.Cells[0, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[0, 0].Row.Height = 50;
                    sv.Cells[0, 0].ColumnSpan = ColumnCount;


                    sv.Cells[1, 0].Text = dw;
                    sv.Cells[1, 0].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[1, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
                    sv.Cells[1, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[1, 0].ColumnSpan = ColumnCount;

                    for (int i = 0; i < ColumnCount; i++)
                    {
                        sv.Cells[2, i].Row.Height = 40;
                        sv.Cells[2, i].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }

                    sv.AddRows(RowCount + 2, 2);
                    sv.Cells[RowCount + 2, 0].Text = "����ʱ��:" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    sv.Cells[RowCount + 2, 0].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[RowCount + 2, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
                    sv.Cells[RowCount + 2, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[RowCount + 2, 0].ColumnSpan = ColumnCount;
                    for (int j = 0; j < sv.NonEmptyRowCount; j++)
                    {
                        for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                        {
                            sv.Cells[j, k].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                        }

                    }
                    fps.SaveExcel(fname);
                    // ����Ҫʹ�õ�Excel ����ӿ�
                    // ����Application ����,�˶����ʾ����Excel ����
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // ����Workbook����,�˶����������
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // ����Worksheet ����,�˶����ʾExecel �е�һ�Ź�����
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();

                    workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    for (int i = 1; i <= workBook.Worksheets.Count; i++)
                    {

                        ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                        //ȡ������������
                        ws.Unprotect(Missing.Value);
                        //�����ݵ�����
                        int row = ws.UsedRange.Rows.Count;
                        //�����ݵ�����
                        int col = ws.UsedRange.Columns.Count;
                        //����һ������
                        range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                        //�������ڵĵ�Ԫ���Զ�����
                        range.Select();
                        range.NumberFormatLocal = "G/ͨ�ø�ʽ";

                        //����������
                        ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    //���湤����
                    workBook.Save();
                    //�رչ�����
                    excelApp.Workbooks.Close();

                    if (MsgBox.ShowYesNo("�����ɹ����Ƿ�򿪸��ĵ���") != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);
                }
                catch
                {
                    MsgBox.Show("�޷�����" + fname + "�����������ļ��������ļ������ļ���������λ�á�");
                    return;
                }
            }

        }
        public static void ExportToExcelOld(string title, string dw, GridControl gc)
        {

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    fname = saveFileDialog1.FileName;
                    gc.ExportToExcelOld(fname);
                    FarPoint.Win.Spread.FpSpread fps = new FarPoint.Win.Spread.FpSpread();
                    fps.OpenExcel(fname);
                    SheetView sv = fps.Sheets[0];

                    int ColumnCount = sv.NonEmptyColumnCount;
                    int RowCount = sv.NonEmptyRowCount;


                    //sv.ColumnCount = ColumnCount;
                    //sv.RowCount = RowCount;

                    sv.AddRows(0, 2);
                    sv.Cells[0, 0].Text = title;
                    sv.Cells[0, 0].Font = new System.Drawing.Font("����", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[0, 0].HorizontalAlignment = CellHorizontalAlignment.Center;
                    sv.Cells[0, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[0, 0].Row.Height = 50;
                    sv.Cells[0, 0].ColumnSpan = ColumnCount;


                    sv.Cells[1, 0].Text = dw;
                    sv.Cells[1, 0].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[1, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
                    sv.Cells[1, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[1, 0].ColumnSpan = ColumnCount;

                    for (int i = 0; i < ColumnCount; i++)
                    {
                        sv.Cells[2, i].Row.Height = 40;
                        sv.Cells[2, i].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }

                    sv.AddRows(RowCount + 2, 2);
                    sv.Cells[RowCount + 2, 0].Text = "����ʱ��:" + DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + DateTime.Now.Day;
                    sv.Cells[RowCount + 2, 0].Font = new System.Drawing.Font("����", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    sv.Cells[RowCount + 2, 0].HorizontalAlignment = CellHorizontalAlignment.Right;
                    sv.Cells[RowCount + 2, 0].VerticalAlignment = CellVerticalAlignment.Center;
                    sv.Cells[RowCount + 2, 0].ColumnSpan = ColumnCount;
                    sv.SetColumnVisible(ColumnCount - 1, false);
                    sv.SetColumnVisible(ColumnCount - 2, false);
                    for (int j = 0; j < sv.NonEmptyRowCount; j++)
                    {
                        for (int k = 0; k < sv.NonEmptyColumnCount; k++)
                        {
                            sv.Cells[j, k].CellType = new FarPoint.Win.Spread.CellType.NumberCellType();
                        }

                    }
                    fps.SaveExcel(fname);
                    // ����Ҫʹ�õ�Excel ����ӿ�
                    // ����Application ����,�˶����ʾ����Excel ����
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // ����Workbook����,�˶����������
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // ����Worksheet ����,�˶����ʾExecel �е�һ�Ź�����
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();

                    workBook = excelApp.Workbooks.Open(fname, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                    for (int i = 1; i <= workBook.Worksheets.Count; i++)
                    {

                        ws = (Microsoft.Office.Interop.Excel.Worksheet)workBook.Worksheets[i];
                        //ȡ������������
                        ws.Unprotect(Missing.Value);
                        //�����ݵ�����
                        int row = ws.UsedRange.Rows.Count;
                        //�����ݵ�����
                        int col = ws.UsedRange.Columns.Count;
                        //����һ������
                        range = ws.get_Range(ws.Cells[1, 1], ws.Cells[row, col]);
                        //�������ڵĵ�Ԫ���Զ�����
                        range.Select();
                        range.NumberFormatLocal = "G/ͨ�ø�ʽ";

                        //����������
                        ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                    }
                    //���湤����
                    workBook.Save();
                    //�رչ�����
                    excelApp.Workbooks.Close();

                    if (MsgBox.ShowYesNo("�����ɹ����Ƿ�򿪸��ĵ���") != DialogResult.Yes)
                        return;

                    System.Diagnostics.Process.Start(fname);
                }
                catch
                {
                    MsgBox.Show("�޷�����" + fname + "�����������ļ��������ļ������ļ���������λ�á�");
                    return;
                }
            }

        }
    }

}
