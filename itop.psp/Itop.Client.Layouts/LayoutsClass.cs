using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;


namespace Itop.Client.Layouts
{
    public class LayoutsClass
    {
        public LayoutsClass()
        { 
        
        
        }





        public bool InsertExcel(string titleName, string unit, GridControl gridControl, TONLI.BZH.UI.DSOFramerWordControl wordcontrol)
        {
            try
            {
                System.Drawing.Font font = new System.Drawing.Font("宋体", 16);
                wordcontrol.DoInsert("\n\n"+titleName+"\n"  , font, TONLI.BZH.UI.WdParagraphAlignment.Center);
                //wordcontrol.DoPaste();

                font = new System.Drawing.Font("宋体", 12);
                wordcontrol.DoInsert(unit + "\n", font, TONLI.BZH.UI.WdParagraphAlignment.Right);
                //wordcontrol.DoPaste();

                wordcontrol.DoInsert("", font, TONLI.BZH.UI.WdParagraphAlignment.Center);
                string filename =System.Windows.Forms.Application.StartupPath + "\\BlogData\\Blog.xls";
                gridControl.DefaultView.ExportToExcelOld(filename);





                wordcontrol.DoInsertOleObject(filename);
                //System.Drawing.Font font = new System.Drawing.Font("宋体", 16);
                wordcontrol.DoInsert("", font, TONLI.BZH.UI.WdParagraphAlignment.Center);
                //Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();

                //Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(filename);

                //Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                //Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(1);// [System.Type.Missing];//.get.get_Item("xx");
                //ws.UsedRange.Select();
                //ws.UsedRange.Copy(System.Type.Missing);

                //wordcontrol.DoPaste();


                return true;
            }
            catch { return false; }
        }



        public bool InsertFpSpread(FarPoint.Win.Spread.FpSpread fp, int sheetcount, TONLI.BZH.UI.DSOFramerWordControl wordcontrol)
        {
            try
            {
                string filename = System.Windows.Forms.Application.StartupPath + "\\BlogData\\Blog.xls";
                fp.SaveExcel(filename);
                if (sheetcount == 0)
                    sheetcount = fp.Sheets.Count;
                //fp.ActiveSheetIndex = 0;
                //for (int j = 9; j > 0; j++)
                //{
                //    fp.Sheets[j].Dispose();
                
                //}
                //fp.Sheets[0].SaveTextFile(""

                //FarPoint.Win.Spread.FpSpread ft=fp;


                //for (int i = 0; i < sheetcount; i++)
                //{
                //    FarPoint.Win.Spread.FpSpread ft = fp;
                //    for (int j = sheetcount; j > i; j--)
                //    {
                //        ft.Sheets.RemoveAt(j);
                //    }
                //    ft.SaveExcel(filename);
                //    wordcontrol.DoInsertOleObject(filename);
                    
                
                
                //}








                //for (int j = 6; j > 1; j--)
                //{
                //    ft.Sheets.RemoveAt(j);
                //    ft.SaveExcel(filename);
                //    wordcontrol.DoInsertOleObject(filename);
                //}




                

                //string filename = System.Windows.Forms.Application.StartupPath + "\\BlogData\\Blog.xls";

                //string filename1 = System.Windows.Forms.Application.StartupPath + "\\BlogData\\Blog1.xls";
                //fp.SaveExcel(filename);

                //FarPoint.Win.Spread.FpSpread fs = new FarPoint.Win.Spread.FpSpread();
                //fs.Sheets[0].OpenExcel(filename, 1);
                //fs.SaveExcel(filename1);
                //wordcontrol.DoInsertOleObject(filename1);
                

                //FarPoint.Win.Spread.FpSpread fs = new FarPoint.Win.Spread.FpSpread();

                //for (int i = 0; i < sheetcount; i++)
                //{
                //    try
                //    {
                //        fs.Sheets.Clear();
                //        fs.Sheets.Add(fp.Sheets[i]);
                //        fs.SaveExcel(filename);

                //for (int i = 0; i < sheetcount; i++)
                //{
                //    fp.ActiveSheetIndex = i;
                //    MessageBox.Show("111");
                //    fp.SaveExcel(filename);
                //    MessageBox.Show("222");
                //    wordcontrol.DoInsertOleObject(filename);
                //    MessageBox.Show("333");
                //}
                    //}
                    //catch { }
                //}



                System.Drawing.Font font = new System.Drawing.Font("宋体", 12);


                Microsoft.Office.Interop.Excel.Application ep = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel._Workbook wb = ep.Workbooks.Add(filename);

                Microsoft.Office.Interop.Excel.Sheets sheets = wb.Worksheets;

                for (int i = 1; i <= sheetcount; i++)
                {

                    //wordcontrol.DoInsertOleObject(filename);
                    Microsoft.Office.Interop.Excel._Worksheet ws = (Microsoft.Office.Interop.Excel._Worksheet)sheets.get_Item(i);// [System.Type.Missing];//.get.get_Item("xx");
                   // ws.UsedRange.se.UsedRange.Select();
                    //cli


                    ws.UsedRange.Copy(System.Type.Missing);

                    wordcontrol.DoPaste();
                    wordcontrol.DoInsert(" ", font, TONLI.BZH.UI.WdParagraphAlignment.Left);
                    
                }
                Clipboard.Clear();
                ep.DisplayAlerts = false;
                ep.Quit();
                
                
                return true;
            }
            catch (Exception rc) { System.Windows.Forms.MessageBox.Show(rc.Message); return false; }
        }



        



    }
}
