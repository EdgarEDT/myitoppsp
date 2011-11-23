using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraGrid;
using System.Windows.Forms;
using Itop.Common;
using System.IO;

namespace Itop.TLPSP.DEVICE
{
    public class FileClass
    {
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



    }
}
