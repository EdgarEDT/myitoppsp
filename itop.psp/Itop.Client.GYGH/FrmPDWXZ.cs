using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using Itop.Domain.Table;
using Itop.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;

using System.Reflection;
using System.Diagnostics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using Itop.Client.Base;
using FarPoint.Win;
using Itop.Domain.Forecast;
using System.IO;
using Microsoft.Office.Interop.Excel;

namespace Itop.Client.GYGH
{
    public partial class FrmPDWXZ : FormBase
    {


        //创建表对象
        PDWXZ.Sheet31 sh31 = new Itop.Client.GYGH.PDWXZ.Sheet31();
        PDWXZ.Sheet32 sh32 = new Itop.Client.GYGH.PDWXZ.Sheet32();
        PDWXZ.Sheet33 sh33 = new Itop.Client.GYGH.PDWXZ.Sheet33();
        PDWXZ.Sheet33_1 sh33_1 = new Itop.Client.GYGH.PDWXZ.Sheet33_1();
        PDWXZ.Sheet34 sh34 = new Itop.Client.GYGH.PDWXZ.Sheet34();
        PDWXZ.Sheet34_2 sh34_2 = new Itop.Client.GYGH.PDWXZ.Sheet34_2();
        PDWXZ.Sheet35 sh35 = new Itop.Client.GYGH.PDWXZ.Sheet35();
        PDWXZ.Sheet36 sh36 = new Itop.Client.GYGH.PDWXZ.Sheet36();
        PDWXZ.Sheet37 sh37 = new Itop.Client.GYGH.PDWXZ.Sheet37();
        PDWXZ.Sheet37_3 sh37_3 = new Itop.Client.GYGH.PDWXZ.Sheet37_3();
        PDWXZ.Sheet38 sh38 = new Itop.Client.GYGH.PDWXZ.Sheet38();
        PDWXZ.Sheet38_8 sh38_8 = new Itop.Client.GYGH.PDWXZ.Sheet38_8();
        PDWXZ.Sheet39 sh39 = new Itop.Client.GYGH.PDWXZ.Sheet39();
        PDWXZ.Sheet39_6 sh39_6 = new Itop.Client.GYGH.PDWXZ.Sheet39_6();
        PDWXZ.Sheet310 sh310 = new Itop.Client.GYGH.PDWXZ.Sheet310();
        PDWXZ.Sheet310_5 sh310_5 = new Itop.Client.GYGH.PDWXZ.Sheet310_5();
        PDWXZ.Sheet311 sh311 = new Itop.Client.GYGH.PDWXZ.Sheet311();
        PDWXZ.Sheet311_9 sh311_9 = new Itop.Client.GYGH.PDWXZ.Sheet311_9();
        PDWXZ.Sheet312 sh312 = new Itop.Client.GYGH.PDWXZ.Sheet312();
        PDWXZ.Sheet312_10 sh312_10 = new Itop.Client.GYGH.PDWXZ.Sheet312_10();
        PDWXZ.Sheet313 sh313 = new Itop.Client.GYGH.PDWXZ.Sheet313();
        PDWXZ.Sheet313_11 sh313_11 = new Itop.Client.GYGH.PDWXZ.Sheet313_11();
        PDWXZ.Sheet314 sh314 = new Itop.Client.GYGH.PDWXZ.Sheet314();
        PDWXZ.Sheet314_12 sh314_12 = new Itop.Client.GYGH.PDWXZ.Sheet314_12();
        PDWXZ.Sheet315 sh315 = new Itop.Client.GYGH.PDWXZ.Sheet315();
        PDWXZ.Sheet315_14 sh315_14 = new Itop.Client.GYGH.PDWXZ.Sheet315_14();
        PDWXZ.Sheet316 sh316 = new Itop.Client.GYGH.PDWXZ.Sheet316();
        PDWXZ.Sheet316_16 sh316_16 = new Itop.Client.GYGH.PDWXZ.Sheet316_16();
        PDWXZ.Sheet317 sh317 = new Itop.Client.GYGH.PDWXZ.Sheet317();
        PDWXZ.Sheet317_17 sh317_17 = new Itop.Client.GYGH.PDWXZ.Sheet317_17();
        PDWXZ.Sheet318 sh318 = new Itop.Client.GYGH.PDWXZ.Sheet318();
        PDWXZ.Sheet318_19 sh318_19 = new Itop.Client.GYGH.PDWXZ.Sheet318_19();
        PDWXZ.Sheet319 sh319 = new Itop.Client.GYGH.PDWXZ.Sheet319();
        PDWXZ.Sheet319_20 sh319_20 = new Itop.Client.GYGH.PDWXZ.Sheet319_20();
        PDWXZ.Sheet320 sh320 = new Itop.Client.GYGH.PDWXZ.Sheet320();
        PDWXZ.Sheet320_21 sh320_21 = new Itop.Client.GYGH.PDWXZ.Sheet320_21();



        //工程号
        string ProjID = Itop.Client.MIS.ProgUID;
        //设现状的默认年份为当前年的前一年
        int year = DateTime.Now.Year - 1;

        //用自定义类创建对象
        fpcommon fc = new fpcommon();
        //存放市县固定名称编号及相应值
        List<string[]> SxXjName = new List<string[]>();
        //建两个哈希表，把地区名称和地区ID对应起来，因为在PSPDEV中无AreaName,只有AreaID
        Hashtable area_key_name = new Hashtable();
        Hashtable area_key_id = new Hashtable();
        
        private void Add_WH_AreaNameAndId()
        {
            //初始化哈希表
            string areaall = " ProjectID='" + ProjID + "'";
            IList<PS_Table_AreaWH> tempPTA = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", areaall);

            if (tempPTA.Count != 0)
            {
                for (int i = 0; i < tempPTA.Count; i++)
                {
                    area_key_name.Add(tempPTA[i].Title, tempPTA[i].ID);
                    area_key_id.Add(tempPTA[i].ID, tempPTA[i].Title);
                }
            }
        }
        private void Add_SxXJname()
        {
            //初始化市县地区名编号等
            string[] str1 ={ "1", "市辖供电区", "市辖供电区" };
            string[] str2 ={ "2", "县级供电区", "合计" };
            string[] str3 ={ "2.1", "其中：直供直管", "县级直供直管" };
            string[] str4 ={ "2.2", "控股", "县级控股" };
            string[] str5 ={ "2.3", "参股", "县级参股" };
            string[] str6 ={ "2.4", "代管", "县级代管" };
            string[] str7 ={ "3", "全地区", "合计" };
            SxXjName.Add(str1);
            SxXjName.Add(str2);
            SxXjName.Add(str3);
            SxXjName.Add(str4);
            SxXjName.Add(str5);
            SxXjName.Add(str6);
            SxXjName.Add(str7);
        }

        public FrmPDWXZ()
        {
            InitializeComponent();
        }

        private void FrmPDWXZ_Load(object sender, EventArgs e)
        {
            Add_SxXJname();
            Add_WH_AreaNameAndId();
            Add_Year();
            //根据窗口变化全部添满
            fpSpread1.Dock = System.Windows.Forms.DockStyle.Fill;
            //fpSpread_addsheet();
            //Firstadddata();
        }
        private void Add_Year()
        {
            //添加可选年份
            for (int i = 1970; i < 2026; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)BcobYear.Edit).Items.Add(i.ToString());
            }
            BcobYear.EditValue = year.ToString();
        }
        private void BcobYear_EditValueChanged(object sender, EventArgs e)
        {
            year = int.Parse(BcobYear.EditValue.ToString());
            //记录当前活动表的索引，完成后回复为当前表
            int activeindex = fpSpread1.ActiveSheetIndex;
            fpSpread_addsheet();
            //如果是第一次生运行activeindex=-1，不处理
            if (activeindex != -1)
            {
                fpSpread1.ActiveSheet = fpSpread1.Sheets[activeindex];
            }
        }
        private void fpSpread_addsheet()
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            try
            {
                //打开Excel表格
                //清空工作表
                fpSpread1.Sheets.Clear();
                fpSpread1.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\PDWXZ" + year + ".xls");
                fc.SpreadRemoveEmptyCells(fpSpread1);
                //保持格式
                sh31.CellType(fpSpread1.Sheets[0]);
                sh32.CellType(fpSpread1.Sheets[1]);
                sh33.CellType(fpSpread1.Sheets[2]);
                sh33_1.CellType(fpSpread1.Sheets[3]);
                sh37.CellType(fpSpread1.Sheets[8]);
                sh37_3.CellType(fpSpread1.Sheets[9]);
                sh314.CellType(fpSpread1.Sheets[22]);
                sh314_12.CellType(fpSpread1.Sheets[23]);
                sh317.CellType(fpSpread1.Sheets[28]);
                sh317_17.CellType(fpSpread1.Sheets[29]);
                sh318.CellType(fpSpread1.Sheets[30]);
                sh318_19.CellType(fpSpread1.Sheets[31]);
                sh320.CellType(fpSpread1.Sheets[34]);
                sh320_21.CellType(fpSpread1.Sheets[35]);
            }
            catch (System.Exception e)
            {
                //如果打开出错则重新生成并保存
                fpSpread1.Sheets.Clear();
                Firstadddata();
                //判断文件夹是否存在，不存在则创建
                if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
                {
                    Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
                }
                //保存excel文件
                fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\PDWXZ" + year + ".xls");
                //以下是打开文件设表格自动换行

                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\PDWXZ" + year + ".xls";
                workBook = excelApp.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
                    range.WrapText = true;
                    //保护工作表
                    ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                }
                //保存工作簿
                workBook.Save();
                //关闭工作簿
                excelApp.Workbooks.Close();

            }
            wait.Close();

        }
        private void Firstadddata()
        {
            //生成表3-1
            FarPoint.Win.Spread.SheetView Sheet31 = new FarPoint.Win.Spread.SheetView();
            //生成表3-2
            FarPoint.Win.Spread.SheetView Sheet32 = new FarPoint.Win.Spread.SheetView();
            //生成表3-3
            FarPoint.Win.Spread.SheetView Sheet33 = new FarPoint.Win.Spread.SheetView();
            //生成表3-3附表1
            FarPoint.Win.Spread.SheetView Sheet33_1 = new FarPoint.Win.Spread.SheetView();
            //生成表3-4
            FarPoint.Win.Spread.SheetView Sheet34 = new FarPoint.Win.Spread.SheetView();
            //生成表3-4附表2
            FarPoint.Win.Spread.SheetView Sheet34_2 = new FarPoint.Win.Spread.SheetView();
            //生成表3-5
            FarPoint.Win.Spread.SheetView Sheet35 = new FarPoint.Win.Spread.SheetView();
            //生成表3-6
            FarPoint.Win.Spread.SheetView Sheet36 = new FarPoint.Win.Spread.SheetView();
            //生成表3-7
            FarPoint.Win.Spread.SheetView Sheet37 = new FarPoint.Win.Spread.SheetView();
            //生成表3-7附表3
            FarPoint.Win.Spread.SheetView Sheet37_3 = new FarPoint.Win.Spread.SheetView();
            //生成表3-8
            FarPoint.Win.Spread.SheetView Sheet38 = new FarPoint.Win.Spread.SheetView();
            //生成表3-8附表8
            FarPoint.Win.Spread.SheetView Sheet38_8 = new FarPoint.Win.Spread.SheetView();
            //生成表3-9
            FarPoint.Win.Spread.SheetView Sheet39 = new FarPoint.Win.Spread.SheetView();
            //生成表3-9附表6
            FarPoint.Win.Spread.SheetView Sheet39_6 = new FarPoint.Win.Spread.SheetView();
            //生成表3-10
            FarPoint.Win.Spread.SheetView Sheet310 = new FarPoint.Win.Spread.SheetView();
            //生成表3-10附表5
            FarPoint.Win.Spread.SheetView Sheet310_5 = new FarPoint.Win.Spread.SheetView();
            //生成表3-11
            FarPoint.Win.Spread.SheetView Sheet311 = new FarPoint.Win.Spread.SheetView();
            //生成表3-11附表9
            FarPoint.Win.Spread.SheetView Sheet311_9 = new FarPoint.Win.Spread.SheetView();
            //生成表3-12
            FarPoint.Win.Spread.SheetView Sheet312 = new FarPoint.Win.Spread.SheetView();
            //生成表3-12附表10
            FarPoint.Win.Spread.SheetView Sheet312_10 = new FarPoint.Win.Spread.SheetView();
            //生成表3-13
            FarPoint.Win.Spread.SheetView Sheet313 = new FarPoint.Win.Spread.SheetView();
            //生成表3-13附表11
            FarPoint.Win.Spread.SheetView Sheet313_11 = new FarPoint.Win.Spread.SheetView();
            //生成表3-14
            FarPoint.Win.Spread.SheetView Sheet314 = new FarPoint.Win.Spread.SheetView();
            //生成表3-14附表12
            FarPoint.Win.Spread.SheetView Sheet314_12 = new FarPoint.Win.Spread.SheetView();
            //生成表3-15
            FarPoint.Win.Spread.SheetView Sheet315 = new FarPoint.Win.Spread.SheetView();
            //生成表3-15附表14
            FarPoint.Win.Spread.SheetView Sheet315_14 = new FarPoint.Win.Spread.SheetView();
            //生成表3-16
            FarPoint.Win.Spread.SheetView Sheet316 = new FarPoint.Win.Spread.SheetView();
            //生成表3-16附表16
            FarPoint.Win.Spread.SheetView Sheet316_16 = new FarPoint.Win.Spread.SheetView();
            //生成表3-17
            FarPoint.Win.Spread.SheetView Sheet317 = new FarPoint.Win.Spread.SheetView();
            //生成表3-17附表17
            FarPoint.Win.Spread.SheetView Sheet317_17 = new FarPoint.Win.Spread.SheetView();
            //生成表3-18
            FarPoint.Win.Spread.SheetView Sheet318 = new FarPoint.Win.Spread.SheetView();
            //生成表3-18附表19
            FarPoint.Win.Spread.SheetView Sheet318_19 = new FarPoint.Win.Spread.SheetView();
            //生成表3-19
            FarPoint.Win.Spread.SheetView Sheet319 = new FarPoint.Win.Spread.SheetView();
            //生成表3-19附表20
            FarPoint.Win.Spread.SheetView Sheet319_20 = new FarPoint.Win.Spread.SheetView();
            //生成表3-20
            FarPoint.Win.Spread.SheetView Sheet320 = new FarPoint.Win.Spread.SheetView();
            //生成表3-20附表21
            FarPoint.Win.Spread.SheetView Sheet320_21 = new FarPoint.Win.Spread.SheetView();

            //添加表3-1
            fpSpread1.Sheets.Add(Sheet31);
            //添加表3-2
            fpSpread1.Sheets.Add(Sheet32);
            //添加3-3
            fpSpread1.Sheets.Add(Sheet33);
            //添加表3-3附表1
            fpSpread1.Sheets.Add(Sheet33_1);
            //添加表3-4
            fpSpread1.Sheets.Add(Sheet34);
            //添加表3-4附表2
            fpSpread1.Sheets.Add(Sheet34_2);
            //添加表35
            fpSpread1.Sheets.Add(Sheet35);
            //添加表36
            fpSpread1.Sheets.Add(Sheet36);
            //添加表37
            fpSpread1.Sheets.Add(Sheet37);
            //添加表37附表3
            fpSpread1.Sheets.Add(Sheet37_3);
            //添加表38
            fpSpread1.Sheets.Add(Sheet38);
            //添加表38附表8
            fpSpread1.Sheets.Add(Sheet38_8);
            //添加表39
            fpSpread1.Sheets.Add(Sheet39);
            //添加表39附表6
            fpSpread1.Sheets.Add(Sheet39_6);
            //添加表310
            fpSpread1.Sheets.Add(Sheet310);
            //添加表310附表5
            fpSpread1.Sheets.Add(Sheet310_5);
            //添加表311
            fpSpread1.Sheets.Add(Sheet311);
            //添加表311附表9
            fpSpread1.Sheets.Add(Sheet311_9);
            //添加表312
            fpSpread1.Sheets.Add(Sheet312);
            //添加表312附表12
            fpSpread1.Sheets.Add(Sheet312_10);
            //添加表313
            fpSpread1.Sheets.Add(Sheet313);
            //添加表313附表11
            fpSpread1.Sheets.Add(Sheet313_11);
            //添加表314
            fpSpread1.Sheets.Add(Sheet314);
            //添加表314附表12
            fpSpread1.Sheets.Add(Sheet314_12);
            //添加表315
            fpSpread1.Sheets.Add(Sheet315);
            //添加表315附表14
            fpSpread1.Sheets.Add(Sheet315_14);
            //添加表316
            fpSpread1.Sheets.Add(Sheet316);
            //添加表316附表16
            fpSpread1.Sheets.Add(Sheet316_16);
            //添加表317
            fpSpread1.Sheets.Add(Sheet317);
            //添加表317附表17
            fpSpread1.Sheets.Add(Sheet317_17);
            //添加表318
            fpSpread1.Sheets.Add(Sheet318);
            //添加表318附表19
            fpSpread1.Sheets.Add(Sheet318_19);
            //添加表319
            fpSpread1.Sheets.Add(Sheet319);
            //添加表319附表20
            fpSpread1.Sheets.Add(Sheet319_20);
            //添加表320
            fpSpread1.Sheets.Add(Sheet320);
            //添加表320附表21
            fpSpread1.Sheets.Add(Sheet320_21);

             //创建表3-1
             sh31.Build(Sheet31, year, ProjID, SxXjName);
             //创建表3-2
             sh32.Build(Sheet32, year, ProjID, SxXjName);
             //创建表3-3
             sh33.Build(Sheet33, year, ProjID, SxXjName);
             //创建表3-3附表1
             sh33_1.Build(Sheet33_1, year, ProjID, area_key_id, SxXjName);
             //创建表3-4
             sh34.Build(Sheet34, year, ProjID, SxXjName);
             //创建表3-4附表2
             sh34_2.Build(Sheet34_2, year, ProjID, area_key_id, SxXjName);
             //创建表35
             sh35.Build(Sheet35, year, ProjID, SxXjName);
             //创建表36
             sh36.Build(Sheet36, year, ProjID, SxXjName);
             //创建表37
             sh37.Build(Sheet37, year, ProjID, SxXjName);
             //创建表37附表3
             sh37_3.Build(Sheet37_3, year, ProjID, area_key_id, SxXjName);
             //创建表38
             sh38.Build(Sheet38, year, ProjID, SxXjName);
             //创建表38附表8
             sh38_8.Build(Sheet38_8,year, ProjID,area_key_name, area_key_id, SxXjName);
             //创建表39
             sh39.Build(Sheet39, year, ProjID, SxXjName);
             //创建表39附表6
             sh39_6.Build(Sheet39_6, year, ProjID, area_key_id, SxXjName);
             //创建表310
             sh310.Build(Sheet310, year, ProjID, SxXjName);
             //创建表39附表6
             sh310_5.Build(Sheet310_5, year, ProjID, area_key_id, SxXjName);
             //创建表311
             sh311.Build(Sheet311, year, ProjID, SxXjName);
             //创建表311附表9
             sh311_9.Build(Sheet311_9, year, ProjID, area_key_id, SxXjName);
             //创建表312
             sh312.Build(Sheet312, year, ProjID, SxXjName);
             //创建表312附表10
             sh312_10.Build(Sheet312_10, year, ProjID, area_key_id, SxXjName);
             //创建表313
             sh313.Build(Sheet313, year, ProjID, SxXjName);
             //创建表313附表11
             sh313_11.Build(Sheet313_11, year, ProjID, area_key_id, SxXjName);
             //创建表314
             sh314.Build(Sheet314, year, ProjID, SxXjName);
             //创建表314附表12
             sh314_12.Build(Sheet314_12, year, ProjID, area_key_id, SxXjName);
             //创建表315
             sh315.Build(fpSpread1,Sheet315, year, ProjID, SxXjName);
             //创建表315附表14
             sh315_14.Build(Sheet315_14, year, ProjID, area_key_id, SxXjName);
             //创建表316
             sh316.Build(Sheet316, year, ProjID, SxXjName);
             //创建表316附表16
             sh316_16.Build(Sheet316_16, year, ProjID,area_key_id,SxXjName);
             //创建表317
             sh317.Build(Sheet317, year, ProjID, SxXjName);
             //创建表316附表16
             sh317_17.Build(Sheet317_17, year, ProjID, area_key_id, SxXjName);
             //创建表318
             sh318.Build(Sheet318, year, ProjID, SxXjName);
             //创建表316附表16
             sh318_19.Build(Sheet318_19, year, ProjID, area_key_id, SxXjName);
             //创建表319
             sh319.Build(Sheet319, year, ProjID, SxXjName);
             //创建表316附表16
             sh319_20.Build(Sheet319_20, year, ProjID, area_key_id, SxXjName);
             //创建表320
             sh320.Build(Sheet320, year, ProjID, SxXjName);
             //创建表320附表21
             sh320_21.Build(Sheet320_21, year, ProjID, area_key_id, SxXjName);
       

           fc.Sheet_Colautoenter(fpSpread1);
        }
        private void barBtnClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void barBtnDaochuExcel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                try
                {
                    fpSpread1.SaveExcel(fname);
                    //以下是打开文件设表格自动换行

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
                    range.WrapText = true;
                    //保护工作表
                    //ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                }
                //保存工作簿
                workBook.Save();
                //关闭工作簿
                excelApp.Workbooks.Close();
                if (MsgBox.ShowYesNo("导出成功，是否打开该文档？") != DialogResult.Yes)
                    return;

                System.Diagnostics.Process.Start(fname);
                
                }
                catch
                {
                    MsgBox.Show("无法保存" + fname + "。请用其他文件名保存文件，或将文件存至其他位置。");
                    return;
                }
            }
        }
        private void barBtnRefreshData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
          
            WaitDialogForm newwait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            //生成一个空表用来保存当前表
            FarPoint.Win.Spread.SheetView obj_sheet = null;
            //生成一个空表，行列值都设为0用来做为程序处理时的当前表，这样可以提高处理速度
            FarPoint.Win.Spread.SheetView activesheet = new FarPoint.Win.Spread.SheetView();
            activesheet.RowCount = 0;
            activesheet.ColumnCount = 0;
            //添加空表
            fpSpread1.Sheets.Add(activesheet);
            //保留当前表，以备程序结束后还原当前表
            obj_sheet = fpSpread1.ActiveSheet;
            //将空表设为当前表
            fpSpread1.ActiveSheet = activesheet;


            //更新表3-1
            sh31.Sheet_AddData(fpSpread1.Sheets[0], year, ProjID, SxXjName);
            sh31.CellType(fpSpread1.Sheets[0]);
            //更新表3-2
            sh32.Sheet_AddData(fpSpread1.Sheets[1], year, ProjID, SxXjName);
            sh32.CellType(fpSpread1.Sheets[1]);
            //更新表3-3全手写，无需更新
            //更新表3-3附表1 分区有可能变化，所以先存数据，重建表，再比对写回数据
            sh33_1.SaveData(fpSpread1.Sheets[3]);
            fpSpread1.Sheets[3].RowCount = 0;
            fpSpread1.Sheets[3].ColumnCount = 0;
            sh33_1.Build(fpSpread1.Sheets[3], year, ProjID, area_key_id, SxXjName);
            sh33_1.WriteData(fpSpread1.Sheets[3]);
            
            //更新表3-4
            fpSpread1.Sheets[4].RowCount = 0;
            fpSpread1.Sheets[4].ColumnCount = 0;
            sh34.Build(fpSpread1.Sheets[4], year, ProjID, SxXjName);
            //更新表3-4附表2
            fpSpread1.Sheets[5].RowCount = 0;
            fpSpread1.Sheets[5].ColumnCount = 0;
            sh34_2.Build(fpSpread1.Sheets[5], year, ProjID,area_key_id,SxXjName);
            //清除表格原有数据，这样写入速度快
            //更新表3-5
            fpSpread1.Sheets[6].RowCount = 0;
            fpSpread1.Sheets[6].ColumnCount = 0;
            sh35.Build(fpSpread1.Sheets[6],year,ProjID,SxXjName);
            //更新表3-6
            fpSpread1.Sheets[7].RowCount = 0;
            fpSpread1.Sheets[7].ColumnCount = 0;
            sh36.Build(fpSpread1.Sheets[7], year, ProjID, SxXjName);
            //更新表3-7 有手写数据，只更新动态数据
            sh37.Sheet_AddData(fpSpread1.Sheets[8],year,ProjID);
            //更新表3-7附表3 有手写，表格是动态
            sh37_3.SaveData(fpSpread1.Sheets[9]);
            fpSpread1.Sheets[9].RowCount = 0;
            fpSpread1.Sheets[9].ColumnCount = 0;
            sh37_3.Build(fpSpread1.Sheets[9],year,ProjID,area_key_id,SxXjName);
            sh37_3.WriteData(fpSpread1.Sheets[9]);
            //更新表3-8
            fpSpread1.Sheets[10].RowCount = 0;
            fpSpread1.Sheets[10].ColumnCount = 0;
            sh38.Build(fpSpread1.Sheets[10], year, ProjID, SxXjName);
            //更新表3-8附表8
            fpSpread1.Sheets[11].RowCount = 0;
            fpSpread1.Sheets[11].ColumnCount = 0;
            sh38_8.Build(fpSpread1.Sheets[11], year, ProjID,area_key_name, area_key_id, SxXjName);
            //更新表3-9
            fpSpread1.Sheets[12].RowCount = 0;
            fpSpread1.Sheets[12].ColumnCount = 0;
            sh39.Build(fpSpread1.Sheets[12], year, ProjID, SxXjName);
            //更新表3-9附表6
            fpSpread1.Sheets[13].RowCount = 0;
            fpSpread1.Sheets[13].ColumnCount = 0;
            sh39_6.Build(fpSpread1.Sheets[13], year, ProjID, area_key_id, SxXjName);
            //更新表3-10
            fpSpread1.Sheets[14].RowCount = 0;
            fpSpread1.Sheets[14].ColumnCount = 0;
            sh310.Build(fpSpread1.Sheets[14], year, ProjID, SxXjName);
            //更新表3-10附表5
            fpSpread1.Sheets[15].RowCount = 0;
            fpSpread1.Sheets[15].ColumnCount = 0;
            sh310_5.Build(fpSpread1.Sheets[15], year, ProjID, area_key_id, SxXjName);
            //更新表3-11
            fpSpread1.Sheets[16].RowCount = 0;
            fpSpread1.Sheets[16].ColumnCount = 0;
            sh311.Build(fpSpread1.Sheets[16], year, ProjID, SxXjName);
            //更新表3-11附表9
            fpSpread1.Sheets[17].RowCount = 0;
            fpSpread1.Sheets[17].ColumnCount = 0;
            sh311_9.Build(fpSpread1.Sheets[17], year, ProjID, area_key_id, SxXjName);
            //更新表3-12
            fpSpread1.Sheets[18].RowCount = 0;
            fpSpread1.Sheets[18].ColumnCount = 0;
            sh312.Build(fpSpread1.Sheets[18], year, ProjID, SxXjName);
            //更新表3-12附表10
            fpSpread1.Sheets[19].RowCount = 0;
            fpSpread1.Sheets[19].ColumnCount = 0;
            sh312_10.Build(fpSpread1.Sheets[19],year,ProjID,area_key_id,SxXjName);
            //更新表3-13
            fpSpread1.Sheets[20].RowCount = 0;
            fpSpread1.Sheets[20].ColumnCount = 0;
            sh313.Build(fpSpread1.Sheets[20], year, ProjID, SxXjName);
            //更新表3-13附表11
            fpSpread1.Sheets[21].RowCount = 0;
            fpSpread1.Sheets[21].ColumnCount = 0;
            sh313_11.Build(fpSpread1.Sheets[21], year, ProjID, area_key_id, SxXjName);
            //更新表3-14
            sh314.SaveData(fpSpread1.Sheets[22]);
            fpSpread1.Sheets[22].RowCount = 0;
            fpSpread1.Sheets[22].ColumnCount = 0;
            sh314.Build(fpSpread1.Sheets[22], year, ProjID, SxXjName);
            sh314.WriteData(fpSpread1.Sheets[22]);
            //更新表3-14附表12
            sh314_12.SaveData(fpSpread1.Sheets[23]);
            fpSpread1.Sheets[23].RowCount = 0;
            fpSpread1.Sheets[23].ColumnCount = 0;
            sh314_12.Build(fpSpread1.Sheets[23], year, ProjID, area_key_id, SxXjName);
            sh314_12.WriteData(fpSpread1.Sheets[23]);
            //更新表3-15
            fpSpread1.Sheets[24].RowCount = 0;
            fpSpread1.Sheets[24].ColumnCount = 0;
            sh315.Build(fpSpread1,fpSpread1.Sheets[24], year, ProjID, SxXjName);
            //更新表3-15附表14
            fpSpread1.Sheets[25].RowCount = 0;
            fpSpread1.Sheets[25].ColumnCount = 0;
            sh315_14.Build(fpSpread1.Sheets[25], year, ProjID, area_key_id, SxXjName);
            //更新表3-16
            fpSpread1.Sheets[26].RowCount = 0;
            fpSpread1.Sheets[26].ColumnCount = 0;
            sh316.Build(fpSpread1.Sheets[26], year, ProjID, SxXjName);
            //更新表3-16附表16
            fpSpread1.Sheets[27].RowCount = 0;
            fpSpread1.Sheets[27].ColumnCount = 0;
            sh316_16.Build(fpSpread1.Sheets[27], year, ProjID, area_key_id, SxXjName);
            //更新表3-17
            sh317.SaveData(fpSpread1.Sheets[28]);
            fpSpread1.Sheets[28].RowCount = 0;
            fpSpread1.Sheets[28].ColumnCount = 0;
            sh317.Build(fpSpread1.Sheets[28], year, ProjID, SxXjName);
            sh317.WriteData(fpSpread1.Sheets[28]);
            //更新表3-17附表17
            sh317_17.SaveData(fpSpread1.Sheets[29]);
            fpSpread1.Sheets[29].RowCount = 0;
            fpSpread1.Sheets[29].ColumnCount = 0;
            sh317_17.Build(fpSpread1.Sheets[29], year, ProjID, area_key_id, SxXjName);
            sh317_17.WriteData(fpSpread1.Sheets[29]);
            //更新表3-18全部手写，无电压等级，表格固定无需更新
            //表为fpSpread1.Sheets[30]
            //更新表3-18附表19(分区有可能更新，所以要保存原数据更新后写回)
            sh318_19.SaveData(fpSpread1.Sheets[31]);
            fpSpread1.Sheets[31].RowCount = 0;
            fpSpread1.Sheets[31].ColumnCount = 0;
            sh318_19.Build(fpSpread1.Sheets[31], year, ProjID, area_key_id, SxXjName);
            sh318_19.WriteData(fpSpread1.Sheets[31]);
            //更新表3-19
            fpSpread1.Sheets[32].RowCount = 0;
            fpSpread1.Sheets[32].ColumnCount = 0;
            sh319.Build(fpSpread1.Sheets[32], year, ProjID, SxXjName);
            //更新表3-19附表20
            fpSpread1.Sheets[33].RowCount = 0;
            fpSpread1.Sheets[33].ColumnCount = 0;
            sh319_20.Build(fpSpread1.Sheets[33], year, ProjID, area_key_id, SxXjName);
            //更新表3-20全部手写，无电压等级，表格固定无需更新
            //表为fpSpread1.Sheets[34]
            //更新表3-20附表21(分区有可能更新，所以要保存原数据更新后写回)
            sh320_21.SaveData(fpSpread1.Sheets[35]);
            fpSpread1.Sheets[35].RowCount = 0;
            fpSpread1.Sheets[35].ColumnCount = 0;
            sh320_21.Build(fpSpread1.Sheets[35], year, ProjID, area_key_id, SxXjName);
            sh320_21.WriteData(fpSpread1.Sheets[35]);
            

            //移除空表
            fpSpread1.Sheets.Remove(activesheet);
            //还原当前表
            fpSpread1.ActiveSheet = obj_sheet;
            //设文本自动换行
            fc.Sheet_Colautoenter(fpSpread1);
            newwait.Close();
            MessageBox.Show("更新数据完成！");

        }
        private void barBtnSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm wait = null;
            wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
            //判断文件夹xls是否存在，不存在则创建
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
            }
            try
            {
                //保存excel文件
                fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\PDWXZ"+year+".xls");
                //以下是打开文件设表格自动换行

                // 定义要使用的Excel 组件接口
                // 定义Application 对象,此对象表示整个Excel 程序
                Microsoft.Office.Interop.Excel.Application excelApp = null;
                // 定义Workbook对象,此对象代表工作薄
                Microsoft.Office.Interop.Excel.Workbook workBook;
                // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                Microsoft.Office.Interop.Excel.Worksheet ws = null;
                Microsoft.Office.Interop.Excel.Range range = null;
                excelApp = new Microsoft.Office.Interop.Excel.Application();
                string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\PDWXZ" + year + ".xls";
                workBook = excelApp.Workbooks.Open(filename, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
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
                    range.WrapText = true;
                    //保护工作表
                    ws.Protect(Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                }
                //保存工作簿
                workBook.Save();
                //关闭工作簿
                excelApp.Workbooks.Close();
                wait.Close();
                MsgBox.Show("保存成功");
            }
            catch (System.Exception ee)
            {
                wait.Close();
                MsgBox.Show("保存错误！确定您安装有Office Excel,或者关闭所有Excel文件重试");
            }
            

        }

       

    }
}