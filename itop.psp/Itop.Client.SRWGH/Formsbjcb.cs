using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.IO;
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
//using Itop.Domain.RightManager;
using Itop.Client.Base;
using FarPoint.Win;
using Itop.Domain.Forecast;
using Itop.Domain.HistoryValue;
using Itop.Domain.Chen;
namespace Itop.Client.SRWGH
{
    public partial class Formsbjcb : FormBase
    {
        System.IO.MemoryStream si1 = new System.IO.MemoryStream();
        System.IO.MemoryStream si2 = new System.IO.MemoryStream();
        byte[] by1 = null;
        byte[] bts = null;
        int firstyear = 2000;
        int endyear = 2008;
        string uid1 = "";
        string type1 = "1";
        int type = 1;
        DataTable dataTable = new DataTable();
        private FarPoint.Win.Spread.CellHorizontalAlignment HAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
        private FarPoint.Win.Spread.CellVerticalAlignment VAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
        FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
        FarPoint.Win.Spread.CellType.NumberCellType numberCellTypes1 = new FarPoint.Win.Spread.CellType.NumberCellType();
      
        FarPoint.Win.Spread.CellType.PercentCellType percentcelltypes = new FarPoint.Win.Spread.CellType.PercentCellType();
        FarPoint.Win.Spread.CellType.TextCellType texttype = new FarPoint.Win.Spread.CellType.TextCellType();

        //定义一个边框线
        LineBorder border = new LineBorder(Color.Black);
        public FarPoint.Win.Spread.FpSpread FPSpread
        {
            get { return fpSpread1; }

        }

        public Formsbjcb()
        {
            InitializeComponent();
           
        }

        private void Formsbjcb_Load(object sender, EventArgs e)
        {
            numberCellTypes1.DecimalPlaces = 3;
            //打开模板
            Econ ed = new Econ();
            ed.UID = "SBJCB";

            bts = Itop.Client.Common.Services.BaseService.GetOneByKey<Econ>(ed).ExcelData;
            initdata();
            //增加一列数据
            //for (int i = 0; i < 2;i++ )
            //{
            //    fpSpread1.Sheets[0].Columns.Add(2, 1);
            //    fpSpread1.Sheets[0].Columns[2+i].Border = new FarPoint.Win.ComplexBorder(null, null, null, bottomborder);
            //}
         
            //添加单元格式
           // FarPoint.Win.ComplexBorderSide bottomborder = new FarPoint.Win.ComplexBorderSide(true, Color.Black, 1, System.Drawing.Drawing2D.DashStyle.Solid, null, new Single[] { 0f, 0.33f, 0.66f, 1f });
            
            //文字居中

        }
        private void SpreadRemoveEmptyCells(FarPoint.Win.Spread.FpSpread tempspread)
        {
            //本方法用于去掉多表格中多余的行和列（空）
            //定义无空单元格模式
            FarPoint.Win.Spread.Model.INonEmptyCells nec;
            //计算spread有多少个表格
            int sheetscount = tempspread.Sheets.Count;
            //定义行数
            int rowcount = 0;
            //定义列数
            int colcount = 0;
            for (int m = 0; m < sheetscount; m++)
            {
                nec = (FarPoint.Win.Spread.Model.INonEmptyCells)tempspread.Sheets[m].Models.Data;
                //计算无空单元格列数
                colcount = nec.NonEmptyColumnCount;
                //计算无空单元格行数
                rowcount = nec.NonEmptyRowCount;
                tempspread.Sheets[m].RowCount = rowcount;
                tempspread.Sheets[m].ColumnCount = colcount;
            }
        }
        private void initdata()
        {
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls"))
            {
              WaitDialogForm  wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                fpSpread1.Sheets.Clear();
                //fpSpread1.Open(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xml");
                fpSpread1.OpenExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls");
                SpreadRemoveEmptyCells(fpSpread1);
                wait.Close();

            }
            else
            {
                WaitDialogForm wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                refreshdata();
                wait.Close();

            }
            //string uid = "Remark='" + ProjectUID + "'";
            //EconomyAnalysis ec = (EconomyAnalysis)Services.BaseService.GetObject("SelectEconomyAnalysisByvalue", uid);
           
            //if (ec != null)
            //{
            //    WaitDialogForm wait = null;
            //    try
            //    {
            //        wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
            //        System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.Contents);
            //        by1 = ec.Contents;
            //        fpSpread1.Open(ms);

            //        try
            //        {
            //            // fpSpread1.Sheets[0].Cells[0, 0].Text = "附表1 " + treeList1.FocusedNode["Title"].ToString() + "基础数据";
            //        }
            //        catch { }

            //        wait.Close();

            //    }
            //    catch { wait.Close(); }
            //}
            //else
            //{
            //    ec=new EconomyAnalysis();
            //    ec.Contents = bts;
            //    //obj.ParentID = uid;
            //    ec.CreateDate = DateTime.Now;
            //    ec.Remark = ProjectUID;
            //    Services.BaseService.Create<EconomyAnalysis>(ec);
            //    System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.Contents);
            //    fpSpread1.Open(ms);
            //}
        }

        //更新输变电情况
        private void resbdsb()
        {
#region 根据电压等级获取数据 线路和变电站数据 按照排序来获得
            IList<Substation_Info> list1000s = new List<Substation_Info>();
            IList<PSPDEV> list1000l = new List<PSPDEV>();
            IList<Substation_Info> list750s = new List<Substation_Info>();
            IList<PSPDEV> list750l = new List<PSPDEV>();
            IList<Substation_Info> list500s = new List<Substation_Info>();
            IList<PSPDEV> list500l = new List<PSPDEV>();
            IList<Substation_Info> list330s = new List<Substation_Info>();
            IList<PSPDEV> list330l = new List<PSPDEV>();
            IList<Substation_Info> list220s = new List<Substation_Info>();
            IList<PSPDEV> list220l = new List<PSPDEV>();
            IList<Substation_Info> list110s = new List<Substation_Info>();
            IList<PSPDEV> list110l = new List<PSPDEV>();
            //配网数据统计
            IList<Substation_Info> listcity66s = new List<Substation_Info>();
           // IList<Substation_Info> listcountry66s = new List<Substation_Info>();
            IList<PSPDEV> listcity66l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry66l = new List<PSPDEV>();

            IList<Substation_Info> listcity35s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry35s = new List<Substation_Info>();
            IList<PSPDEV> listcity35l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry35l = new List<PSPDEV>();

            IList<Substation_Info> listcity10s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry10s = new List<Substation_Info>();
            IList<PSPDEV> listcity10l = new List<PSPDEV>();
           // IList<PSPDEV> listcountry10l = new List<PSPDEV>();

            IList<Substation_Info> listcity6s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry6s = new List<Substation_Info>();
            IList<PSPDEV> listcity6l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry6l = new List<PSPDEV>();

            IList<Substation_Info> listcity3s = new List<Substation_Info>();
            //IList<Substation_Info> listcountry3s = new List<Substation_Info>();
            IList<PSPDEV> listcity3l = new List<PSPDEV>();
            //IList<PSPDEV> listcountry3l = new List<PSPDEV>();

            string con = "AreaID='" + ProjectUID+ "'and L1=1000 order by L2,AreaName,CreateDate";
            list1000s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=750 order by L2,AreaName,CreateDate";
            list750s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=500 order by L2,AreaName,CreateDate";
            list500s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=330 order by L2,AreaName,CreateDate";
            list330s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=220 order by L2,AreaName,CreateDate";
            list220s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            con = "AreaID='" + ProjectUID + "'and L1=110 order by L2,AreaName,CreateDate";
            list110s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
           
            con = "AreaID='" + ProjectUID + "'and L1=66 and DQ='城网'order by L2,AreaName,CreateDate";
            listcity66s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and L1=66 and DQ='农网'order by L2,AreaName,CreateDate";
            //listcountry66s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='城网'and L1=35 order by L2,AreaName,CreateDate";
            listcity35s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='农网'and L1=35 order by L2,AreaName,CreateDate";
           // listcountry35s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='城网'and L1=10 order by L2,AreaName,CreateDate";
            listcity10s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='农网'and L1=10 order by L2,AreaName,CreateDate";
            //listcountry10s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='城网' and L1=6 order by L2,AreaName,CreateDate";
            listcity6s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='农网' and L1=6 order by L2,AreaName,CreateDate";
            //listcountry6s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);

            con = "AreaID='" + ProjectUID + "'and DQ='城网'and L1=3 order by L2,AreaName,CreateDate";
            listcity3s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //con = "AreaID='" + ProjectUID + "'and DQ='农网'and L1=3 order by L2,AreaName,CreateDate";
            //listcountry3s = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
            //线路信息  
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='1000'  ORDER BY LineLength";
            list1000l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition",con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='750'  ORDER BY LineLength";
            list750l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='500'  ORDER BY LineLength";
            list500l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='330'  ORDER BY LineLength";
            list330l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='220'  ORDER BY LineLength";
            list220l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='110'  ORDER BY LineLength";
            list110l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='66'AND DQ='城网'ORDER BY LineLength";
            listcity66l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='66'AND DQ='农网'ORDER BY LineLength";
            //listcountry66l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='35' AND DQ='城网' ORDER BY LineLength";
            listcity35l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='35' AND DQ='农网' ORDER BY LineLength";
            //listcountry35l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='10'AND DQ='城网'  ORDER BY LineLength";
            listcity10l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='10'AND DQ='农网'  ORDER BY LineLength";
           // listcountry10l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='6'AND DQ='城网'  ORDER BY LineLength";
            listcity6l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='6'AND DQ='农网'  ORDER BY LineLength";
            //listcountry6l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='3'AND DQ='城网'  ORDER BY LineLength";
            listcity3l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
            //con = "where ProjectID='" + ProjectUID + "'AND Type = '5'AND RateVolt='3'AND DQ='农网'  ORDER BY LineLength";
           // listcountry3l = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);

            //先获取区域名称　根据区域和农网类型搜索线路和变电站　将其放入容器中进行统计
　           Dictionary<string,IList<PSPDEV>> listcountry66l=new Dictionary<string,IList<PSPDEV>>();
             Dictionary<string,IList<Substation_Info>>listcountry66s=new Dictionary<string,IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry35l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry35s = new Dictionary<string, IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry10l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry10s = new Dictionary<string, IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry6l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry6s = new Dictionary<string, IList<Substation_Info>>();

             Dictionary<string, IList<PSPDEV>> listcountry3l = new Dictionary<string, IList<PSPDEV>>();
             Dictionary<string, IList<Substation_Info>> listcountry3s = new Dictionary<string, IList<Substation_Info>>();

            bool flag66l=false,flag66s=false,flag35l=false,flag35s=false,flag10l=false,flag10s=false,flag6l=false,flag6s=false,flag3l=false,flag3s=false;
            string conn = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list =Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            
            foreach (PS_Table_AreaWH area in list)
            {
                con = "where ProjectID='" + ProjectUID + "'AND AreaID='"+area.ID+"'AND Type = '5'AND RateVolt='66'AND DQ='农网'  ORDER BY LineLength";
                IList<PSPDEV> list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count!=0)
                {
                    flag66l=true;
                    listcountry66l.Add(area.Title, list1);
                }
                
                con = "AreaID='" + ProjectUID + "'and AreaName='"+area.Title+"'and DQ='农网' and L1=66 order by L2,AreaName,CreateDate";
                IList<Substation_Info>  list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count!=0)
                {
                    flag66s=true;
                    listcountry66s.Add(area.Title, list2);

                }
                
                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='35'AND DQ='农网'  ORDER BY LineLength";
               list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
               if (list1.Count != 0)
                {
                    flag35l=true;
                    listcountry35l.Add(area.Title, list1);
                }
                
                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ='农网' and L1=35 order by L2,AreaName,CreateDate";
                list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag35s=true;
                    listcountry35s.Add(area.Title, list2);
                }
                

                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='10'AND DQ='农网'  ORDER BY LineLength";
                list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    flag10l=true;
                    listcountry10l.Add(area.Title, list1);
                }
                
                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ='农网' and L1=10 order by L2,AreaName,CreateDate";
                list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag10s=true;
                    listcountry10s.Add(area.Title, list2);
                }
                

                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='6'AND DQ='农网'  ORDER BY LineLength";
                list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    flag6l=true;
                     listcountry6l.Add(area.Title, list1);
                }
               
                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ='农网' and L1=6 order by L2,AreaName,CreateDate";
                list2 = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag6s=true;
                    listcountry6s.Add(area.Title, list2);
                }
               

                con = "where ProjectID='" + ProjectUID + "'AND AreaID='" + area.ID + "'AND Type = '5'AND RateVolt='3'AND DQ='农网'  ORDER BY LineLength";
                list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    flag3l=true;
                     listcountry3l.Add(area.Title, list1);
                }
               
                con = "AreaID='" + ProjectUID + "'and AreaName='" + area.Title + "'and DQ='农网' and L1=3 order by L2,AreaName,CreateDate";
                list2= Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                if (list2.Count != 0)
                {
                    flag3s=true;
                     listcountry3s.Add(area.Title, list2);
                }
               
               // repositoryItemComboBox3.Items.Add(area.Title);
            }

#endregion
#region 根据数据添加行和添加单元格式
            List<string> title=new List<string>();
            int list1000i = 0;
            //1000kv电网
            if (list1000s.Count!=0||list1000l.Count!=0)
            {
                list1000i = 1;
                fpSpread1.Sheets[1].Rows.Add(3, 1);
                fpSpread1.Sheets[1].SetValue(3,0, "1000KV电网");
                fpSpread1.Sheets[1].Rows[3].Font = new Font("宋体",9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list1000l.Count; i++)
                {
                    fpSpread1.Sheets[1].Rows.Add(4, 1);
                    fpSpread1.Sheets[1].SetValue(4, 0, list1000l[list1000l.Count-1-i].Name);
                    fpSpread1.Sheets[1].SetValue(4, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4, 3, list1000l[list1000l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4,4, list1000l[list1000l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4, 4].CellType = numberCellTypes1;
                    list1000i++;
                }
                fpSpread1.Sheets[1].Cells[3, 4].Formula = "SUM(R5C5:R" + (5 + list1000l.Count - 1).ToString() + "C5)";
                for (int i = 0; i < list1000s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4+list1000l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 0, list1000s[list1000s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 1, list1000s[list1000s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 2, list1000s[list1000s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000l.Count, 2].CellType = numberCellTypes1;
                    list1000i++;
                }
                fpSpread1.Sheets[1].Cells[3, 1].Formula = "SUM(R"+(5+list1000l.Count).ToString()+"C2:R" + (5 + list1000l.Count+list1000s.Count-1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3, 2].Formula = "SUM(R" + (5 + list1000l.Count).ToString() + "C3:R" + (5 + list1000l.Count + list1000s.Count - 1).ToString() + "C3)";
            }
            //750KV电网
            int list750i = 0;
            if (list750s.Count!=0||list750l.Count!=0)
            {
                list750i = 1;
                fpSpread1.Sheets[1].Rows.Add(3+list1000i, 1);
                fpSpread1.Sheets[1].SetValue(3+list1000i, 0, "750KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list750l.Count; i++)
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 0, list750l[list750l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 3, list750l[list750l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i, 4, list750l[list750l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i, 4].CellType = numberCellTypes1;
                    list750i++;
                }
                fpSpread1.Sheets[1].Cells[3+list1000i, 4].Formula = "SUM(R"+(5+list1000i).ToString()+"C5:R" + (5+list1000i + list750l.Count - 1).ToString() + "C5)";
                for (int i = 0; i < list750s.Count; i++)
                {
                    fpSpread1.Sheets[1].Rows.Add(4 +list1000i+ list750l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 0, list750s[list750s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 1, list750s[list750s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 2, list750s[list750s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750l.Count, 2].CellType = numberCellTypes1;
                    list750i++;
                }
                fpSpread1.Sheets[1].Cells[3+list1000i, 1].Formula = "SUM(R" + (5 +list1000i+ list750l.Count).ToString() + "C2:R" + (5+list1000i + list750l.Count + list750s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3+list1000i, 2].Formula = "SUM(R" + (5+list1000i + list750l.Count).ToString() + "C3:R" + (5+list1000i + list750l.Count + list750s.Count - 1).ToString() + "C3)";
            }
            //500KV电网
            int list500i = 0;
            if (list500l.Count!=0||list500s.Count!=0)
            {
                list500i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i+list750i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i+list750i, 0, "500KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i+list750i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i+list750i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list500l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i+list750i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 0, list500l[list500l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i+list750i, 3, list500l[list500l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i, 4, list500l[list500l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i, 4].CellType = numberCellTypes1;
                    list500i++;  
                }
                for (int i = 0; i < list500s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i+list750i + list500l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 0, list500s[list500s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 1, list500s[list500s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 2, list500s[list500s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500l.Count, 2].CellType = numberCellTypes1;
                    list500i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i+list750i, 1].Formula = "SUM(R" + (5 + list1000i+list750i + list500l.Count).ToString() + "C2:R" + (5 + list1000i +list750i+ list500l.Count + list500s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i+list750i, 2].Formula = "SUM(R" + (5 + list1000i+list750i + list500l.Count).ToString() + "C3:R" + (5 + list1000i+list750i + list500l.Count + list500s.Count - 1).ToString() + "C3)";
            }
            //330KV电网
            int list330i = 0;
            if (list330l.Count!=0||list330s.Count!=0)
            {
                list330i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i+list500i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i+list500i, 0, "330KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i+list500i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i+list500i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list330l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i+list500i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i+list500i, 0, list330l[list330l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i+list500i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i+list500i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i, 3, list330l[list330l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i, 4, list330l[list330l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i+list500i, 4].CellType = numberCellTypes1;
                    list330i++;  
                }
                for (int i = 0; i < list330s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i+list500i + list330l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i +list500i+ list330l.Count, 0, list330s[list330s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 1, list330s[list330s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 2, list330s[list330s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330l.Count, 2].CellType = numberCellTypes1;
                    list330i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i, 1].Formula = "SUM(R" + (5 + list1000i + list750i+list500i + list330l.Count).ToString() + "C2:R" + (5 + list1000i + list750i +list500i+ list330l.Count + list330s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i, 2].Formula = "SUM(R" + (5 + list1000i + list750i+list500i + list330l.Count).ToString() + "C3:R" + (5 + list1000i + list750i+list500i + list330l.Count + list330s.Count - 1).ToString() + "C3)";
            }
            //220KV电网
            int list220i = 0;
            if (list220l.Count!=0||list220s.Count!=0)
            {
                list220i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i+list330i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i, 0, "220KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list220l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i+list330i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 0, list220l[list220l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 3, list220l[list220l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i, 4, list220l[list220l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i+list330i, 4].CellType = numberCellTypes1;
                    list220i++;  
                }
                for (int i = 0; i < list220s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i+list330i + list220l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 0, list220s[list220s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 1, list220s[list220s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 2, list220s[list220s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i+list330i + list220l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i+list330i + list220l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i +list330i+ list220l.Count, 2].CellType = numberCellTypes1;
                    list220i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i+list330i, 1].Formula = "SUM(R" + (5 + list1000i + list750i+list500i+list330i + list220l.Count).ToString() + "C2:R" + (5 + list1000i + list750i +list500i+list330i+ list220l.Count + list220s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i+list500i+list330i, 2].Formula = "SUM(R" + (5 + list1000i + list750i+list500i +list330i+ list220l.Count).ToString() + "C3:R" + (5 + list1000i + list750i+list500i+list330i + list220l.Count + list220s.Count - 1).ToString() + "C3)";
                 
            }
            //110KV电网
            int list110i = 0;
            if (list110l.Count!=0||list110s.Count!=0)
            {
                list110i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i+list220i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i+list220i, 0, "110KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i+list220i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i+list220i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                for (int i = 0; i < list110l.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i+list220i, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 0, list110l[list110l.Count - 1 - i].Name);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 1, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 2, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 3, list110l[list110l.Count - 1 - i].LineType);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i, 4, list110l[list110l.Count - 1 - i].LineLength);
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330i+list220i, 4].CellType = numberCellTypes1;
                    list110i++;  
                }
                for (int i = 0; i < list110s.Count;i++ )
                {
                    fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 1);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 0, list110s[list110s.Count - 1 - i].Title);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 1, list110s[list110s.Count - 1 - i].L2);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count, 2, list110s[list110s.Count - 1 - i].L3);
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count, 3, "X");
                    fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 4, "X");
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count, 1].CellType = numberCellTypes1;
                    fpSpread1.Sheets[1].Cells[4 + list1000i + list750i + list500i + list330i+list220i + list110l.Count, 2].CellType = numberCellTypes1;
                    list110i++;
                }
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i+list220i, 1].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i+list220i + list110l.Count).ToString() + "C2:R" + (5 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count + list110s.Count - 1).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i+list220i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count).ToString() + "C3:R" + (5 + list1000i + list750i + list500i + list330i +list220i+ list110l.Count + list110s.Count - 1).ToString() + "C3)";
            }
#region 配网数据的添加 其中配网数据分为城网和农网
            //城市66KV电网
            int listcity66i = 0;
            if (listcity66s.Count!=0||listcity6l.Count!=0)
            {
                listcity66i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i+list110i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "城市66KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "柱上开关");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "柱配电变压器");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "箱式变电站");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(7+ list1000i + list750i + list500i + list330i + list220i + list110i, 0, "开闭所");
                fpSpread1.Sheets[1].Rows[7+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "公用线路");
                fpSpread1.Sheets[1].Rows[8+ list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "其中：架空线路");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 0, "      电缆线路");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //数值统计
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i+list110i,1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i +list110i).ToString() + "C2:R" + (7+ list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i,2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i,4].Formula = "SUM(R" + (10+ list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i).ToString() + "C4)";
                //添加数值
               double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity66l.Count;i++ )
                {
                    if (listcity66l[i].JXFS=="架空线路")
                    {
                        jknum += listcity66l[i].LineLength;
                    }
                    if (listcity66l[i].JXFS == "电缆线路")
                    {
                        dlnum += listcity66l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i, 3, dlnum);
            }
            //城市35KV电网
            int listcity35i = 0;
            if (listcity35l.Count!=0||listcity35s.Count!=0)
            {
                listcity35i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i+listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "城市35KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "柱上开关");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "柱配电变压器");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "箱式变电站");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "开闭所");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "公用线路");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "其中：架空线路");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 0, "      电缆线路");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //数值统计
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i).ToString() + "C4)";
                //添加数值
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity35l.Count; i++)
                {
                    if (listcity35l[i].JXFS == "架空线路")
                    {
                        jknum += listcity35l[i].LineLength;
                    }
                    if (listcity35l[i].JXFS == "电缆线路")
                    {
                        dlnum += listcity35l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i, 3, dlnum);
            }
            //城市10KV电网
            int listcity10i = 0;
            if (listcity10s.Count!=0||listcity10l.Count!=0)
            {
                listcity10i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i+listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "城市10KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "柱上开关");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "柱配电变压器");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "箱式变电站");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "开闭所");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "公用线路");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "其中：架空线路");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 0, "      电缆线路");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //数值统计
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i).ToString() + "C4)";
                //添加数值
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity10l.Count; i++)
                {
                    if (listcity10l[i].JXFS == "架空线路")
                    {
                        jknum += listcity10l[i].LineLength;
                    }
                    if (listcity10l[i].JXFS == "电缆线路")
                    {
                        dlnum += listcity10l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i, 3, dlnum);
            }
            //城市6KV电网
            int listcity6i = 0;
            if (listcity6l.Count!=0||listcity6s.Count!=0)
            {
                listcity6i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i+listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "城市6KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "柱上开关");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "柱配电变压器");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "箱式变电站");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "开闭所");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "公用线路");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "其中：架空线路");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 0, "      电缆线路");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //数值统计
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i).ToString() + "C4)";
                //添加数值
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity6l.Count; i++)
                {
                    if (listcity6l[i].JXFS == "架空线路")
                    {
                        jknum += listcity6l[i].LineLength;
                    }
                    if (listcity6l[i].JXFS == "电缆线路")
                    {
                        dlnum += listcity6l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i, 3, dlnum);
            }
            int listcity3i = 0;
            if (listcity3s.Count!=0||listcity3l.Count!=0)
            {
                listcity3i = 8;
                fpSpread1.Sheets[1].Rows.Add(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i+listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "城市3KV电网");
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

                fpSpread1.Sheets[1].Rows.Add(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "柱上开关");
                fpSpread1.Sheets[1].Rows[4 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "柱配电变压器");
                fpSpread1.Sheets[1].Rows[5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "箱式变电站");
                fpSpread1.Sheets[1].Rows[6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "开闭所");
                fpSpread1.Sheets[1].Rows[7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "公用线路");
                fpSpread1.Sheets[1].Rows[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "其中：架空线路");
                fpSpread1.Sheets[1].Rows[9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                fpSpread1.Sheets[1].Rows.Add(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 0, "      电缆线路");
                fpSpread1.Sheets[1].Rows[10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                //数值统计
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 1].Formula = "SUM(R" + (6 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C2:R" + (7 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C2)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 2].Formula = "SUM(R" + (5 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3:R" + (8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C3)";
                fpSpread1.Sheets[1].Cells[3 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4)";
                fpSpread1.Sheets[1].Cells[8 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 4].Formula = "SUM(R" + (10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4:R" + (11 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i).ToString() + "C4)";
                //添加数值
                double jknum = 0, dlnum = 0;
                for (int i = 0; i < listcity3l.Count; i++)
                {
                    if (listcity3l[i].JXFS == "架空线路")
                    {
                        jknum += listcity3l[i].LineLength;
                    }
                    if (listcity3l[i].JXFS == "电缆线路")
                    {
                        dlnum += listcity3l[i].LineLength;
                    }
                }
                fpSpread1.Sheets[1].SetValue(9 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 3, jknum);
                fpSpread1.Sheets[1].SetValue(10 + list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i, 3, dlnum);
            }

            //农村66KV电网 在此期间要区分县和统计线路和变电所的数量 容量
            int citynum = list1000i + list750i + list500i + list330i + list220i + list110i + listcity66i + listcity35i + listcity10i + listcity6i + listcity3i;
            int listcountry66i = 0;
            if (flag66l||flag66s)
            {
                listcountry66i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum, 0, "农村66KV电网");
                fpSpread1.Sheets[1].Rows[3 + citynum].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //根据县域来统计线路和变电所
                 foreach (PS_Table_AreaWH area in list)
                 {
                     if (listcountry66l.ContainsKey(area.Title) && listcountry66l.ContainsKey(area.Title))
                     {
                         fpSpread1.Sheets[1].Rows.Add(4 + citynum, 1);
                         fpSpread1.Sheets[1].SetValue(4 + citynum, 0, area.Title);
                         fpSpread1.Sheets[1].Rows[4 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                         fpSpread1.Sheets[1].Rows.Add(5 + citynum, 1);
                         fpSpread1.Sheets[1].SetValue(5 + citynum, 0, "其中：线路");
                         fpSpread1.Sheets[1].Rows[5 + citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                         fpSpread1.Sheets[1].Rows.Add(6 + citynum, 1);
                         fpSpread1.Sheets[1].SetValue(6 + citynum, 0, "    变电所");
                         fpSpread1.Sheets[1].Rows[6+ citynum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                         listcountry66i += 3;  //行数记录

                         //统计和添加结果
                         double Zrlnum = 0; int bdts = 0; double lenth = 0;
                         foreach (Substation_Info si in listcountry66s[area.Title])
                         {
                             Zrlnum += si.L2;
                             bdts += Convert.ToInt32(si.L3);
                         }
                         foreach(PSPDEV ps in listcountry66l[area.Title])
                         {
                             lenth += ps.LineLength;

                         }
                         //数值统计
                         fpSpread1.Sheets[1].Cells[4 + citynum, 1].Formula = "R" + (7 + citynum).ToString() + "C2";
                         fpSpread1.Sheets[1].Cells[4 + citynum, 2].Formula = "R" + (7 + citynum).ToString() + "C3";
                         fpSpread1.Sheets[1].Cells[4 + citynum, 4].Formula = "R" + (6 + citynum).ToString() + "C5";

                         //赋值
                         fpSpread1.Sheets[1].SetValue(5 + citynum, 4, lenth);
                         fpSpread1.Sheets[1].SetValue(6 + citynum, 2, Zrlnum);
                         fpSpread1.Sheets[1].SetValue(6 + citynum, 3, bdts);
                     }
                 }

            }
            //农村35KV电网生成
            int listcountry35i = 0;
            if (flag35l||flag35s)
            {

                listcountry35i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum+listcountry66i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i, 0, "农村35KV电网");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //根据县域来统计线路和变电所
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry35l.ContainsKey(area.Title) && listcountry35l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum+listcountry66i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i, 0, "其中：线路");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6 + citynum + listcountry66i, 1);
                        fpSpread1.Sheets[1].SetValue(6+ citynum + listcountry66i, 0, "    变电所");
                        fpSpread1.Sheets[1].Rows[6+ citynum + listcountry66i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry35i += 3;  //行数记录

                        //统计和添加结果
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry35s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts += Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry35l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //数值统计
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i, 1].Formula = "R" + (7 + citynum + listcountry66i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i, 2].Formula = "R" + (7 + citynum + listcountry66i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i, 4].Formula = "R" + (6 + citynum + listcountry66i).ToString() + "C5";

                        //赋值
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i, 3, bdts);
                    }

                }
            }
            //农村10KV电网
            int listcountry10i = 0;
            if (flag10l||flag10s)
            {
                listcountry10i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum + listcountry66i+listcountry35i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i + listcountry35i, 0, "农村10KV电网");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //根据县域来统计线路和变电所
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry10l.ContainsKey(area.Title) && listcountry10l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i + listcountry35i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i, 0, "其中：线路");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6+ citynum + listcountry66i + listcountry35i, 1);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i, 0, "    变电所");
                        fpSpread1.Sheets[1].Rows[6+ citynum + listcountry66i + listcountry35i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry10i += 3;  //行数记录

                        //统计和添加结果
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry10s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts +=Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry10l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //数值统计
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i, 4].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i).ToString() + "C5";

                        //赋值
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry35i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i, 3, bdts);
                    }

                }
            }
            //农村6KV电网
            int listcountry6i = 0;
            if (flag6l||flag6s)
            {
                listcountry6i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum + listcountry66i + listcountry35i+listcountry10i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "农村6KV电网");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //根据县域来统计线路和变电所
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry6l.ContainsKey(area.Title) && listcountry6l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry10i, 0, "其中：线路");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6+ citynum + listcountry66i + listcountry35i + listcountry10i, 1);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 0, "    变电所");
                        fpSpread1.Sheets[1].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry6i += 3;  //行数记录

                        //统计和添加结果
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry6s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts +=Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry6l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //数值统计
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i, 4].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i + listcountry10i).ToString() + "C5";

                        //赋值
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i, 3, bdts);
                    }

                }
            }
            //农村3KV电网
            int listcountry3i = 0;
            if (flag3l||flag3s)
            {
                listcountry3i = 1;
                fpSpread1.Sheets[1].Rows.Add(3 + citynum + listcountry66i + listcountry35i + listcountry10i+listcountry6i, 1);
                fpSpread1.Sheets[1].SetValue(3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "农村6KV电网");
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].Font = new Font("宋体", 9, FontStyle.Bold);
                fpSpread1.Sheets[1].Rows[3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                //根据县域来统计线路和变电所
                foreach (PS_Table_AreaWH area in list)
                {


                    if (listcountry3l.ContainsKey(area.Title) && listcountry3l.ContainsKey(area.Title))
                    {
                        fpSpread1.Sheets[1].Rows.Add(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[1].SetValue(4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, area.Title);
                        fpSpread1.Sheets[1].Rows[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry10i + listcountry6i, 0, "其中：线路");
                        fpSpread1.Sheets[1].Rows[5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;

                        fpSpread1.Sheets[1].Rows.Add(6+ citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 0, "    变电所");
                        fpSpread1.Sheets[1].Rows[6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                        listcountry3i += 3;  //行数记录

                        //统计和添加结果
                        double Zrlnum = 0; int bdts = 0; double lenth = 0;
                        foreach (Substation_Info si in listcountry3s[area.Title])
                        {
                            Zrlnum += si.L2;
                            bdts += Convert.ToInt32(si.L3);
                        }
                        foreach (PSPDEV ps in listcountry3l[area.Title])
                        {
                            lenth += ps.LineLength;

                        }
                        //数值统计
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 1].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C2";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2].Formula = "R" + (7 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C3";
                        fpSpread1.Sheets[1].Cells[4 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 4].Formula = "R" + (6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i).ToString() + "C5";

                        //赋值
                        fpSpread1.Sheets[1].SetValue(5 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 4, lenth);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 2, Zrlnum);
                        fpSpread1.Sheets[1].SetValue(6 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i, 3, bdts);
                    }

                }
                //设定格式
               
            }
#endregion
#endregion
            Sheet_GridandColor(fpSpread1.Sheets[1], 3 + citynum + listcountry66i + listcountry35i + listcountry10i + listcountry6i + listcountry3i, 6);
        }

        //高压配电网主要运行指标
        private void gypdyxzb()
        {
            //添加表头
            gypdyxtable();
            //添加数据
#region //添加数据
            string con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
            double rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
            double sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 4, sumrl / rongliang);
            }
            
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 4, sumrl / rongliang);
            }
            
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            { 
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 4, sumrl / rongliang);
            }
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            { 
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 4, sumrl / rongliang);
            }
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='110'OR L1='66'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='110'OR L1='66'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 4, sumrl / rongliang);
            }
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='35'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 4, sumrl / rongliang);
            }
            

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='110'OR L1='66'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='110'OR L1='66'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 4, sumrl / rongliang);
            }
       

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='35'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 4, sumrl / rongliang);
            }
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='110'OR L1='66'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='110'OR L1='66'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 4, sumrl / rongliang);
            }
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='35'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            { 
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 4, sumrl / rongliang);
            }
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='110'OR L1='66'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='110'OR L1='66'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 4, sumrl / rongliang);
            }
            

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='35'";
            rongliang = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 3, rongliang);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            if (rongliang!=0)
            { 
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 4, sumrl / rongliang);
            }
           
            fpSpread1.Sheets[0].Cells[15, 3].Formula = "R4C4+R6C4";
            FPSpread.Sheets[0].Cells[16, 3].Formula = "R5C4+R7C4";
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND L1='110'OR L1='66'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            fpSpread1.Sheets[0].Cells[15, 4].Formula = sumrl.ToString() + "/R16C4";
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND L1='35'";
            sumrl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
            fpSpread1.Sheets[0].Cells[16, 4].Formula = sumrl.ToString() + "/R17C4";

            //统计主变台数的关系
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'and S1='合格'";
            int hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
            int ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 6, hegts / ts);
            }
            else
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 6, 0);
            

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 6, hegts / ts);
            }
            else
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 6, 0);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 6, hegts / ts);
            }
            else
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 6, 0);
           

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'";            
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
               CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 6, hegts / ts);
            }
            else
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 6, 0);
            

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='110'OR L1='66'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='110'OR L1='66'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 6, hegts / ts);
            }
           else
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 6, 0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='35'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级直供直管'AND L1='35'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 6, hegts / ts);

            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 6,0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='110'OR L1='66'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='110'OR L1='66'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 6, hegts / ts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 6,0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='35'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级控股'AND L1='35'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 6, hegts / ts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 6, 0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='110'OR L1='66'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='110'OR L1='66'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 6, hegts / ts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 6, 0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='35'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级参股'AND L1='35'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 6, hegts / ts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 6, 0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='110'OR L1='66'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='110'OR L1='66'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 6, hegts / ts);

            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 6, 0);

            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='35'and S1='合格'";
            hegts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 5, hegts);
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='县级代管'AND L1='35'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 6, hegts / ts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 6, 0);
            fpSpread1.Sheets[0].Cells[15, 5].Formula = "R16C6+R16C6";
            FPSpread.Sheets[0].Cells[16, 5].Formula = "R17C6+R17C6";
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND L1='110'OR L1='66'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                fpSpread1.Sheets[0].Cells[15, 6].Formula = "R16C7/" + ts.ToString();
            }
            
            con = "AreaID='" + ProjectUID + "'AND S4='公用'AND L1='35'";
            ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
            if (ts!=0)
            {
                fpSpread1.Sheets[0].Cells[16, 6].Formula = "R17C7/" + ts.ToString();

            }
          
            //线路合格率统计
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            int hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='市辖供电区'and LineType2='公用'and type='05'";
            int zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='市辖供电区'and LineType2='公用'and type='05'";
            
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ!='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ!='市辖供电区'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 5, 8,0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ!='市辖供电区'and LineType2='公用'AND(HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ!='市辖供电区'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 6, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级直供直管'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级直供直管'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 7, 8,0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级直供直管'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级直供直管'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 8, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级控股'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级控股'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 9, 8,0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级控股'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级控股'and LineType2='公用'and type='05'";
           
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts != 0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 8, hgts / zts);

            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 10, 8,0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级参股'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级参股'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 11, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级参股'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级控股'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 12, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级代管'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='县级代管'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 8, hgts / zts);
            }
            else
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 13, 8, 0);

        con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级代管'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
            hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 7, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='县级代管'and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

            fpSpread1.Sheets[0].Cells[15, 7].Formula = "R4C8+R6C8";
            FPSpread.Sheets[0].Cells[16, 7].Formula = "R5C8+R7C8";
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                fpSpread1.Sheets[0].Cells[15, 6].Formula = "R16C8/" + zts.ToString();
            }

            con = "ProjectID='" + ProjectUID + "'and RateVolt in('35')and LineType2='公用'and type='05'";
            zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 14, 8, hgts / zts);
            }
            fpSpread1.Sheets[0].Cells[16, 6].Formula = "R17C8/" + zts.ToString();
           





#endregion
            
        }
        private void gypdyxtable()
        {
            fpSpread1.Sheets[0].RowCount = 0;
            fpSpread1.Sheets[0].ColumnCount = 0;
            fpSpread1.Sheets[0].RowCount = 17;
            fpSpread1.Sheets[0].ColumnCount = 9;
            fpSpread1.Sheets[0].SetValue(0, 0, "2010年铜陵市高压配电网主要运行指标");
            fpSpread1.Sheets[0].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[0].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[0].Cells[0, 0].ColumnSpan = 9;
            fpSpread1.Sheets[0].Columns[4].CellType = numberCellTypes1;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[0], 1, "其中：直供直管");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 1, 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 1, 3, "负荷");
            fpSpread1.Sheets[0].Cells[1,4].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 1,4, "容载比");
            CreateSheetView(fpSpread1.Sheets[0], 1, 2, 1, 5, "满足N-1主变");
            CreateSheetView(fpSpread1.Sheets[0], 1, 2, 1, 7, "满足N-1线路");
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 2, 5, "台数（台）");
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 2, 6, "比例（%）");
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 2, 7, "条数（条）");
            CreateSheetView(fpSpread1.Sheets[0], 1, 1, 2, 8, "比例（%）");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 3, 0, "1");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 3, 1, "市辖供电区");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 5, 0, "2");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 5, 1, "县级供电区");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 7, 0, "2.1");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 7, 1, "其中：直供直管");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 9, 0, "2.2");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 9, 1, "控股");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 11, 0, "2.3");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 11, 1, "参股");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 13, 0, "2.4");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 13, 1, "代管");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 15, 0, "2.5");
            CreateSheetView(fpSpread1.Sheets[0], 2, 1, 15, 1, "全地区");
            for (int i = 0; i < 14;i+=2 )
            {
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 3 + i,2, "110（66）");
                CreateSheetView(fpSpread1.Sheets[0], 1, 1, 4 + i,2, "35");
            }
            Sheet_GridandColor(fpSpread1.Sheets[0], 17, 9);

        }
        //高压配电网主要运行指标附表
        private struct tjclass
        {
            public double fh;
            public double rl;
            public int subts;
            public int subhgts;
            public int linets;
            public int linehgts;
        }
        private void gypdyxzbfb()
        {
            //添加表头
            gypdyxfbtable();
            //添加数据
            #region 统计数据
            Dictionary<string, tjclass> city110tj = new Dictionary<string, tjclass>();
            Dictionary<string, tjclass> country110tj = new Dictionary<string, tjclass>();
            Dictionary<string, tjclass> city35tj = new Dictionary<string, tjclass>();
            Dictionary<string, tjclass> country35tj = new Dictionary<string, tjclass>();

            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            tjclass cityzon110=new tjclass();tjclass cityzon35=new tjclass();tjclass countryzon110=new tjclass();tjclass countyzon35=new tjclass();
           
            foreach (PS_Table_AreaWH pa in list)
            { 
                //市辖110
                tjclass tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                tj.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                tj.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'and S1='合格'and AreaName='" + pa.Title + "'";
                tj.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                tj.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and AreaID='" + pa.ID + "'and type='05'";
                tj.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='市辖供电区'and LineType2='公用'and AreaID='" + pa.ID + "'and type='05'";
                tj.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
               if (tj.fh!=0||tj.rl!=0||tj.subhgts!=0||tj.subts!=0||tj.linehgts!=0||tj.linets!=0)
               {
                   city110tj.Add(pa.Title,tj);
               }
                
                //市辖35
                tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                tj.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                tj.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'and S1='合格'and AreaName='" + pa.Title + "'";
                tj.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                tj.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and AreaID='" + pa.ID + "'and type='05'";
                tj.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='市辖供电区'and LineType2='公用'and AreaID='" + pa.ID + "'and type='05'";
                tj.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
                 if (tj.fh!=0||tj.rl!=0||tj.subhgts!=0||tj.subts!=0||tj.linehgts!=0||tj.linets!=0)
               {
                   city35tj.Add(pa.Title, tj);
               }
                
                //县110
                tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                tj.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                tj.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and S1='合格'and AreaName='" + pa.Title + "'";
                tj.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                tj.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ!='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and AreaID='" + pa.ID + "'and type='05'";
                tj.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ!='市辖供电区'and LineType2='公用'and AreaID='" + pa.ID + "'and type='05'";
                tj.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
                 if (tj.fh!=0||tj.rl!=0||tj.subhgts!=0||tj.subts!=0||tj.linehgts!=0||tj.linets!=0)
               {
                  country110tj.Add(pa.Title,tj);
               }
                
                //县35
                tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                tj.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                tj.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'and S1='合格'and AreaName='" + pa.Title + "'";
                tj.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                tj.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ!='市辖供电区'and LineType2='公用'AND(HgFlag is null or HgFlag='合格')and AreaID='" + pa.ID + "'and type='05'";
                tj.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ!='市辖供电区'and LineType2='公用'and AreaID='" + pa.ID + "'and type='05'";
                tj.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
                if (tj.fh!=0||tj.rl!=0||tj.subhgts!=0||tj.subts!=0||tj.linehgts!=0||tj.linets!=0)
               {
                   country35tj.Add(pa.Title, tj);
               }
                
              

            }
             //统计合计的总数
             //市辖110
                //tjclass tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
                cityzon110.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
                cityzon110.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'and S1='合格'";
                cityzon110.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
                cityzon110.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
                cityzon110.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ='市辖供电区'and LineType2='公用'and type='05'";
                cityzon110.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
              
                //市辖35
               // tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'";
                cityzon35.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'";
               cityzon35.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'and S1='合格'";
                cityzon35.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ='市辖供电区'AND L1='35'";
                cityzon35.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
                cityzon35.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ='市辖供电区'and LineType2='公用'and type='05'";
                cityzon35.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
              
                //县110
               // tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
                countryzon110.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
                 countryzon110.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and S1='合格'";
                 countryzon110.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
                 countryzon110.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                 con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ!='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
                 countryzon110.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                 con = "ProjectID='" + ProjectUID + "'and RateVolt in('110','66') and DQ!='市辖供电区'and LineType2='公用'and type='05'";
                 countryzon110.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
              
                //县35
                //tj = new tjclass();
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'";
                countyzon35.fh = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML9", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'";
                countyzon35.rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML2", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'and S1='合格'";
                countyzon35.subhgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "AreaID='" + ProjectUID + "'AND S4='公用'AND DQ!='市辖供电区'AND L1='35'";
                countyzon35.subts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSP_Substation_InfoSUML3", con));
                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ!='市辖供电区'and LineType2='公用'AND (HgFlag is null or HgFlag='合格')and type='05'";
                countyzon35.linehgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));

                con = "ProjectID='" + ProjectUID + "'and RateVolt in('35') and DQ!='市辖供电区'and LineType2='公用'and type='05'";
                countyzon35.linets = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
              
            //添加数据
            int numcityi = 0; int numcountryi = 0;
            fpSpread1.Sheets[1].Rows.Add(3,2);

            CreateSheetView(fpSpread1.Sheets[1],2,1,3,1,"合计");
            CreateSheetView(fpSpread1.Sheets[1],1,1,3,2,"110(66)");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 3, 3, cityzon110.fh);
            if (cityzon110.fh!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 3, 4, cityzon110.rl / cityzon110.fh);
            }
           
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 3, 5, cityzon110.subhgts);
            if (cityzon110.subhgts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 3, 6, cityzon110.subhgts / cityzon110.subts);
            }
            
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 3, 7, cityzon110.linehgts);
            if (cityzon110.linehgts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 3, 8, cityzon110.linehgts / cityzon110.linets);
            }
            
            CreateSheetView(fpSpread1.Sheets[1],1,1,4,2,"35");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 4, 3, cityzon35.fh);
            if (cityzon35.fh!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 4, 4, cityzon35.rl / cityzon35.fh);
            }
            
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 4, 5, cityzon35.subhgts);
            if (cityzon35.subts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 4, 6, cityzon35.subhgts / cityzon35.subts);
            }
            
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 4, 7, cityzon35.linehgts);
            if (cityzon35.linets!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 4, 8, cityzon35.linehgts / cityzon35.linets);
            }
          
          
            foreach (PS_Table_AreaWH pa in list)
            {
                if (city110tj.ContainsKey(pa.Title)||city35tj.ContainsKey(pa.Title))
                {
                    numcityi+=2;
                    fpSpread1.Sheets[1].Rows.Add(5, 2);
                    CreateSheetView(fpSpread1.Sheets[1], 2, 1, 5, 1,pa.Title);
                    CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 2, "110(66)");
                    CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 2, "35");
                    if (city110tj.ContainsKey(pa.Title))
                    {
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 3, city110tj[pa.Title].fh);
                        if (city110tj[pa.Title].fh!=0)
                       {
                           CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 4, city110tj[pa.Title].rl / city110tj[pa.Title].fh);
                       }
                        
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 5, city110tj[pa.Title].subhgts);
                        if (city110tj[pa.Title].subts!=0)
                       {
                           CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 6, city110tj[pa.Title].subhgts / city110tj[pa.Title].subts);
                       }
                        
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 7, city110tj[pa.Title].linehgts);
                        if (city110tj[pa.Title].linets!=0)
                       {
                           CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5, 8, city110tj[pa.Title].linehgts / city110tj[pa.Title].linets);
                       }
                        
                    }
                    if (city35tj.ContainsKey(pa.Title))
                    {
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 3, city35tj[pa.Title].fh);
                        if (city35tj[pa.Title].fh!=0)
                       {
                           CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 4, city35tj[pa.Title].rl / city35tj[pa.Title].fh);
                       }
                       
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 5, city35tj[pa.Title].subhgts);
                        if (city35tj[pa.Title].subts!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 6, city35tj[pa.Title].subhgts / city35tj[pa.Title].subts);
                        }
                        
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 7, city35tj[pa.Title].linehgts);
                        if (city35tj[pa.Title].linets!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6, 8, city35tj[pa.Title].linehgts / city35tj[pa.Title].linets);
                        }
                      
                    }
                }
                
            }
            CreateSheetView(fpSpread1.Sheets[1], 2+numcityi, 1, 3, 0, "市辖供电区");
           //县级
            fpSpread1.Sheets[1].Rows.Add(5+numcityi, 2);

            CreateSheetView(fpSpread1.Sheets[1], 2, 1, 5 + numcityi, 1, "合计");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 2, "110(66)");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 3, countryzon110.fh);
            if (countryzon110.fh!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 4, countryzon110.rl / countryzon110.fh);
            }
           
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 5, countryzon110.subhgts);
            if (countryzon110.subts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 6, countryzon110.subhgts / countryzon110.subts);
            }
          
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 7, countryzon110.linehgts);
            if (countryzon110.linets!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 5 + numcityi, 8, countryzon110.linehgts / countryzon110.linets);
            }
            
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 2, "35");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 3, countyzon35.fh);
            if (countyzon35.fh!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 4, countyzon35.rl / countyzon35.fh);
            }
            
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 5, countyzon35.subhgts);
            if (countyzon35.subts!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 6, countyzon35.subhgts / countyzon35.subts);
            }
           
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 7, countyzon35.linehgts);
            if (countyzon35.linets!=0)
            {
                CreateSheetView(fpSpread1.Sheets[1], 1, 1, 6 + numcityi, 8, countyzon35.linehgts / countyzon35.linets);
            }
           

            foreach (PS_Table_AreaWH pa in list)
            {
                if (country110tj.ContainsKey(pa.Title) || country35tj.ContainsKey(pa.Title))
                {
                    numcountryi += 2;
                    fpSpread1.Sheets[1].Rows.Add(7+numcityi, 2);
                    CreateSheetView(fpSpread1.Sheets[1], 2, 1, 7 + numcityi, 1, pa.Title);
                    CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 2, "110(66)");
                    CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 2, "35");
                    if (country110tj.ContainsKey(pa.Title))
                    {
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 3, country110tj[pa.Title].fh);
                        if (country110tj[pa.Title].fh!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 4, country110tj[pa.Title].rl / country110tj[pa.Title].fh);
                        }
                       
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 5, country110tj[pa.Title].subhgts);
                        if (country110tj[pa.Title].subts!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 6, country110tj[pa.Title].subhgts / country110tj[pa.Title].subts);
                        }
                        
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 7, country110tj[pa.Title].linehgts);
                        if (country110tj[pa.Title].linets!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 7 + numcityi, 8, country110tj[pa.Title].linehgts / country110tj[pa.Title].linets);
                        }
                       
                    }
                    if (country35tj.ContainsKey(pa.Title))
                    {
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 3, country35tj[pa.Title].fh);
                        if (country35tj[pa.Title].fh!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 4, country35tj[pa.Title].rl / country35tj[pa.Title].fh);
                        }
                        
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 5, country35tj[pa.Title].subhgts);
                        if (country35tj[pa.Title].subts!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 6, country35tj[pa.Title].subhgts / country35tj[pa.Title].subts);
                        }
                       
                        CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 7, country35tj[pa.Title].linehgts);
                        if (country35tj[pa.Title].linets!=0)
                        {
                            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 8 + numcityi, 8, country35tj[pa.Title].linehgts / country35tj[pa.Title].linets);
                        }
                       
                    }
                }

            }
            CreateSheetView(fpSpread1.Sheets[1], 2+numcountryi, 1, 5 + numcityi, 0, "县级供电区");
            fpSpread1.Sheets[1].Rows.Remove(7+numcityi+numcountryi,1);
            Sheet_GridandColor(fpSpread1.Sheets[1], 7 + numcountryi + numcityi, 9);
            #endregion
           
        }
        private void gypdyxfbtable()
        {
            fpSpread1.Sheets[1].RowCount = 0;
            fpSpread1.Sheets[1].ColumnCount = 0;
            fpSpread1.Sheets[1].RowCount = 4;
            fpSpread1.Sheets[1].ColumnCount = 9;
            fpSpread1.Sheets[1].SetValue(0, 0, "2010年铜陵市高压配电网主要运行指标");
            fpSpread1.Sheets[1].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[1].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[1].Cells[0, 0].ColumnSpan = 9;
            SetSheetViewColumnsWhith(fpSpread1.Sheets[1], 1, "其中：直供直管");
            CreateSheetView(fpSpread1.Sheets[1], 2, 1, 1, 0, "分区类型");
            CreateSheetView(fpSpread1.Sheets[1], 2, 1, 1, 1, "分区名称");
            CreateSheetView(fpSpread1.Sheets[1], 2, 1, 1, 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[1], 2, 1, 1, 3, "负荷");
            CreateSheetView(fpSpread1.Sheets[1], 2, 1, 1, 4, "容载比");
            CreateSheetView(fpSpread1.Sheets[1], 1, 2, 1, 5, "满足N-1主变");
            CreateSheetView(fpSpread1.Sheets[1], 1, 2, 1, 7, "满足N-1线路");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 2, 5, "台数（台）");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 2, 6, "比例（%）");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 2, 7, "条数（条）");
            CreateSheetView(fpSpread1.Sheets[1], 1, 1, 2, 8, "比例（%）");
         

        }
        //容载比分布表
        private void rzbfbb()
        {
         //制表
            rzbfbbtable();
            //添加数据
            string con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
            IList list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 4);

            con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 5);
            //加入统计企业
            con = "ProjectID='"+ProjectUID+"'and SType='市辖供电区'";
            int entpnum = 0;
            entpnum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 3, 2, entpnum);
            con = "AreaID='" + ProjectUID + "'AND DQ='县级直供直管'AND L1='110'OR L1='66'";
           list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 7);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级直供直管'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 8);
            con = "ProjectID='" + ProjectUID + "'and SType='县级直供直管'";
            entpnum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 7, 2, entpnum);
            con = "AreaID='" + ProjectUID + "'AND DQ='县级控股'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 9);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级控股'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 10);
            con = "ProjectID='" + ProjectUID + "'and SType='县级控股'";
            entpnum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 9, 2, entpnum);
            con = "AreaID='" + ProjectUID + "'AND DQ='县级参股'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 11);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级参股'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 12);
            con = "ProjectID='" + ProjectUID + "'and SType='县级参股'";
            entpnum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 11, 2, entpnum);
            con = "AreaID='" + ProjectUID + "'AND DQ='县级代管'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 13);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级代管'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet2value(fpSpread1.Sheets[2], list1, 14);
            con = "ProjectID='" + ProjectUID + "'and SType='县级代管'";
            entpnum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 13, 2, entpnum);
            fpSpread1.Sheets[2].Cells[5, 2].Formula = "R8C2+R10C2+R12C2+R14C2";
            fpSpread1.Sheets[2].Cells[5, 2].RowSpan = 2;
            fpSpread1.Sheets[2].Cells[5, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[2].Cells[5, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            fpSpread1.Sheets[2].Cells[5, 4].Formula = "R8C5+R10C5+R12C5+R14C5";
            FPSpread.Sheets[2].Cells[5, 6].Formula = "R8C7+R10C7+R12C7+R14C7";
            FPSpread.Sheets[2].Cells[5, 8].Formula = "R8C9+R10C9+R12C9+R14C9";
            FPSpread.Sheets[2].Cells[5, 10].Formula = "R8C11+R10C11+R12C11+R14C11";
            FPSpread.Sheets[2].Cells[5, 12].Formula = "R8C13+R10C13+R12C13+R14C13";

            fpSpread1.Sheets[2].Cells[5, 5].Formula = "R6C5/(R6C5+R6C7+R6C9+R6C11+R6C13)";
            FPSpread.Sheets[2].Cells[5, 7].Formula = "R6C7/(R6C5+R6C7+R6C9+R6C11+R6C13)";
            FPSpread.Sheets[2].Cells[5, 9].Formula = "R6C9/(R6C5+R6C7+R6C9+R6C11+R6C13)";
            FPSpread.Sheets[2].Cells[5, 11].Formula = "R6C11/(R6C5+R6C7+R6C9+R6C11+R6C13)";
            FPSpread.Sheets[2].Cells[5, 13].Formula = "R6C13/(R6C5+R6C7+R6C9+R6C11+R6C13)";

            fpSpread1.Sheets[2].Cells[6, 4].Formula = "R9C5+R11C5+R13C5+R15C5";
            FPSpread.Sheets[2].Cells[6, 6].Formula = "R9C7+R11C7+R13C7+R15C7";
            FPSpread.Sheets[2].Cells[6, 8].Formula = "R9C9+R11C9+R13C9+R15C9";
            FPSpread.Sheets[2].Cells[6, 10].Formula = "R9C11+R11C11+R13C11+R15C11";
            FPSpread.Sheets[2].Cells[6, 12].Formula = "R9C13+R11C13+R13C13+R15C13";

            fpSpread1.Sheets[2].Cells[6, 5].Formula = "R7C5/(R7C5+R7C7+R7C9+R7C11+R7C13)";
            FPSpread.Sheets[2].Cells[6, 7].Formula = "R7C7/(R7C5+R7C7+R7C9+R7C11+R7C13)";
            FPSpread.Sheets[2].Cells[6, 9].Formula = "R7C9/(R7C5+R7C7+R7C9+R7C11+R7C13)";
            FPSpread.Sheets[2].Cells[6, 11].Formula = "R7C11/(R7C5+R7C7+R7C9+R7C11+R7C13)";
            FPSpread.Sheets[2].Cells[6, 13].Formula = "R7C13/(R7C5+R7C7+R7C9+R7C11+R7C13)";

            fpSpread1.Sheets[2].Cells[15, 4].Formula = "R4C5+R6C5";
            fpSpread1.Sheets[2].Cells[15,6].Formula = "R4C7+R6C7";
            fpSpread1.Sheets[2].Cells[15, 8].Formula = "R4C9+R6C9";
            fpSpread1.Sheets[2].Cells[15,10].Formula = "R4C11+R6C11";
            fpSpread1.Sheets[2].Cells[15, 12].Formula = "R4C13+R6C13";


            fpSpread1.Sheets[2].Cells[15, 5].Formula = "R16C5/(R16C5+R16C7+R16C9+R16C11+R16C13)";
            fpSpread1.Sheets[2].Cells[15, 7].Formula = "R16C7/(R16C5+R16C7+R16C9+R16C11+R16C13)";
            fpSpread1.Sheets[2].Cells[15, 9].Formula = "R16C9/(R16C5+R16C7+R16C9+R16C11+R16C13)";
            fpSpread1.Sheets[2].Cells[15, 11].Formula = "R16C11/(R16C5+R16C7+R16C9+R16C11+R16C13)";
            fpSpread1.Sheets[2].Cells[15, 13].Formula = "R16C13/(R16C5+R16C7+R16C9+R16C11+R16C13)";

            fpSpread1.Sheets[2].Cells[16, 4].Formula = "R5C5+R7C5";
            fpSpread1.Sheets[2].Cells[16, 6].Formula = "R5C7+R7C7";
            fpSpread1.Sheets[2].Cells[16, 8].Formula = "R5C9+R7C9";
            fpSpread1.Sheets[2].Cells[16, 10].Formula = "R5C11+R7C11";
            fpSpread1.Sheets[2].Cells[16, 12].Formula = "R5C13+R7C13";

            fpSpread1.Sheets[2].Cells[16, 5].Formula = "R17C5/(R17C5+R17C7+R17C9+R17C11+R17C13)";
            fpSpread1.Sheets[2].Cells[16, 7].Formula = "R17C7/(R17C5+R17C7+R17C9+R17C11+R17C13)";
            fpSpread1.Sheets[2].Cells[16, 9].Formula = "R17C9/(R17C5+R17C7+R17C9+R17C11+R17C13)";
            fpSpread1.Sheets[2].Cells[16, 11].Formula = "R17C11/(R17C5+R17C7+R17C9+R17C11+R17C13)";
            fpSpread1.Sheets[2].Cells[16, 13].Formula = "R17C13/(R17C5+R17C7+R17C9+R17C11+R17C13)";


        }
        //rownum从零开始的
        private void setsheet2value(FarPoint.Win.Spread.SheetView obj,IList list,int rownum)
        {
            int num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0;
            foreach (PSP_Substation_Info ps in list)
            {
                if (ps.L9 != 0)
                {
                    double rzb = Convert.ToDouble(ps.L2 / ps.L9);
                    if (rzb <=1.6)
                    {
                        num1 += 1;
                    }
                    if (rzb >= 1.6 && rzb < 1.8)
                    {
                        num2++;
                    }
                    if (rzb >= 1.8 && rzb < 2.0)
                    {
                        num3++;
                    }
                    if (rzb >= 2.0 && rzb < 2.2)
                    {
                        num4++;
                    }
                    if (rzb >= 2.2)
                    {
                        num5++;
                    }
                }

            }
            CreateSheetView(obj, 1, 1, rownum, 4, num1);
            CreateSheetView(obj, 1, 1, rownum, 6, num2);
            CreateSheetView(obj, 1, 1, rownum, 8, num3);
            CreateSheetView(obj, 1, 1, rownum, 10, num4);
            CreateSheetView(obj, 1, 1, rownum, 12, num5);
            obj.Cells[rownum, 5].Formula = "R" + (rownum + 1).ToString() + "C5/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13)";
            obj.Cells[rownum, 7].Formula = "R" + (rownum + 1).ToString() + "C7/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13)";
            obj.Cells[rownum, 9].Formula = "R" + (rownum + 1).ToString() + "C9/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13)";
            obj.Cells[rownum, 11].Formula = "R" + (rownum + 1).ToString() + "C11/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13)";
            obj.Cells[rownum, 13].Formula = "R" + (rownum + 1).ToString() + "C13/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13)";

        }
        private void rzbfbbtable()
        {
            fpSpread1.Sheets[2].RowCount = 0;
            fpSpread1.Sheets[2].ColumnCount = 0;
            fpSpread1.Sheets[2].RowCount = 17;
            fpSpread1.Sheets[2].ColumnCount = 14;
            fpSpread1.Sheets[2].SetValue(0, 0, "2010年铜陵市容载比分布表");
            fpSpread1.Sheets[2].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[2].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[2].Cells[0, 0].ColumnSpan = 14;
            for (int i = 0; i <10;i+=2 )
            {
                fpSpread1.Sheets[2].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[2].Rows[1].CellType = texttype;
            fpSpread1.Sheets[2].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 1, 2, "供电企业个数（个）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[2], 2, "供电企业个数（个）");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 1, 3, "电压等级（KV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[2], 3, "电压等级（KV）");
            CreateSheetView(fpSpread1.Sheets[2], 1, 2, 1, 4, "1.6以下");
            CreateSheetView(fpSpread1.Sheets[2], 1, 2, 1, 6, "1.6～1.8");
            CreateSheetView(fpSpread1.Sheets[2], 1, 2, 1, 8, "1.8～2.0");
            CreateSheetView(fpSpread1.Sheets[2], 1, 2, 1, 10, "2.0～2.2");
            CreateSheetView(fpSpread1.Sheets[2], 1, 2, 1, 12, "2.2以上");
            for (int i = 0; i <10;i+=2 )
            {
                CreateSheetView(fpSpread1.Sheets[2], 1, 1, 2, 4+i, "个数");
                CreateSheetView(fpSpread1.Sheets[2], 1, 1, 2, 5+i, "占比");
            }
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 3, 0, "1");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 3, 1, "市辖供电区");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 5, 0, "2");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 5, 1, "县级供电区");

            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 7, 0, "2.1");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 7, 1, "其中：直供直管");

            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 9, 0, "2.2");
             CreateSheetView(fpSpread1.Sheets[2], 2, 1, 9, 1, "控股");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 11, 0, "2.3");
             CreateSheetView(fpSpread1.Sheets[2], 2, 1, 11, 1, "参股");

            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 13, 0, "2.4");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 13, 1, "代管");
            CreateSheetView(fpSpread1.Sheets[2], 2, 1, 15, 0, "3");
             CreateSheetView(fpSpread1.Sheets[2], 2, 1, 15, 1, "全地区");
            for (int i=0;i<14;i+=2)
            {
                 CreateSheetView(fpSpread1.Sheets[2], 1, 1,3+i, 3, "110（66）");
                 CreateSheetView(fpSpread1.Sheets[2], 1, 1,4+i, 3, "35");
            }
            Sheet_GridandColor(fpSpread1.Sheets[2], 17, 14);
        }

        //变电站最大负载率分布表
        private void submaxfz()
        {
        //表头
            submaxfztable();
            //数据
            string con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
            IList list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 4);

            con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 5);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级直供直管'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 7);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级直供直管'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 8);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级控股'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 9);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级控股'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 10);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级参股'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 11);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级参股'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 12);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级代管'AND L1='110'OR L1='66'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 13);

            con = "AreaID='" + ProjectUID + "'AND DQ='县级代管'AND L1='35'";
            list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            setsheet3value(fpSpread1.Sheets[3], list1, 14);

            fpSpread1.Sheets[3].Cells[5, 4].Formula = "R8C5+R10C5+R12C5+R14C5";
            fpSpread1.Sheets[3].Cells[5, 6].Formula = "R8C7+R10C7+R12C7+R14C7";
            fpSpread1.Sheets[3].Cells[5, 8].Formula = "R8C9+R10C9+R12C9+R14C9";
            fpSpread1.Sheets[3].Cells[5, 10].Formula = "R8C11+R10C11+R12C11+R14C11";
            fpSpread1.Sheets[3].Cells[5, 12].Formula = "R8C13+R10C13+R12C13+R14C13";
            fpSpread1.Sheets[3].Cells[5, 14].Formula = "R8C15+R10C15+R12C15+R14C15";
            fpSpread1.Sheets[3].Rows[5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[3].Rows[5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            fpSpread1.Sheets[3].Cells[5, 5].Formula = "R6C5/(R6C5+R6C7+R6C9+R6C11+R6C13+R6C15)";
            fpSpread1.Sheets[3].Cells[5, 7].Formula = "R6C7/(R6C5+R6C7+R6C9+R6C11+R6C13+R6C15)";
            fpSpread1.Sheets[3].Cells[5, 9].Formula = "R6C9/(R6C5+R6C7+R6C9+R6C11+R6C13+R6C15)";
            fpSpread1.Sheets[3].Cells[5, 11].Formula = "R6C11/(R6C5+R6C7+R6C9+R6C11+R6C13+R6C15)";
            fpSpread1.Sheets[3].Cells[5, 13].Formula = "R6C13/(R6C5+R6C7+R6C9+R6C11+R6C13+R6C15)";
            fpSpread1.Sheets[3].Cells[5, 15].Formula = "R6C15/(R6C5+R6C7+R6C9+R6C11+R6C13+R6C15)";
            fpSpread1.Sheets[3].Cells[5, 3].Formula = "R6C5+R6C7+R6C9+R6C11+R6C13+R6C15";


            fpSpread1.Sheets[3].Cells[6, 4].Formula = "R9C5+R11C5+R13C5+R15C5";
            fpSpread1.Sheets[3].Cells[6, 6].Formula = "R9C7+R11C7+R13C7+R15C7";
            fpSpread1.Sheets[3].Cells[6, 8].Formula = "R9C9+R11C9+R13C9+R15C9";
            fpSpread1.Sheets[3].Cells[6, 10].Formula = "R9C11+R11C11+R13C11+R15C11";
            fpSpread1.Sheets[3].Cells[6, 12].Formula = "R9C13+R11C13+R13C13+R15C13";
            fpSpread1.Sheets[3].Cells[6, 14].Formula = "R9C15+R11C15+R13C15+R15C15";

            fpSpread1.Sheets[3].Cells[6, 5].Formula = "R7C5/(R7C5+R7C7+R7C9+R7C11+R7C13+R7C15)";
            fpSpread1.Sheets[3].Cells[6, 7].Formula = "R7C7/(R7C5+R7C7+R7C9+R7C11+R7C13+R7C15)";
            fpSpread1.Sheets[3].Cells[6, 9].Formula = "R7C9/(R7C5+R7C7+R7C9+R7C11+R7C13+R7C15)";
            fpSpread1.Sheets[3].Cells[6, 11].Formula = "R7C11/(R7C5+R7C7+R7C9+R7C11+R7C13+R7C15)";
            fpSpread1.Sheets[3].Cells[6, 13].Formula = "R7C13/(R7C5+R7C7+R7C9+R7C11+R7C13+R7C15)";
            fpSpread1.Sheets[3].Cells[6, 15].Formula = "R7C15/(R7C5+R7C7+R7C9+R7C11+R7C13+R7C15)";
            fpSpread1.Sheets[3].Cells[6, 3].Formula = "R7C5+R7C7+R7C9+R7C11+R7C13+R7C15";
            //统计全地区
            fpSpread1.Sheets[3].Cells[15, 3].Formula = "R4C4+R6C4";
            fpSpread1.Sheets[3].Cells[15, 4].Formula = "R4C5+R6C5";
            fpSpread1.Sheets[3].Cells[15, 6].Formula = "R4C7+R6C7";
            fpSpread1.Sheets[3].Cells[15, 8].Formula = "R4C9+R6C9";
            fpSpread1.Sheets[3].Cells[15, 10].Formula = "R4C11+R6C11";
            fpSpread1.Sheets[3].Cells[15, 12].Formula = "R4C13+R6C13";
            fpSpread1.Sheets[3].Cells[15, 14].Formula = "R4C15+R6C15";
            fpSpread1.Sheets[3].Rows[15].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[3].Rows[15].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            fpSpread1.Sheets[3].Cells[15, 5].Formula = "R16C5/(R16C5+R16C7+R16C9+R16C11+R16C13+R16C15)";
            fpSpread1.Sheets[3].Cells[15, 7].Formula = "R16C7/(R16C5+R16C7+R16C9+R16C11+R16C13+R16C15)";
            fpSpread1.Sheets[3].Cells[15, 9].Formula = "R16C9/(R16C5+R16C7+R16C9+R16C11+R16C13+R16C15)";
            fpSpread1.Sheets[3].Cells[15, 11].Formula = "R16C11/(R16C5+R16C7+R16C9+R16C11+R16C13+R16C15)";
            fpSpread1.Sheets[3].Cells[15, 13].Formula = "R16C13/(R16C5+R16C7+R16C9+R16C11+R16C13+R16C15)";
            fpSpread1.Sheets[3].Cells[15, 15].Formula = "R16C15/(R16C5+R16C7+R16C9+R16C11+R16C13+R16C15)";

            fpSpread1.Sheets[3].Cells[16, 3].Formula = "R5C4+R7C4";
            fpSpread1.Sheets[3].Cells[16, 4].Formula = "R5C5+R7C5";
            fpSpread1.Sheets[3].Cells[16, 6].Formula = "R5C7+R7C7";
            fpSpread1.Sheets[3].Cells[16, 8].Formula = "R5C9+R7C9";
            fpSpread1.Sheets[3].Cells[16, 10].Formula = "R5C11+R7C11";
            fpSpread1.Sheets[3].Cells[16, 12].Formula = "R5C13+R7C13";
            fpSpread1.Sheets[3].Cells[16, 14].Formula = "R5C15+R7C15";

            fpSpread1.Sheets[3].Cells[16, 5].Formula = "R17C5/(R17C5+R17C7+R17C9+R17C11+R17C13+R17C15)";
            fpSpread1.Sheets[3].Cells[16, 7].Formula = "R17C7/(R17C5+R17C7+R17C9+R17C11+R17C13+R17C15)";
            fpSpread1.Sheets[3].Cells[16, 9].Formula = "R17C9/(R17C5+R17C7+R17C9+R17C11+R17C13+R17C15)";
            fpSpread1.Sheets[3].Cells[16, 11].Formula = "R17C11/(R17C5+R17C7+R17C9+R17C11+R17C13+R17C15)";
            fpSpread1.Sheets[3].Cells[16, 13].Formula = "R17C13/(R17C5+R17C7+R17C9+R17C11+R17C13+R17C15)";
            fpSpread1.Sheets[3].Cells[16, 15].Formula = "R17C15/(R17C5+R17C7+R17C9+R17C11+R17C13+R17C15)";


        }
        private void setsheet3value(FarPoint.Win.Spread.SheetView obj ,IList list,int rownum)
        {

            int num1 = 0, num2 = 0, num3 = 0, num4 = 0, num5 = 0,num6=0;
            foreach (PSP_Substation_Info ps in list)
            {
                if (ps.L2 != 0)
                {
                    double fzl = Convert.ToDouble(ps.L9 /ps.L2 );
                    if (fzl< 0.2)
                    {
                        num1 += 1;
                    }
                    if (fzl>= 0.2 && fzl< 0.4)
                    {
                        num2++;
                    }
                    if (fzl>= 0.4 && fzl< 0.6)
                    {
                        num3++;
                    }
                    if (fzl>= 0.6 && fzl< 0.8)
                    {
                        num4++;
                    }
                    if (fzl>= 0.8 && fzl< 1.0)
                    {
                        num5++;
                    }
                    if (fzl>= 1.0)
                    {
                        num6++;
                    }
                }

            }
            CreateSheetView(obj, 1, 1, rownum, 4, num1);
            CreateSheetView(obj, 1, 1, rownum, 6, num2);
            CreateSheetView(obj, 1, 1, rownum, 8, num3);
            CreateSheetView(obj, 1, 1, rownum, 10, num4);
            CreateSheetView(obj, 1, 1, rownum, 12, num5);
            CreateSheetView(obj, 1, 1, rownum, 14, num6);
            obj.Cells[rownum, 3].Formula = "R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15";
            obj.Cells[rownum, 5].Formula = "R" + (rownum + 1).ToString() + "C5/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 7].Formula = "R" + (rownum + 1).ToString() + "C7/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 9].Formula = "R" + (rownum + 1).ToString() + "C9/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 11].Formula = "R" + (rownum + 1).ToString() + "C11/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 13].Formula = "R" + (rownum + 1).ToString() + "C13/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 15].Formula = "R" + (rownum + 1).ToString() + "C15/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
        }
        private void submaxfztable()
        {
            fpSpread1.Sheets[3].RowCount = 0;
            fpSpread1.Sheets[3].ColumnCount = 0;
            fpSpread1.Sheets[3].RowCount = 17;
            fpSpread1.Sheets[3].ColumnCount = 16;
            fpSpread1.Sheets[3].SetValue(0, 0, "2010年铜陵市变电站最大负载率分布表");
            fpSpread1.Sheets[3].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[3].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[3].Cells[0, 0].ColumnSpan = 16;
            for (int i = 0; i < 12; i += 2)
            {
                fpSpread1.Sheets[3].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[3].Rows[1].CellType = texttype;
            fpSpread1.Sheets[3].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 1, 2, "电压等级（kV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[3], 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 1, 3, "变电站总数");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[3],3, "变电站总数");

            CreateSheetView(fpSpread1.Sheets[3], 1, 2, 1, 4, "0～20%");
            CreateSheetView(fpSpread1.Sheets[3], 1, 2, 1, 6, "21%～40%");
            CreateSheetView(fpSpread1.Sheets[3], 1, 2, 1, 8, "41%～60%");
            CreateSheetView(fpSpread1.Sheets[3], 1, 2, 1, 10, "61%～80%");
            CreateSheetView(fpSpread1.Sheets[3], 1, 2, 1, 12, "81%～100%");
            CreateSheetView(fpSpread1.Sheets[3], 1, 2, 1, 14, "100%以上");
            for (int i = 0; i < 12; i += 2)
            {
                CreateSheetView(fpSpread1.Sheets[3], 1, 1, 2, 4 + i, "座数");
                CreateSheetView(fpSpread1.Sheets[3], 1, 1, 2, 5 + i, "比例");
            }
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 3, 0, "1");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 3, 1, "市辖供电区");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 5, 0, "2");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 5, 1, "县级供电区");

            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 7, 0, "2.1");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 7, 1, "其中：直供直管");

            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 9, 0, "2.2");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 9, 1, "控股");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 11, 0, "2.3");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 11, 1, "参股");

            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 13, 0, "2.4");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 13, 1, "代管");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 15, 0, "3");
            CreateSheetView(fpSpread1.Sheets[3], 2, 1, 15, 1, "全地区");
            for (int i = 0; i < 14; i +=2)
            {
                CreateSheetView(fpSpread1.Sheets[3], 1, 1, 3 + i, 2, "110（66）");
                CreateSheetView(fpSpread1.Sheets[3], 1, 1, 4 + i, 2, "35");
            }
            Sheet_GridandColor(fpSpread1.Sheets[3], 17, 16);
        }
        //变电站最大负载率分布附表

        private void submaxfb()
        {
            //table
            submaxfbtable();
            //数据
#region 统计数据
            //获取数据
            Dictionary<string, IList> listcity110 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcity35 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry110 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry35 = new Dictionary<string, IList>();
            
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            foreach (PS_Table_AreaWH pa in list)
            {
                con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                IList list1  =Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                if (list1.Count!=0)
                {
                    listcity110.Add(pa.Title, list1);
                }
                con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                if (list1.Count != 0)
                {
                    listcity35.Add(pa.Title, list1);
                }
                con = "AreaID='" + ProjectUID + "'AND DQ!='市辖供电区'AND L1='110'OR L1='66'and AreaName='" + pa.Title + "'";
                list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                if (list1.Count != 0)
                {
                    listcountry110.Add(pa.Title, list1);
                }
                con = "AreaID='" + ProjectUID + "'AND DQ!='市辖供电区'AND L1='35'and AreaName='" + pa.Title + "'";
                list1 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
                if (list1.Count != 0)
                {
                    listcountry35.Add(pa.Title, list1);
                }
            }
            con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='110'OR L1='66'";
            IList listcityzon110 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
            con = "AreaID='" + ProjectUID + "'AND DQ='市辖供电区'AND L1='35'";
            IList listcityzon35 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);

            con = "AreaID='" + ProjectUID + "'AND DQ!='市辖供电区'AND L1='110'OR L1='66'";
            IList listcountryzon110 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);

            con = "AreaID='" + ProjectUID + "'AND DQ!='市辖供电区'AND L1='35'";
            IList listcountryzon35 = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", con);
          //市辖供电区
            int numcityi = 0; int numcountryi = 0;
            fpSpread1.Sheets[4].Rows.Add(3, 2);

            CreateSheetView(fpSpread1.Sheets[4], 2, 1, 3, 1, "合计");
            CreateSheetView(fpSpread1.Sheets[4], 1, 1, 3, 2, "110(66)");
            Subnum sbz = getsubvalue(listcityzon110);
            setsheet4value(fpSpread1.Sheets[4], sbz, 3);
            CreateSheetView(fpSpread1.Sheets[4], 1, 1, 4, 2, "35");
            sbz = getsubvalue(listcityzon35);
            setsheet4value(fpSpread1.Sheets[4], sbz, 4);
            foreach (PS_Table_AreaWH pa in list)
            {
                if (listcity110.ContainsKey(pa.Title) || listcity35.ContainsKey(pa.Title))
                {
                    numcityi += 2;
                    fpSpread1.Sheets[4].Rows.Add(5, 2);
                    CreateSheetView(fpSpread1.Sheets[4], 2, 1, 5, 1, pa.Title);
                    CreateSheetView(fpSpread1.Sheets[4], 1, 1, 5, 2, "110(66)");
                    CreateSheetView(fpSpread1.Sheets[4], 1, 1, 6, 2, "35");
                    if (listcity110.ContainsKey(pa.Title))
                    {
                        sbz = getsubvalue(listcity110[pa.Title]);
                        setsheet4value(fpSpread1.Sheets[4], sbz, 5);
                    }
                    if (listcity35.ContainsKey(pa.Title))
                    {
                        sbz = getsubvalue(listcity35[pa.Title]);
                        setsheet4value(fpSpread1.Sheets[4], sbz, 6);
                    }
                }
                
            }
            CreateSheetView(fpSpread1.Sheets[4], 2 + numcityi, 1, 3, 0, "市辖供电区");
            //县供电区
            fpSpread1.Sheets[4].Rows.Add(5+numcityi, 2);

            CreateSheetView(fpSpread1.Sheets[4], 2, 1, 5+numcityi, 1, "合计");
            CreateSheetView(fpSpread1.Sheets[4], 1, 1, 5+numcityi, 2, "110(66)");
            sbz = getsubvalue(listcountryzon35);
            setsheet4value(fpSpread1.Sheets[4], sbz, 5+numcityi);
            CreateSheetView(fpSpread1.Sheets[4], 1, 1, 6+numcityi, 2, "35");
            sbz = getsubvalue(listcountryzon35);
            setsheet4value(fpSpread1.Sheets[4], sbz, 6+numcityi);
            foreach (PS_Table_AreaWH pa in list)
            {
                if (listcountry110.ContainsKey(pa.Title) || listcountry35.ContainsKey(pa.Title))
                {
                    numcountryi += 2;
                    fpSpread1.Sheets[4].Rows.Add(7+numcityi, 2);
                    CreateSheetView(fpSpread1.Sheets[4], 2, 1, 7 + numcityi, 1, pa.Title);
                    CreateSheetView(fpSpread1.Sheets[4], 1, 1, 7 + numcityi, 2, "110(66)");
                    CreateSheetView(fpSpread1.Sheets[4], 1, 1, 8 + numcityi, 2, "35");
                    if (listcountry110.ContainsKey(pa.Title))
                    {
                        sbz = getsubvalue(listcountry110[pa.Title]);
                        setsheet4value(fpSpread1.Sheets[4], sbz, 7+numcityi);
                    }
                    if (listcountry35.ContainsKey(pa.Title))
                    {
                        sbz = getsubvalue(listcountry35[pa.Title]);
                        setsheet4value(fpSpread1.Sheets[4], sbz, 8+numcityi);
                    }
                }

            }
            CreateSheetView(fpSpread1.Sheets[4], 2 + numcountryi, 1, 5 + numcityi, 0, "县级供电区");
            fpSpread1.Sheets[4].Rows.Remove(7 + numcityi + numcountryi, 1);
            Sheet_GridandColor(fpSpread1.Sheets[4], 7 + numcountryi + numcityi, 16);
            
#endregion
           

        }
        struct Subnum
        {
            //public int zonsum;
            public int sum1;
            public int sum2;
            public int sum3;
            public int sum4;
            public int sum5;
            public int sum6;
            public Subnum(int _sum1,int _sum2,int _sum3,int _sum4,int _sum5,int _sum6)
            {
                //zonsum = _zonsum;
                sum1 = _sum1;
                sum2 = _sum2;
                sum3 = _sum3;
                sum4 = _sum4;
                sum5 = _sum5;
                sum6 = _sum6;
            }
        }
        private void setsheet4value(FarPoint.Win.Spread.SheetView obj,Subnum suz,int rownum)
        {
            CreateSheetView(obj, 1, 1, rownum, 4, suz.sum1);
            CreateSheetView(obj, 1, 1, rownum, 6, suz.sum2);
            CreateSheetView(obj, 1, 1, rownum, 8, suz.sum3);
            CreateSheetView(obj, 1, 1, rownum, 10, suz.sum4);
            CreateSheetView(obj, 1, 1, rownum, 12, suz.sum5);
            CreateSheetView(obj, 1, 1, rownum, 14, suz.sum6);
            obj.Cells[rownum, 3].Formula = "R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15";
            
            obj.Cells[rownum, 5].Formula = "R" + (rownum + 1).ToString() + "C5/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 7].Formula = "R" + (rownum + 1).ToString() + "C7/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 9].Formula = "R" + (rownum + 1).ToString() + "C9/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 11].Formula = "R" + (rownum + 1).ToString() + "C11/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum,13].Formula = "R" + (rownum + 1).ToString() + "C13/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 15].Formula = "R" + (rownum + 1).ToString() + "C15/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
        }
        private Subnum getsubvalue(IList list)
        {
            Subnum sb = new Subnum(0,0,0,0,0,0);
            foreach(PSP_Substation_Info ps in list)
            {
                
                if (ps.L2!=0)
                {
                    double fzl = Convert.ToDouble(ps.L9 / ps.L2);
                    if (fzl>=0&&fzl < 0.2)
                    {
                       sb.sum1 += 1;
                    }
                    if (fzl >= 0.2 && fzl < 0.4)
                    {
                        sb.sum2++;
                    }
                    if (fzl >= 0.4 && fzl < 0.6)
                    {
                        sb.sum3++;
                    }
                    if (fzl >= 0.6 && fzl < 0.8)
                    {
                        sb.sum4++;
                    }
                    if (fzl >= 0.8 && fzl < 1.0)
                    {
                        sb.sum5++;
                    }
                    if (fzl >= 1.0)
                    {
                        sb.sum6++;
                    }
                }
            }
            return sb;
        }
        private void submaxfbtable()
        {
            fpSpread1.Sheets[4].RowCount = 0;
            fpSpread1.Sheets[4].ColumnCount = 0;
            fpSpread1.Sheets[4].RowCount =4;
            fpSpread1.Sheets[4].ColumnCount = 16;
            fpSpread1.Sheets[4].SetValue(0, 0, "2010年铜陵市变电站最大负载率分布表");
            fpSpread1.Sheets[4].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[4].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[4].Cells[0, 0].ColumnSpan = 16;
            for (int i = 0; i < 12; i+=2)
            {
                fpSpread1.Sheets[4].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[4].Rows[1].CellType = texttype;
            fpSpread1.Sheets[4].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[4], 2, 1, 1, 0, "分区类型");
            CreateSheetView(fpSpread1.Sheets[4], 2, 1, 1, 1, "分区名称");
            CreateSheetView(fpSpread1.Sheets[4], 2, 1, 1, 2, "电压等级（kV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[4], 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[4], 2, 1, 1, 3, "变电站总数");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[4], 3, "变电站总数");

            CreateSheetView(fpSpread1.Sheets[4], 1, 2, 1, 4, "0～20%");
            CreateSheetView(fpSpread1.Sheets[4], 1, 2, 1, 6, "21%～40%");
            CreateSheetView(fpSpread1.Sheets[4], 1, 2, 1, 8, "41%～60%");
            CreateSheetView(fpSpread1.Sheets[4], 1, 2, 1, 10, "61%～80%");
            CreateSheetView(fpSpread1.Sheets[4], 1, 2, 1, 12, "81%～100%");
            CreateSheetView(fpSpread1.Sheets[4], 1, 2, 1, 14, "100%以上");
            for (int i = 0; i < 12; i+= 2)
            {
                CreateSheetView(fpSpread1.Sheets[4], 1, 1, 2, 4 + i, "座数");
                CreateSheetView(fpSpread1.Sheets[4], 1, 1, 2, 5 + i, "比例");
            }
           
        }
        //中压配电网主要运行指标
        private void zypdzb()
        {
            //table
            zypdzbtable();
            //数据
            setsheet5Value("('市辖供电区')", 3);
            setsheet5Value("('县级直供直管')", 5);
            setsheet5Value("('县级控股')", 6);
            setsheet5Value("('县级参股')", 7);
            setsheet5Value("('县级代管')", 8);
            fpSpread1.Sheets[5].Cells[4, 2].Formula = "R6C3+R7C3+R8C3+R9C3";
            setsheet5Value("('县级直供直管','县级控股','县级参股','县级代管')",4);
            fpSpread1.Sheets[5].Cells[4, 2].Formula = "R4C3+R5C3";
          setsheet5Value("('市辖供电区','县级直供直管','县级控股','县级参股','县级代管')",9);
        }
        private void setsheet5Value(string title,int rownum)
        {
            string con = "(ProjectID='" + ProjectUID + "')and (RateVolt in('10','20','6')) and (DQ in" + title + ")and (LineType2='公用')AND (HgFlag is null or HgFlag='合格')and type='05'";
            int hgts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, rownum, 2, hgts);
            con = "ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6') and DQ in" + title + "and LineType2='公用'and type='05'";
            int zts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_CountAll", con));
            if (zts != 0)
            {
                CreateSheetView(fpSpread1.Sheets[5], 1, 1, rownum, 3, hgts / zts);
            }
            else
                CreateSheetView(fpSpread1.Sheets[5], 1, 1, rownum, 3, 0);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6') and DQ in" + title + "and LineType2='公用'and type='05'";
            IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            double fzl=0.0;int linenum=0;
            foreach (PSPDEV psp in list1)
            {
               double burden =(double) psp.Burthen;
               double factI = psp.FactI;
                if (burden!=0)
                {
                    fzl+=factI/burden;
                }
                linenum++;
            }
            
                CreateSheetView(fpSpread1.Sheets[5], 1, 1, rownum, 4, fzl/linenum);

                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6') and DQ in" + title + "and LineType2='公用'and type='05'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                double pbfzl = 0.0; int pblinenum = 0;
            foreach (PSPDEV ps in list1)
            {
                con = "a.ProjectID='" + ProjectUID + "'and a.SUID='"+ps.SUID+"'";
                double rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSPDEV_SUMNum2_type50-59", con));
                if (rl!=0)
                {
                    pbfzl += ps.FactI / rl;
                }
                pblinenum++;
            }
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, rownum, 5, pbfzl / pblinenum); 
           

        }
      
        private void zypdzbtable()
        {
            fpSpread1.Sheets[5].RowCount = 0;
            fpSpread1.Sheets[5].ColumnCount = 0;
            fpSpread1.Sheets[5].RowCount = 10;
            fpSpread1.Sheets[5].ColumnCount = 6;
            fpSpread1.Sheets[5].SetValue(0, 0, "2010年铜陵市中压配电网主要运行指标");
            fpSpread1.Sheets[5].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[5].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[5].Cells[0, 0].ColumnSpan = 6;
            fpSpread1.Sheets[5].Columns[3].CellType = percentcelltypes;
            fpSpread1.Sheets[5].Rows[1].CellType = texttype;
            fpSpread1.Sheets[5].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[5], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[5], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[5], 1, 2, 1, 2, "满足N-1的线路");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[5], 2, "满足N-1的线路");
            CreateSheetView(fpSpread1.Sheets[5], 2, 1, 1, 4, "线路最大负载率平均值（%）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[5],  4, "线路最大负载率平均值（%）");
            CreateSheetView(fpSpread1.Sheets[5], 2, 1, 1, 5, "配变负载率平均值（%）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[5], 5, "配变负载率平均值（%）");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 2, 2, "条数（条）");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 2, 3, "比例");


            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 3, 0, "1");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 3, 1, "市辖供电区");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1,4, 0, "2");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 4, 1, "县级供电区");

            CreateSheetView(fpSpread1.Sheets[5], 1, 1,5, 0, "2.1");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 5, 1, "其中：直供直管");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[5], 1, "其中：直供直管");
            for (int i = 0; i < 4;i++ )
            {
                fpSpread1.Sheets[5].Cells[5 + i, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            }
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 6, 0, "2.2");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 6, 1, "控股");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 7, 0, "2.3");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 7, 1, "参股");

            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 8, 0, "2.4");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 8, 1, "代管");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 9, 0, "3");
            CreateSheetView(fpSpread1.Sheets[5], 1, 1, 9, 1, "全地区");
            Sheet_GridandColor(fpSpread1.Sheets[5], 10, 6);

        }
        //中压配电网主要运行指标附表
        private void zypdzbfb()
        {
          //table
            zypdzbfbtable();
            //数据
            Dictionary<string, IList> listcity = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry = new Dictionary<string, IList>();
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            foreach (PS_Table_AreaWH pa in list)
            {
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6') AND AreaID='"+pa.ID+"'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6') AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry.Add(pa.Title, list1);
                }
              
            }
            //另外总的单独来弄
            con="WHERE ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6')and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt in('10','20','6')and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcountry = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            //增加市辖的统计
            fpSpread1.Sheets[6].Rows.Add(3, 1);
            CreateSheetView(fpSpread1.Sheets[6], 1, 1, 3, 1, "合计");
            setsheet6value(fpSpread1.Sheets[6], listzcity, 3);
            int citynum=0,countrynum=0;
            foreach (PS_Table_AreaWH pa in list)
            {
                if (listcity.ContainsKey(pa.Title))
                {
                    citynum++;
                    fpSpread1.Sheets[6].Rows.Add(4, 1);
                    CreateSheetView(fpSpread1.Sheets[6], 1, 1, 4 , 1,pa.Title);
                    setsheet6value(fpSpread1.Sheets[6], listcity[pa.Title], 4);
                }
            }
            CreateSheetView(fpSpread1.Sheets[6],1+citynum,1,3,0,"市辖供电区");
            //增加农村的统计
            fpSpread1.Sheets[6].Rows.Add(4 + citynum, 1);
            CreateSheetView(fpSpread1.Sheets[6], 1, 1, 4 + citynum, 1, "合计");
            setsheet6value(fpSpread1.Sheets[6], listzcountry, 4 + citynum);
            foreach (PS_Table_AreaWH pa in list)
            {
                if (listcountry.ContainsKey(pa.Title))
                {
                    countrynum++;
                    fpSpread1.Sheets[6].Rows.Add(5 + citynum,1);
                    CreateSheetView(fpSpread1.Sheets[6], 1, 1, 5+citynum, 1, pa.Title);
                    setsheet6value(fpSpread1.Sheets[6], listcountry[pa.Title], 5 + citynum);
                }
            }
            CreateSheetView(fpSpread1.Sheets[6], 1 + countrynum, 1, 4 + citynum, 0, "县级供电区");
            fpSpread1.Sheets[6].Rows.Remove(5+citynum+countrynum,1);
            Sheet_GridandColor(fpSpread1.Sheets[6],5+citynum+countrynum,9);
         
        }
        private void setsheet6value(FarPoint.Win.Spread.SheetView obj,IList list,int rownum)
        {
            int hgts = 0, hgzts = 0; double linefzl=0.0; double pbfzl=0.0;
            foreach (PSPDEV ps in list)
            {
                if (ps.HgFlag!="不合格")
                {
                    hgts++;
                }
                hgzts++;
                double facti  = ps.FactI;
                double burden =Convert.ToDouble(ps.Burthen);
                if (burden!=0)
                {
                    linefzl += facti / burden;
                }
              string  con = "a.ProjectID='" + ProjectUID + "'and a.SUID='" + ps.SUID + "'";
                double rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSPDEV_SUMNum2_type50-59", con));
                if (rl != 0)
                {
                    pbfzl += ps.FactI / rl;
                }
               
            }
            if (hgzts!=0)
            {
                CreateSheetView(obj, 1, 1, rownum, 5, hgts);
                CreateSheetView(obj, 1, 1, rownum, 6, hgts / hgzts);
                CreateSheetView(obj, 1, 1, rownum, 7, linefzl / hgzts);
                CreateSheetView(obj, 1, 1, rownum, 8, pbfzl / hgzts);
            }
           

        }
        private void zypdzbfbtable()
        {
            fpSpread1.Sheets[6].RowCount = 0;
            fpSpread1.Sheets[6].ColumnCount = 0;
            fpSpread1.Sheets[6].RowCount = 4;
            fpSpread1.Sheets[6].ColumnCount = 9;
            fpSpread1.Sheets[6].SetValue(0, 0, "2010年铜陵市中压配电网主要运行指标");
            fpSpread1.Sheets[6].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[6].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[6].Cells[0, 0].ColumnSpan = 9;
            fpSpread1.Sheets[6].Columns[2].CellType = percentcelltypes;
            fpSpread1.Sheets[6].Columns[3].CellType = percentcelltypes;
            fpSpread1.Sheets[6].Columns[4].CellType = percentcelltypes;
            fpSpread1.Sheets[6].Columns[6].CellType = percentcelltypes;
            fpSpread1.Sheets[6].Columns[7].CellType = percentcelltypes;
            fpSpread1.Sheets[6].Columns[8].CellType = percentcelltypes;
            fpSpread1.Sheets[6].Rows[1].CellType = texttype;
            fpSpread1.Sheets[6].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 0, "分区类型");
            CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 1, "分区名称");
            CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 2, "供电可靠率(RS-3)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[6], 2, "供电可靠率(RS-3)");
            CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 3, "10(20)kV及以下线损率(%)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[6], 3, "10(20)kV及以下线损率(%)");

            CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 4, "综合电压合格率(%)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[6],4, "综合电压合格率(%)");
            CreateSheetView(fpSpread1.Sheets[6], 1, 2, 1, 5, "满足N-1的线路");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[6], 5, "满足N-1的线路");
             CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 7, "线路最大负荷率平均值(%)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[6], 7, "线路最大负荷率平均值(%)");
            CreateSheetView(fpSpread1.Sheets[6], 2, 1, 1, 8, "配变负载率平均值");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[6], 8, "配变负载率平均值");
            CreateSheetView(fpSpread1.Sheets[6], 1, 1, 2, 5, "条数(条)");

        }
        //中压配变负载率分布表
        private void zypdfzl()
        {
         //table
            zypdfzltable();
            //数据
            
            int rownum=0;
            //市辖供电区
            rownum=getrownum("('市辖供电区')",3);
            if (rownum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],rownum,1,3,0,"1");
                CreateSheetView(fpSpread1.Sheets[7],rownum,1,3,1,"市辖供电区");
            }
            else
            {
                rownum=1;
               CreateSheetView(fpSpread1.Sheets[7],rownum,1,3,0,"1");
                CreateSheetView(fpSpread1.Sheets[7],rownum,1,3,1,"市辖供电区");
            }
            
           //县级供电区
           int xiannum=0;
          xiannum=getrownum("('县级直供直管','县级控股','县级参股','县级代管')",3+rownum);
            if (xiannum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],xiannum,1,3+rownum,0,"2");
                CreateSheetView(fpSpread1.Sheets[7],xiannum,1,3+rownum,1,"县级供电区");
            }
            else
            {
                xiannum=1;
               CreateSheetView(fpSpread1.Sheets[7],xiannum,1,3+rownum,0,"2");
                CreateSheetView(fpSpread1.Sheets[7],xiannum,1,3+rownum,1,"县级供电区");
            }
            //直供直管
             int zznum=0;
             zznum=getrownum("('县级直供直管')",3+rownum+xiannum);
            if (zznum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],zznum,1,3+rownum+xiannum,0,"2.1");
                CreateSheetView(fpSpread1.Sheets[7],zznum,1,3+rownum+xiannum,1,"直供直管");
            }
            else
            {
                zznum=1;
               CreateSheetView(fpSpread1.Sheets[7],zznum,1,3+rownum+xiannum,0,"2.1");
                CreateSheetView(fpSpread1.Sheets[7],zznum,1,3+rownum+xiannum,1,"直供直管");
            }
            //控股

             int kgnum=0;
             kgnum=getrownum("('县级控股')",3+rownum+xiannum+zznum);
            if (kgnum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],kgnum,1,3+rownum+xiannum+zznum,0,"2.2");
                CreateSheetView(fpSpread1.Sheets[7],kgnum,1,3+rownum+xiannum+zznum,1,"县级控股");
            }
            else
            {
                kgnum=1;
               CreateSheetView(fpSpread1.Sheets[7],kgnum,1,3+rownum+xiannum+zznum,0,"2.2");
                CreateSheetView(fpSpread1.Sheets[7],kgnum,1,3+rownum+xiannum+zznum,1,"县级控股");
            }
            //参股
               int cgnum=0;
             cgnum=getrownum("('县级参股')",3+rownum+xiannum+zznum+kgnum);
            if (cgnum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],cgnum,1,3+rownum+xiannum+zznum+kgnum,0,"2.3");
                CreateSheetView(fpSpread1.Sheets[7],cgnum,1,3+rownum+xiannum+zznum+kgnum,1,"县级参股");
            }
            else
            {
                cgnum=1;
               CreateSheetView(fpSpread1.Sheets[7],cgnum,1,3+rownum+xiannum+zznum+kgnum,0,"2.3");
                CreateSheetView(fpSpread1.Sheets[7],cgnum,1,3+rownum+xiannum+zznum+kgnum,1,"县级参股");
            }
            //代管
             int dgnum=0;
             dgnum=getrownum("('县级代管')",3+rownum+xiannum+zznum+kgnum+cgnum);
            if (dgnum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],dgnum,1,3+rownum+xiannum+zznum+kgnum+cgnum,0,"2.4");
                CreateSheetView(fpSpread1.Sheets[7],dgnum,1,3+rownum+xiannum+zznum+kgnum+cgnum,1,"县级代管");
            }
            else
            {
                dgnum=1;
               CreateSheetView(fpSpread1.Sheets[7],dgnum,1,3+rownum+xiannum+zznum+kgnum+cgnum,0,"2.4");
                CreateSheetView(fpSpread1.Sheets[7],dgnum,1,3+rownum+xiannum+zznum+kgnum+cgnum,1,"县级代管");
            }
            //全地区
             int qqnum=0;
             qqnum=getrownum("('市辖供电区','县级直供直管','县级控股','县级参股','县级代管')",3+rownum+xiannum+zznum+kgnum+cgnum+dgnum);
            if (dgnum>0)
            {
                CreateSheetView(fpSpread1.Sheets[7],qqnum,1,3+rownum+xiannum+zznum+kgnum+cgnum+dgnum,0,"3");
                CreateSheetView(fpSpread1.Sheets[7],qqnum,1,3+rownum+xiannum+zznum+kgnum+cgnum+dgnum,1,"全地区总计");
            }
            else
            {
                qqnum=1;
               CreateSheetView(fpSpread1.Sheets[7],qqnum,1,3+rownum+xiannum+zznum+kgnum+cgnum+dgnum,0,"2.4");
                CreateSheetView(fpSpread1.Sheets[7],qqnum,1,3+rownum+xiannum+zznum+kgnum+cgnum+dgnum,1,"全地区总计");
            }
            //控制样式
            fpSpread1.Sheets[7].Rows.Remove(3+rownum+xiannum+zznum+kgnum+cgnum+dgnum+qqnum,1);
            Sheet_GridandColor(fpSpread1.Sheets[7],3+rownum+xiannum+zznum+kgnum+cgnum+dgnum+qqnum,16);
        }
        private int getrownum(string title,int rownum)
        {
            string con="WHERE ProjectID='" + ProjectUID + "'and RateVolt='20'and LineType2='公用'and type='05'AND DQ in"+title;
            IList list20= Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            int num=0;
            fpSpread1.Sheets[7].Rows.Add(rownum,1);
            if (list20.Count>0)
            {
               
                CreateSheetView(fpSpread1.Sheets[7],1,1,rownum,2,"20");
                setsheet7value(fpSpread1.Sheets[7],list20,rownum); 
                num+=1;
            }
            con="WHERE ProjectID='" + ProjectUID + "'and RateVolt='10'and LineType2='公用'and type='05'AND DQ in"+title;
            IList list10=Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            fpSpread1.Sheets[7].Rows.Add(rownum+num,1);
            if (list10.Count>0)
            {
                
                 CreateSheetView(fpSpread1.Sheets[7],1,1,rownum+num,2,"10");
                setsheet7value(fpSpread1.Sheets[7],list20,rownum+num);
                num++;
            }
             con="WHERE ProjectID='" + ProjectUID + "'and RateVolt='6'and LineType2='公用'and type='05'AND DQ in"+title;
            IList list6=Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            fpSpread1.Sheets[7].Rows.Add(rownum+num,1);
            if (list6.Count>0)
            {
                
                 CreateSheetView(fpSpread1.Sheets[7],1,1,rownum+num,2,"6");
                setsheet7value(fpSpread1.Sheets[7],list6,rownum+num);
                num++;
            }
         return num;

        }
        private void setsheet7value(FarPoint.Win.Spread.SheetView obj,IList list,int rownum)
        {
           double fh=0.0;double rl=0.0;int sum1=0,sum2=0,sum3=0,sum4=0,sum5=0,sum6=0;
            foreach (PSPDEV ps in list)
            {
                fh=Convert.ToDouble(ps.Burthen);
                  string  con = "a.ProjectID='" + ProjectUID + "'and a.SUID='" + ps.SUID + "'";
                rl= Convert.ToDouble(Services.BaseService.GetObject("SelectPSPDEV_SUMNum2_type50-59", con));
                
                if (rl!=0)
                {
                    double fzl=fh/rl;
                      con = "a.ProjectID='" + ProjectUID + "'and a.SUID='" + ps.SUID + "'and b.Type in('50','51','52')";
                       int ts=Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_SUMFlag", con));
                    if (fzl>=0&&fzl<0.2)
                    {
                         
                        sum1+=ts;
                    }
                    if (fzl>=0.2&&fzl<0.4)
                    {
                        sum2+=ts;
                    }
                    if (fzl>=0.4&&fzl<0.6)
                    {
                        sum3+=ts;
                    }
                    if (fzl>=0.6&&fzl<0.8)
                    {
                        sum4+=ts;
                    }
                    if (fzl>=0.8&&fzl<1)
                    {
                        sum5+=ts;
                    }
                    if (fzl>=1)
                    {
                        sum6+=ts;
                    }
                }

            }
            CreateSheetView(obj,1,1,rownum,4,sum1);
            CreateSheetView(obj,1,1,rownum,6,sum2);
            CreateSheetView(obj,1,1,rownum,8,sum3);
            CreateSheetView(obj,1,1,rownum,10,sum4);
            CreateSheetView(obj,1,1,rownum,12,sum5);
            CreateSheetView(obj,1,1,rownum,14,sum6);
            obj.Cells[rownum,3].Formula="R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15";
            obj.Cells[rownum,5].Formula="R"+(rownum+1).ToString()+"C5/(R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15)";
            obj.Cells[rownum,7].Formula="R"+(rownum+1).ToString()+"C7/(R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15)";
            obj.Cells[rownum,9].Formula="R"+(rownum+1).ToString()+"C9/(R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15)";
            obj.Cells[rownum,11].Formula="R"+(rownum+1).ToString()+"C11/(R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15)";
            obj.Cells[rownum,13].Formula="R"+(rownum+1).ToString()+"C13/(R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15)";
            obj.Cells[rownum,15].Formula="R"+(rownum+1).ToString()+"C15/(R"+(rownum+1).ToString()+"C5+R"+(rownum+1).ToString()+"C7+R"+(rownum+1).ToString()+"C9+R"+(rownum+1).ToString()+"C11+R"+(rownum+1).ToString()+"C13+R"+(rownum+1).ToString()+"C15)";

        }
        private void zypdfzltable()
        {
             fpSpread1.Sheets[7].RowCount = 0;
            fpSpread1.Sheets[7].ColumnCount = 0;
            fpSpread1.Sheets[7].RowCount = 4;
            fpSpread1.Sheets[7].ColumnCount = 16;
            fpSpread1.Sheets[7].SetValue(0, 0, "2010年铜陵市中压配变最大负载率分布表");
            fpSpread1.Sheets[7].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[7].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[7].Cells[0, 0].ColumnSpan = 16;
            for (int i = 0; i < 12; i += 2)
            {
                fpSpread1.Sheets[7].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[7].Rows[1].CellType = texttype;
            fpSpread1.Sheets[7].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[7], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[7], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[7], 2, 1, 1, 2, "电压等级（kV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[7], 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[7], 2, 1, 1, 3, "变电站总数");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[7],3, "变电站总数");

            CreateSheetView(fpSpread1.Sheets[7], 1, 2, 1, 4, "0～20%");
            CreateSheetView(fpSpread1.Sheets[7], 1, 2, 1, 6, "21%～40%");
            CreateSheetView(fpSpread1.Sheets[7], 1, 2, 1, 8, "41%～60%");
            CreateSheetView(fpSpread1.Sheets[7], 1, 2, 1, 10, "61%～80%");
            CreateSheetView(fpSpread1.Sheets[7], 1, 2, 1, 12, "81%～100%");
            CreateSheetView(fpSpread1.Sheets[7], 1, 2, 1, 14, "100%以上");
            for (int i = 0; i < 12; i += 2)
            {
                CreateSheetView(fpSpread1.Sheets[7], 1, 1, 2, 4 + i, "台数");
                CreateSheetView(fpSpread1.Sheets[7], 1, 1, 2, 5 + i, "比例");
            }
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 3, 0, "1");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 3, 1, "市辖供电区");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 4, 0, "2");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 4, 1, "县级供电区");

            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 5, 5, "2.1");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 5, 1, "其中：直供直管");

            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 6, 0, "2.2");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 6, 1, "控股");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 7, 0, "2.3");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 7, 1, "参股");

            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 8, 0, "2.4");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 8, 1, "代管");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 9, 0, "3");
            //CreateSheetView(fpSpread1.Sheets[7], 1, 1, 9, 1, "全地区");
            //for (int i = 0; i < 7; i ++)
            //{
            //    CreateSheetView(fpSpread1.Sheets[7], 1, 1, 3 + i, 2, "10");
                
            //}
            //Sheet_GridandColor(fpSpread1.Sheets[7], 10, 16);
        }
        //中压配电网配变负载率分布附表
        private void zypdfb()
        {
   //table
            zypdfbtable();
            //获取数据
            Dictionary<string, IList> listcity20 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcity10 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcity6 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry20 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry10 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry6 = new Dictionary<string, IList>();
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            foreach (PS_Table_AreaWH pa in list)
            {
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='20' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity20.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='10' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity10.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='6' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity6.Add(pa.Title, list1);
                }

                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='20' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry20.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='10' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry10.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='6' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry6.Add(pa.Title, list1);
                }
            }
            con="WHERE ProjectID='" + ProjectUID + "'and RateVolt ='20' and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity20 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
             con="WHERE ProjectID='" + ProjectUID + "'and RateVolt ='10' and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity10 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
             con="WHERE ProjectID='" + ProjectUID + "'and RateVolt ='6' and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity6= Services.BaseService.GetList("SelectPSPDEVByCondition", con);

            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='20' and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
            IList listzcountry20 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='10' and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
            IList listzcountry10 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='6' and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
            IList listzcountry6 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            //合计
            int cithj = 0;
            cithj = getztrownum(listzcity20, listzcity10, listzcity6, 3);
            //地区
            int citdq=0;
             foreach (PS_Table_AreaWH pa in list)
             {

                 if (listcity20.ContainsKey(pa.Title)||listcity10.ContainsKey(pa.Title)||listcity6.ContainsKey(pa.Title))
                 {
                     citdq += getdicrownum(pa.Title,listcity20, listcity10, listcity6, 3 + cithj);
                 }
             }
             CreateSheetView(fpSpread1.Sheets[8], citdq + cithj, 1, 3, 0, "市辖供电区");
            //合计
             int couhj = 0;
             couhj = getztrownum(listzcountry20, listzcountry10, listzcountry6, 3 + citdq + cithj);
            //地区
             int coundq = 0;
             foreach (PS_Table_AreaWH pa in list)
             {

                 if (listcountry20.ContainsKey(pa.Title) || listcountry10.ContainsKey(pa.Title) || listcountry6.ContainsKey(pa.Title))
                 {
                     coundq += getdicrownum(pa.Title,listcountry20, listcountry10, listcountry6, 3 + cithj+citdq+couhj);
                 }
             }
             CreateSheetView(fpSpread1.Sheets[8], couhj + coundq, 1, 3 + citdq + cithj,0, "县供电区");
            fpSpread1.Sheets[8].Rows.Remove(3+cithj+citdq+couhj+coundq,1);
            Sheet_GridandColor(fpSpread1.Sheets[8], 3 + cithj + citdq + couhj + coundq, 16);
        }
        //合计的内容
        private int getztrownum(IList list1,IList list2,IList list3,int rownum)
        {
            int citznum = 0;
            if (list1.Count>0)
            {
                fpSpread1.Sheets[8].Rows.Add(rownum, 1);
                setsheet8value(fpSpread1.Sheets[8], list1, rownum);
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, rownum, 2, "20");
                citznum++;
            }
            if (list2.Count>0)
            {
                fpSpread1.Sheets[8].Rows.Add(rownum+citznum, 1);
                setsheet8value(fpSpread1.Sheets[8], list2, rownum+citznum);
                CreateSheetView(fpSpread1.Sheets[8], 1, 1,rownum+citznum, 2, "10");
                citznum++;
            }
            if (list3.Count>0)
            {
                fpSpread1.Sheets[8].Rows.Add(rownum+ citznum, 1);
                setsheet8value(fpSpread1.Sheets[8], list3, rownum+ citznum);
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, rownum + citznum, 2, "6");
                citznum++;
            }
          
            CreateSheetView(fpSpread1.Sheets[8], citznum, 1, rownum, 1, "合计");
            return citznum;
        }
        private int getdicrownum(string dq,Dictionary<string,IList> list1,Dictionary<string,IList> list2,Dictionary<string,IList> list3,int rownum)
        {
            int dqnum = 0;
            if (list1.ContainsKey(dq))
            {
                fpSpread1.Sheets[8].Rows.Add(rownum, 1);
                setsheet8value(fpSpread1.Sheets[8], list1[dq], rownum);
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, rownum, 2, "20");
                dqnum++;
            }
            if (list2.ContainsKey(dq))
            {
                fpSpread1.Sheets[8].Rows.Add(rownum + dqnum, 1);
                setsheet8value(fpSpread1.Sheets[8], list2[dq], rownum + dqnum);
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, rownum + dqnum, 2, "10");
                dqnum++;
            }
            if (list3.ContainsKey(dq))
            {
                fpSpread1.Sheets[8].Rows.Add(rownum + dqnum, 1);
                setsheet8value(fpSpread1.Sheets[8], list3[dq], rownum + dqnum);
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, rownum + dqnum, 2, "6");
                dqnum++;
            }
            CreateSheetView(fpSpread1.Sheets[8], dqnum, 1, rownum,1,dq);
            return dqnum;

        }
        private void setsheet8value(FarPoint.Win.Spread.SheetView obj, IList list, int rownum)
        {
            double fh = 0.0; double rl = 0.0; int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0, sum6 = 0;
            foreach (PSPDEV ps in list)
            {
                fh = Convert.ToDouble(ps.Burthen);
                string con = "a.ProjectID='" + ProjectUID + "'and a.SUID='" + ps.SUID + "'";
                rl = Convert.ToDouble(Services.BaseService.GetObject("SelectPSPDEV_SUMNum2_type50-59", con));

                if (rl != 0)
                {
                    double fzl = fh / rl;
                    con = "a.ProjectID='" + ProjectUID + "'and a.SUID='" + ps.SUID + "'and b.Type in('50','51','52')";
                    int ts = Convert.ToInt32(Services.BaseService.GetObject("SelectPSPDEV_SUMFlag", con));
                    if (fzl>=0&&fzl < 0.2)
                    {

                        sum1 += ts;
                    }
                    if (fzl >= 0.2 && fzl < 0.4)
                    {
                        sum2 += ts;
                    }
                    if (fzl >= 0.4 && fzl < 0.6)
                    {
                        sum3 += ts;
                    }
                    if (fzl >= 0.6 && fzl < 0.8)
                    {
                        sum4 += ts;
                    }
                    if (fzl >= 0.8 && fzl < 1)
                    {
                        sum5 += ts;
                    }
                    if (fzl >= 1)
                    {
                        sum6 += ts;
                    }
                }

            }
            CreateSheetView(obj, 1, 1, rownum, 4, sum1);
            CreateSheetView(obj, 1, 1, rownum, 6, sum2);
            CreateSheetView(obj, 1, 1, rownum, 8, sum3);
            CreateSheetView(obj, 1, 1, rownum, 10, sum4);
            CreateSheetView(obj, 1, 1, rownum, 12, sum5);
            CreateSheetView(obj, 1, 1, rownum, 14, sum6);
            obj.Cells[rownum, 3].Formula = "R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15";
            obj.Cells[rownum, 5].Formula = "R" + (rownum + 1).ToString() + "C5/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 7].Formula = "R" + (rownum + 1).ToString() + "C7/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 9].Formula = "R" + (rownum + 1).ToString() + "C9/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 11].Formula = "R" + (rownum + 1).ToString() + "C11/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 13].Formula = "R" + (rownum + 1).ToString() + "C13/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 15].Formula = "R" + (rownum + 1).ToString() + "C15/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";

        }
        private void zypdfbtable()
        {
           fpSpread1.Sheets[8].RowCount = 0;
            fpSpread1.Sheets[8].ColumnCount = 0;
            fpSpread1.Sheets[8].RowCount =4;
            fpSpread1.Sheets[8].ColumnCount = 16;
            fpSpread1.Sheets[8].SetValue(0, 0, "2010年铜陵市中压配变负载率分布表");
            fpSpread1.Sheets[8].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[8].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[8].Cells[0, 0].ColumnSpan = 16;
            for (int i = 0; i < 12; i+=2)
            {
                fpSpread1.Sheets[8].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[8].Rows[1].CellType = texttype;
            fpSpread1.Sheets[8].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[8], 2, 1, 1, 0, "分区类型");
            CreateSheetView(fpSpread1.Sheets[8], 2, 1, 1, 1, "分区名称");
            CreateSheetView(fpSpread1.Sheets[8], 2, 1, 1, 2, "电压等级（kV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[8], 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[8], 2, 1, 1, 3, "变电站总数");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[8], 3, "变电站总数");

            CreateSheetView(fpSpread1.Sheets[8], 1, 2, 1, 4, "0～20%");
            CreateSheetView(fpSpread1.Sheets[8], 1, 2, 1, 6, "21%～40%");
            CreateSheetView(fpSpread1.Sheets[8], 1, 2, 1, 8, "41%～60%");
            CreateSheetView(fpSpread1.Sheets[8], 1, 2, 1, 10, "61%～80%");
            CreateSheetView(fpSpread1.Sheets[8], 1, 2, 1, 12, "81%～100%");
            CreateSheetView(fpSpread1.Sheets[8], 1, 2, 1, 14, "100%以上");
            for (int i = 0; i < 12; i+= 2)
            {
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, 2, 4 + i, "台数");
                CreateSheetView(fpSpread1.Sheets[8], 1, 1, 2, 5 + i, "比例");
            }
            //CreateSheetView(fpSpread1.Sheets[8], 3, 1, 3, 0, "市辖供电区");
            //CreateSheetView(fpSpread1.Sheets[8], 2, 1, 3, 1, "合计");
            //CreateSheetView(fpSpread1.Sheets[8], 1, 1, 3, 2, "10");
          

            //CreateSheetView(fpSpread1.Sheets[8], 3, 1, 5, 0, "县级供电区");
            //CreateSheetView(fpSpread1.Sheets[8], 2, 1, 5, 1, "合计");
            //CreateSheetView(fpSpread1.Sheets[8], 2, 1, 5, 2, "10");
          
        }
        //中压线路最大负载率分布表
        private void zyxlzdfh()
        {
            //table
            zyxlzdfhtable();
            //数据
            int rownum = 0;
            //市辖供电区
            rownum = getrow9num("('市辖供电区')", 3);
            if (rownum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], rownum, 1, 3, 0, "1");
                CreateSheetView(fpSpread1.Sheets[9], rownum, 1, 3, 1, "市辖供电区");
            }
            else
            {
                rownum = 1;
                CreateSheetView(fpSpread1.Sheets[9], rownum, 1, 3, 0, "1");
                CreateSheetView(fpSpread1.Sheets[9], rownum, 1, 3, 1, "市辖供电区");
            }

            //县级供电区
            int xiannum = 0;
            xiannum = getrow9num("('县级直供直管','县级控股','县级参股','县级代管')", 3 + rownum);
            if (xiannum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], xiannum, 1, 3 + rownum, 0, "2");
                CreateSheetView(fpSpread1.Sheets[9], xiannum, 1, 3 + rownum, 1, "县级供电区");
            }
            else
            {
                xiannum = 1;
                CreateSheetView(fpSpread1.Sheets[9], xiannum, 1, 3 + rownum, 0, "2");
                CreateSheetView(fpSpread1.Sheets[9], xiannum, 1, 3 + rownum, 1, "县级供电区");
            }
            //直供直管
            int zznum = 0;
            zznum = getrow9num("('县级直供直管')", 3 + rownum + xiannum);
            if (zznum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], zznum, 1, 3 + rownum + xiannum, 0, "2.1");
                CreateSheetView(fpSpread1.Sheets[9], zznum, 1, 3 + rownum + xiannum, 1, "直供直管");
            }
            else
            {
                zznum = 1;
                CreateSheetView(fpSpread1.Sheets[9], zznum, 1, 3 + rownum + xiannum, 0, "2.1");
                CreateSheetView(fpSpread1.Sheets[9], zznum, 1, 3 + rownum + xiannum, 1, "直供直管");
            }
            //控股

            int kgnum = 0;
            kgnum = getrow9num("('县级控股')", 3 + rownum + xiannum + zznum);
            if (kgnum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], kgnum, 1, 3 + rownum + xiannum + zznum, 0, "2.2");
                CreateSheetView(fpSpread1.Sheets[9], kgnum, 1, 3 + rownum + xiannum + zznum, 1, "县级控股");
            }
            else
            {
                kgnum = 1;
                CreateSheetView(fpSpread1.Sheets[9], kgnum, 1, 3 + rownum + xiannum + zznum, 0, "2.2");
                CreateSheetView(fpSpread1.Sheets[9], kgnum, 1, 3 + rownum + xiannum + zznum, 1, "县级控股");
            }
            //参股
            int cgnum = 0;
            cgnum = getrow9num("('县级参股')", 3 + rownum + xiannum + zznum + kgnum);
            if (cgnum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], cgnum, 1, 3 + rownum + xiannum + zznum + kgnum, 0, "2.3");
                CreateSheetView(fpSpread1.Sheets[9], cgnum, 1, 3 + rownum + xiannum + zznum + kgnum, 1, "县级参股");
            }
            else
            {
                cgnum = 1;
                CreateSheetView(fpSpread1.Sheets[9], cgnum, 1, 3 + rownum + xiannum + zznum + kgnum, 0, "2.3");
                CreateSheetView(fpSpread1.Sheets[9], cgnum, 1, 3 + rownum + xiannum + zznum + kgnum, 1, "县级参股");
            }
            //代管
            int dgnum = 0;
            dgnum = getrow9num("('县级代管')", 3 + rownum + xiannum + zznum + kgnum + cgnum);
            if (dgnum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], dgnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum, 0, "2.4");
                CreateSheetView(fpSpread1.Sheets[9], dgnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum, 1, "县级代管");
            }
            else
            {
                dgnum = 1;
                CreateSheetView(fpSpread1.Sheets[9], dgnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum, 0, "2.4");
                CreateSheetView(fpSpread1.Sheets[9], dgnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum, 1, "县级代管");
            }
            //全地区
            int qqnum = 0;
            qqnum = getrow9num("('市辖供电区','县级直供直管','县级控股','县级参股','县级代管')", 3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum);
            if (dgnum > 0)
            {
                CreateSheetView(fpSpread1.Sheets[9], qqnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum, 0, "3");
                CreateSheetView(fpSpread1.Sheets[9], qqnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum, 1, "全地区总计");
            }
            else
            {
                qqnum = 1;
                CreateSheetView(fpSpread1.Sheets[9], qqnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum, 0, "2.4");
                CreateSheetView(fpSpread1.Sheets[9], qqnum, 1, 3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum, 1, "全地区总计");
            }
            //控制样式
            fpSpread1.Sheets[9].Rows.Remove(3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum + qqnum, 1);
            Sheet_GridandColor(fpSpread1.Sheets[9], 3 + rownum + xiannum + zznum + kgnum + cgnum + dgnum + qqnum, 16);
        }
        private int getrow9num(string title, int rownum)
        {
            string con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='20'and LineType2='公用'and type='05'AND DQ in" + title ;
            IList list20 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            int num = 0;
            fpSpread1.Sheets[9].Rows.Add(rownum, 1);
            if (list20.Count > 0)
            {

                CreateSheetView(fpSpread1.Sheets[9], 1, 1, rownum, 2, "20");
                setsheet9value(fpSpread1.Sheets[9], list20, rownum);
                num += 1;
            }
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='10'and LineType2='公用'and type='05'AND DQ in" + title ;
            IList list10 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            fpSpread1.Sheets[9].Rows.Add(rownum + num, 1);
            if (list10.Count > 0)
            {

                CreateSheetView(fpSpread1.Sheets[9], 1, 1, rownum + num, 2, "10");
                setsheet9value(fpSpread1.Sheets[9], list20, rownum + num);
                num++;
            }
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='6'and LineType2='公用'and type='05'AND DQ in" + title;
            IList list6 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            fpSpread1.Sheets[9].Rows.Add(rownum + num, 1);
            if (list6.Count > 0)
            {

                CreateSheetView(fpSpread1.Sheets[9], 1, 1, rownum + num, 2, "6");
                setsheet9value(fpSpread1.Sheets[9], list6, rownum + num);
                num++;
            }
            return num;

        }
        private void setsheet9value(FarPoint.Win.Spread.SheetView obj, IList list, int rownum)
        {
            double fh = 0.0; double rl = 0.0; int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0, sum6 = 0;
            foreach (PSPDEV ps in list)
            {
                rl = Convert.ToDouble(ps.Burthen);

                fh = ps.FactI;
                if (rl != 0)
                {
                    double fzl = fh / rl;
                    
                    if (fzl>=0&&fzl < 0.2)
                    {

                        sum1 ++;
                    }
                    if (fzl >= 0.2 && fzl < 0.4)
                    {
                        sum2 ++;
                    }
                    if (fzl >= 0.4 && fzl < 0.6)
                    {
                        sum3 ++;
                    }
                    if (fzl >= 0.6 && fzl < 0.8)
                    {
                        sum4 ++;
                    }
                    if (fzl >= 0.8 && fzl < 1)
                    {
                        sum5 ++;
                    }
                    if (fzl >= 1)
                    {
                        sum6 ++;
                    }
                }

            }
            CreateSheetView(obj, 1, 1, rownum, 4, sum1);
            CreateSheetView(obj, 1, 1, rownum, 6, sum2);
            CreateSheetView(obj, 1, 1, rownum, 8, sum3);
            CreateSheetView(obj, 1, 1, rownum, 10, sum4);
            CreateSheetView(obj, 1, 1, rownum, 12, sum5);
            CreateSheetView(obj, 1, 1, rownum, 14, sum6);
            obj.Cells[rownum, 3].Formula = "R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15";
            obj.Cells[rownum, 5].Formula = "R" + (rownum + 1).ToString() + "C5/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 7].Formula = "R" + (rownum + 1).ToString() + "C7/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 9].Formula = "R" + (rownum + 1).ToString() + "C9/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 11].Formula = "R" + (rownum + 1).ToString() + "C11/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 13].Formula = "R" + (rownum + 1).ToString() + "C13/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 15].Formula = "R" + (rownum + 1).ToString() + "C15/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";

        }
        private void zyxlzdfhtable()
        {
              fpSpread1.Sheets[9].RowCount = 0;
            fpSpread1.Sheets[9].ColumnCount = 0;
            fpSpread1.Sheets[9].RowCount = 4;
            fpSpread1.Sheets[9].ColumnCount = 16;
            fpSpread1.Sheets[9].SetValue(0, 0, "2010年铜陵市中压线路最大负载率分布表");
            fpSpread1.Sheets[9].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[9].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[9].Cells[0, 0].ColumnSpan = 16;
            for (int i = 0; i < 12; i += 2)
            {
                fpSpread1.Sheets[9].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[9].Rows[1].CellType = texttype;
            fpSpread1.Sheets[9].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[9], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[9], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[9], 2, 1, 1, 2, "电压等级（kV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[9], 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[9], 2, 1, 1, 3, "线路总数");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[9],3, "线路总数");

            CreateSheetView(fpSpread1.Sheets[9], 1, 2, 1, 4, "0～20%");
            CreateSheetView(fpSpread1.Sheets[9], 1, 2, 1, 6, "21%～40%");
            CreateSheetView(fpSpread1.Sheets[9], 1, 2, 1, 8, "41%～60%");
            CreateSheetView(fpSpread1.Sheets[9], 1, 2, 1, 10, "61%～80%");
            CreateSheetView(fpSpread1.Sheets[9], 1, 2, 1, 12, "81%～100%");
            CreateSheetView(fpSpread1.Sheets[9], 1, 2, 1, 14, "100%以上");
            for (int i = 0; i < 12; i += 2)
            {
                CreateSheetView(fpSpread1.Sheets[9], 1, 1, 2, 4 + i, "台数");
                CreateSheetView(fpSpread1.Sheets[9], 1, 1, 2, 5 + i, "比例");
            }
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 3, 0, "1");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 3, 1, "市辖供电区");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 4, 0, "2");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 4, 1, "县级供电区");

            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 5, 0, "2.1");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 5, 1, "其中：直供直管");

            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 6, 0, "2.2");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 6, 1, "控股");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 7, 0, "2.3");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 7, 1, "参股");

            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 8, 0, "2.4");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 8, 1, "代管");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 9, 0, "3");
            //CreateSheetView(fpSpread1.Sheets[9], 1, 1, 9, 1, "全地区");
            //for (int i = 0; i < 7; i ++)
            //{
            //    CreateSheetView(fpSpread1.Sheets[9], 1, 1, 3 + i, 2, "10");
                
            //}
            //Sheet_GridandColor(fpSpread1.Sheets[9], 10, 16);
        }
        //中压配电网线路最大负载率分布附表
        private void zyxlzdfzfb()
        {
            //table
            zyxlzdfzfbtable();
            //数据
            //获取数据
            Dictionary<string, IList> listcity20 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcity10 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcity6 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry20 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry10 = new Dictionary<string, IList>();
            Dictionary<string, IList> listcountry6 = new Dictionary<string, IList>();
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            foreach (PS_Table_AreaWH pa in list)
            {
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='20' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                IList list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity20.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='10' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity10.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt='6' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcity6.Add(pa.Title, list1);
                }

                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='20' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry20.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='10' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry10.Add(pa.Title, list1);
                }
                con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='6' AND AreaID='" + pa.ID + "'and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
                list1 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
                if (list1.Count != 0)
                {
                    listcountry6.Add(pa.Title, list1);
                }
            }
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='20' and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity20 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='10' and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity10 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='6' and LineType2='公用'and type='05'AND DQ='市辖供电区'";
            IList listzcity6 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);

            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='20' and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
            IList listzcountry20 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='10' and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
            IList listzcountry10 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            con = "WHERE ProjectID='" + ProjectUID + "'and RateVolt ='6' and LineType2='公用'and type='05'AND DQ!='市辖供电区'";
            IList listzcountry6 = Services.BaseService.GetList("SelectPSPDEVByCondition", con);
            //合计
            int cithj = 0;
            cithj = getzt10rownum(listzcity20, listzcity10, listzcity6, 3);
            //地区
            int citdq = 0;
            foreach (PS_Table_AreaWH pa in list)
            {

                if (listcity20.ContainsKey(pa.Title) || listcity10.ContainsKey(pa.Title) || listcity6.ContainsKey(pa.Title))
                {
                    citdq += getdic10rownum(pa.Title, listcity20, listcity10, listcity6, 3 + cithj);
                }
            }
            CreateSheetView(fpSpread1.Sheets[10], citdq + cithj, 1, 3, 0, "市辖供电区");
            //合计
            int couhj = 0;
            couhj = getzt10rownum(listzcountry20, listzcountry10, listzcountry6, 3 + citdq + cithj);
            //地区
            int coundq = 0;
            foreach (PS_Table_AreaWH pa in list)
            {

                if (listcountry20.ContainsKey(pa.Title) || listcountry10.ContainsKey(pa.Title) || listcountry6.ContainsKey(pa.Title))
                {
                    coundq += getdic10rownum(pa.Title, listcountry20, listcountry10, listcountry6, 3 + cithj + citdq + couhj);
                }
            }
            CreateSheetView(fpSpread1.Sheets[10], couhj + coundq, 1, 3 + citdq + cithj, 0, "县供电区");
            fpSpread1.Sheets[10].Rows.Remove(3 + cithj + citdq + couhj + coundq, 1);
            Sheet_GridandColor(fpSpread1.Sheets[10], 3 + cithj + citdq + couhj + coundq, 16);
        }
        private int getzt10rownum(IList list1, IList list2, IList list3, int rownum)
        {
            int citznum = 0;
            if (list1.Count > 0)
            {
                fpSpread1.Sheets[10].Rows.Add(rownum, 1);
                setsheet10value(fpSpread1.Sheets[10], list1, rownum);
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, rownum, 2, "20");
                citznum++;
            }
            if (list2.Count > 0)
            {
                fpSpread1.Sheets[10].Rows.Add(rownum + citznum, 1);
                setsheet10value(fpSpread1.Sheets[10], list2, rownum + citznum);
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, rownum + citznum, 2, "10");
                citznum++;
            }
            if (list3.Count > 0)
            {
                fpSpread1.Sheets[10].Rows.Add(rownum + citznum, 1);
                setsheet10value(fpSpread1.Sheets[10], list3, rownum + citznum);
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, rownum + citznum, 2, "6");
                citznum++;
            }
            CreateSheetView(fpSpread1.Sheets[10], citznum, 1, rownum, 1, "合计");
            return citznum;
        }
        private int getdic10rownum(string dq, Dictionary<string, IList> list1, Dictionary<string, IList> list2, Dictionary<string, IList> list3, int rownum)
        {
            int dqnum = 0;
            if (list1.ContainsKey(dq))
            {
                fpSpread1.Sheets[10].Rows.Add(rownum, 1);
                setsheet10value(fpSpread1.Sheets[10], list1[dq], rownum);
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, rownum, 2, "20");
                dqnum++;
            }
            if (list2.ContainsKey(dq))
            {
                fpSpread1.Sheets[10].Rows.Add(rownum + dqnum, 1);
                setsheet10value(fpSpread1.Sheets[10], list2[dq], rownum + dqnum);
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, rownum + dqnum, 2, "10");
                dqnum++;
            }
            if (list3.ContainsKey(dq))
            {
                fpSpread1.Sheets[10].Rows.Add(rownum + dqnum, 1);
                setsheet10value(fpSpread1.Sheets[10], list3[dq], rownum + dqnum);
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, rownum + dqnum, 2, "6");
                dqnum++;
            }
            CreateSheetView(fpSpread1.Sheets[10], dqnum, 1, rownum, 1, dq);
            return dqnum;

        }
        private void setsheet10value(FarPoint.Win.Spread.SheetView obj, IList list, int rownum)
        {
            double fh = 0.0; double rl = 0.0; int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0, sum5 = 0, sum6 = 0;
            foreach (PSPDEV ps in list)
            {
                rl = Convert.ToDouble(ps.Burthen);

                fh = ps.FactI;
                if (rl != 0)
                {
                    double fzl = fh / rl;

                    if (fzl>=0&&fzl < 0.2)
                    {

                        sum1++;
                    }
                    if (fzl >= 0.2 && fzl < 0.4)
                    {
                        sum2++;
                    }
                    if (fzl >= 0.4 && fzl < 0.6)
                    {
                        sum3++;
                    }
                    if (fzl >= 0.6 && fzl < 0.8)
                    {
                        sum4++;
                    }
                    if (fzl >= 0.8 && fzl < 1)
                    {
                        sum5++;
                    }
                    if (fzl >= 1)
                    {
                        sum6++;
                    }
                }

            }
            CreateSheetView(obj, 1, 1, rownum, 4, sum1);
            CreateSheetView(obj, 1, 1, rownum, 6, sum2);
            CreateSheetView(obj, 1, 1, rownum, 8, sum3);
            CreateSheetView(obj, 1, 1, rownum, 10, sum4);
            CreateSheetView(obj, 1, 1, rownum, 12, sum5);
            CreateSheetView(obj, 1, 1, rownum, 14, sum6);
            obj.Cells[rownum, 3].Formula = "R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15";
            obj.Cells[rownum, 5].Formula = "R" + (rownum + 1).ToString() + "C5/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 7].Formula = "R" + (rownum + 1).ToString() + "C7/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 9].Formula = "R" + (rownum + 1).ToString() + "C9/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 11].Formula = "R" + (rownum + 1).ToString() + "C11/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 13].Formula = "R" + (rownum + 1).ToString() + "C13/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";
            obj.Cells[rownum, 15].Formula = "R" + (rownum + 1).ToString() + "C15/(R" + (rownum + 1).ToString() + "C5+R" + (rownum + 1).ToString() + "C7+R" + (rownum + 1).ToString() + "C9+R" + (rownum + 1).ToString() + "C11+R" + (rownum + 1).ToString() + "C13+R" + (rownum + 1).ToString() + "C15)";

        }
        private void zyxlzdfzfbtable()
        {
             fpSpread1.Sheets[10].RowCount = 0;
            fpSpread1.Sheets[10].ColumnCount = 0;
            fpSpread1.Sheets[10].RowCount =4;
            fpSpread1.Sheets[10].ColumnCount = 16;
            fpSpread1.Sheets[10].SetValue(0, 0, "2010年铜陵市中压配电线路率分布表");
            fpSpread1.Sheets[10].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[10].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[10].Cells[0, 0].ColumnSpan = 16;
            for (int i = 0; i < 12; i+=2)
            {
                fpSpread1.Sheets[10].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[10].Rows[1].CellType = texttype;
            fpSpread1.Sheets[10].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[10], 2, 1, 1, 0, "分区类型");
            CreateSheetView(fpSpread1.Sheets[10], 2, 1, 1, 1, "分区名称");
            CreateSheetView(fpSpread1.Sheets[10], 2, 1, 1, 2, "电压等级（kV）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[10], 2, "电压等级（kV）");
            CreateSheetView(fpSpread1.Sheets[10], 2, 1, 1, 3, "线路总条数（条）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[10], 3, "线路总条数（条）");

            CreateSheetView(fpSpread1.Sheets[10], 1, 2, 1, 4, "0～20%");
            CreateSheetView(fpSpread1.Sheets[10], 1, 2, 1, 6, "21%～40%");
            CreateSheetView(fpSpread1.Sheets[10], 1, 2, 1, 8, "41%～60%");
            CreateSheetView(fpSpread1.Sheets[10], 1, 2, 1, 10, "61%～80%");
            CreateSheetView(fpSpread1.Sheets[10], 1, 2, 1, 12, "81%～100%");
            CreateSheetView(fpSpread1.Sheets[10], 1, 2, 1, 14, "100%以上");
            for (int i = 0; i < 12; i+= 2)
            {
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, 2, 4 + i, "条数");
                CreateSheetView(fpSpread1.Sheets[10], 1, 1, 2, 5 + i, "比例");
            }
            //CreateSheetView(fpSpread1.Sheets[10], 3, 1, 3, 0, "市辖供电区");
            //CreateSheetView(fpSpread1.Sheets[10], 2, 1, 3, 1, "合计");
            //CreateSheetView(fpSpread1.Sheets[10], 1, 1, 3, 2, "10");
          

            //CreateSheetView(fpSpread1.Sheets[10], 3, 1, 5, 0, "县级供电区");
            //CreateSheetView(fpSpread1.Sheets[10], 2, 1, 5, 1, "合计");
            //CreateSheetView(fpSpread1.Sheets[10], 2, 1, 5, 2, "10");
        }
        //供电可靠率（RS-3）分布表
        private void gdkkl()
        {
            //table
            gdkkltable();
            //数据
            string con = "ProjectID='" + ProjectUID + "'and SType='市辖供电区'";
            int enternum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[11], 1, 1,3,2,enternum);
            con = "ProjectID='" + ProjectUID + "'and SType='县级直供直管'";
            enternum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[11], 1, 1,5,2,enternum);
            con = "ProjectID='" + ProjectUID + "'and SType='县级控股'";
            enternum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[11], 1, 1,6,2,enternum);
            con = "ProjectID='" + ProjectUID + "'and SType='县级参股'";
            enternum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[11], 1, 1,7,2,enternum);
            con = "ProjectID='" + ProjectUID + "'and SType='县级代管'";
            enternum = Convert.ToInt32(Services.BaseService.GetObject("SelectPs_Table_Enterprise_CountAll", con));
            CreateSheetView(fpSpread1.Sheets[11], 1, 1,8,2,enternum);
            fpSpread1.Sheets[11].Cells[4,2].Formula="R6C3+R7C3+R8C3+R9C3";
            FPSpread.Sheets[11].Cells[9,2].Formula="R4C3+R5C3";
        }
        private struct RS
        {
            public string title;
            public int sum1;
            public int sum2;
            public int sum3;
            public int sum4;
            public int sum5;
            public RS(string _title,int _sum1,int _sum2,int _sum3,int _sum4,int _sum5)
            {
                title = _title;
                sum1 = _sum1;
                sum2 = _sum2;
                sum3 = _sum3;
                sum4 = _sum4;
                sum5 = _sum5;
            }
        }
        private List<RS> getRStable(FarPoint.Win.Spread.SheetView obj)
        {
            List<RS> list = new List<RS>();
            int rowcount = obj.RowCount;
            int colcount = obj.ColumnCount;
            for (int i = 0; i < rowcount;i++ )
            {
                if (i>2&&i!=4)
                {
                    string title = obj.GetValue(i, 1).ToString();
                    int sum1 = (obj.GetValue(i, 3) == null ? 0 : Convert.ToInt32(obj.GetValue(i, 3)));
                    int sum2 = (obj.GetValue(i, 5) == null ? 0 : Convert.ToInt32(obj.GetValue(i, 5)));
                    int sum3 = (obj.GetValue(i, 7) == null ? 0 : Convert.ToInt32(obj.GetValue(i, 7)));
                    int sum4 = (obj.GetValue(i, 9) == null ? 0 : Convert.ToInt32(obj.GetValue(i, 9)));

                    int sum5 = (obj.GetValue(i, 11) == null ? 0 : Convert.ToInt32(obj.GetValue(i, 11)));
                    RS rs = new RS(title, sum1, sum2, sum3, sum4, sum5);
                    list.Add(rs);
                }
            }
            return list;
        }
        private void setRStable(FarPoint.Win.Spread.SheetView obj,List<RS> list)
        {
            int rowcount = obj.RowCount;
            for (int i = 3; i < rowcount;i++ )
            {
                foreach (RS rs in list)
                {
                  if (obj.GetValue(i,1).ToString()==rs.title)
                {
                    obj.SetValue(i, 3, rs.sum1);
                    obj.SetValue(i, 5, rs.sum2);
                    obj.SetValue(i, 7, rs.sum3);
                    obj.SetValue(i, 9, rs.sum4);
                    obj.SetValue(i, 11, rs.sum5);
                }
                }
                
            }
            obj.Cells[4, 2].Formula = "R6C3+R7C3+R8C3+R9C3";
            obj.Cells[4, 3].Formula = "R6C4+R7C4+R8C4+R9C4";
            obj.Cells[4, 5].Formula = "R6C6+R7C6+R8C6+R9C6";
            obj.Cells[4, 7].Formula = "R6C8+R7C8+R8C8+R9C8";
            obj.Cells[4, 9].Formula = "R6C10+R7C10+R8C10+R9C10";
            obj.Cells[4, 11].Formula = "R6C12+R7C12+R8C12+R9C12";

            obj.Cells[3, 4].Formula = "R4C4/(R4C4+R4C6+R4C8+R4C10+R4C12)";
            obj.Cells[3, 6].Formula = "R4C6/(R4C4+R4C6+R4C8+R4C10+R4C12)";
            obj.Cells[3, 8].Formula = "R4C8/(R4C4+R4C6+R4C8+R4C10+R4C12)";
            obj.Cells[3, 10].Formula = "R4C10/(R4C4+R4C6+R4C8+R4C10+R4C12)";
            obj.Cells[3, 12].Formula = "R4C12/(R4C4+R4C6+R4C8+R4C10+R4C12)";

            obj.Cells[4, 4].Formula = "R5C4/(R5C4+R5C6+R5C8+R5C10+R5C12)";
            obj.Cells[4, 6].Formula = "R5C6/(R5C4+R5C6+R5C8+R5C10+R5C12)";
            obj.Cells[4, 8].Formula = "R5C8/(R5C4+R5C6+R5C8+R5C10+R5C12)";
            obj.Cells[4, 10].Formula = "R5C10/(R5C4+R5C6+R5C8+R5C10+R5C12)";
            obj.Cells[4, 12].Formula = "R5C12/(R5C4+R5C6+R5C8+R5C10+R5C12)";

            obj.Cells[5, 4].Formula = "R6C4/(R6C4+R6C6+R6C8+R6C10+R6C12)";
            obj.Cells[5, 6].Formula = "R6C6/(R6C4+R6C6+R6C8+R6C10+R6C12)";
            obj.Cells[5, 8].Formula = "R6C8/(R6C4+R6C6+R6C8+R6C10+R6C12)";
            obj.Cells[5, 10].Formula = "R6C10/(R6C4+R6C6+R6C8+R6C10+R6C12)";
            obj.Cells[5, 12].Formula = "R6C12/(R6C4+R6C6+R6C8+R6C10+R6C12)";

            obj.Cells[6, 4].Formula = "R7C4/(R7C4+R7C6+R7C8+R7C10+R7C12)";
            obj.Cells[6, 6].Formula = "R7C6/(R7C4+R7C6+R7C8+R7C10+R7C12)";
            obj.Cells[6, 8].Formula = "R7C8/(R7C4+R7C6+R7C8+R7C10+R7C12)";
            obj.Cells[6, 10].Formula = "R7C10/(R7C4+R7C6+R7C8+R7C10+R7C12)";
            obj.Cells[6, 12].Formula = "R7C12/(R7C4+R7C6+R7C8+R7C10+R7C12)";

            obj.Cells[7, 4].Formula = "R8C4/(R8C4+R8C6+R8C8+R8C10+R8C12)";
            obj.Cells[7, 6].Formula = "R8C6/(R8C4+R8C6+R8C8+R8C10+R8C12)";
            obj.Cells[7, 8].Formula = "R8C8/(R8C4+R8C6+R8C8+R8C10+R8C12)";
            obj.Cells[7, 10].Formula = "R8C10/(R8C4+R8C6+R8C8+R8C10+R8C12)";
            obj.Cells[7, 12].Formula = "R8C12/(R8C4+R8C6+R8C8+R8C10+R8C12)";

            obj.Cells[8, 4].Formula = "R9C4/(R9C4+R9C6+R9C8+R9C10+R9C12)";
            obj.Cells[8, 6].Formula = "R9C6/(R9C4+R9C6+R9C8+R9C10+R9C12)";
            obj.Cells[8, 8].Formula = "R9C8/(R9C4+R9C6+R9C8+R9C10+R9C12)";
            obj.Cells[8, 10].Formula = "R9C10/(R9C4+R9C6+R9C8+R9C10+R9C12)";
            obj.Cells[8, 12].Formula = "R9C12/(R9C4+R9C6+R9C8+R9C10+R9C12)";

            obj.Cells[9, 3].Formula = "R4C4+R5C4";
            obj.Cells[9, 5].Formula = "R4C6+R5C6";
            obj.Cells[9, 7].Formula = "R4C8+R5C8";
            obj.Cells[9, 9].Formula = "R4C10+R5C10";
            obj.Cells[9, 11].Formula = "R4C12+R5C12";
            obj.Cells[9, 4].Formula = "R10C4/(R10C4+R10C6+R10C8+R10C10+R10C12)";
            obj.Cells[9, 6].Formula = "R10C6/(R10C4+R10C6+R10C8+R10C10+R10C12)";
            obj.Cells[9, 8].Formula = "R10C8/(R10C4+R10C6+R10C8+R10C10+R10C12)";
            obj.Cells[9, 10].Formula = "R10C10/(R10C4+R10C6+R10C8+R10C10+R10C12)";
            obj.Cells[9, 12].Formula = "R10C12/(R10C4+R10C6+R10C8+R10C10+R10C12)";

            obj.Cells[9, 2].Formula = "R4C3+R5C3";
            

        }
        private void gdkkltable()
        {
             fpSpread1.Sheets[11].RowCount = 0;
            fpSpread1.Sheets[11].ColumnCount = 0;
            fpSpread1.Sheets[11].RowCount = 10;
            fpSpread1.Sheets[11].ColumnCount = 13;
            fpSpread1.Sheets[11].SetValue(0, 0, "2010年铜陵市供电可靠率（RS-3）分布表");
            fpSpread1.Sheets[11].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[11].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[11].Cells[0, 0].ColumnSpan = 13;
            for (int i = 0; i < 10; i += 2)
            {
                fpSpread1.Sheets[11].Columns[4 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[11].Rows[1].CellType = texttype;
            fpSpread1.Sheets[11].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[11], 2, 1, 1, 0, "编号");
            CreateSheetView(fpSpread1.Sheets[11], 2, 1, 1, 1, "类型");
            CreateSheetView(fpSpread1.Sheets[11], 2, 1, 1, 2, "供电企业个数（个）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[10], 2, "供电企业个数（个）");
     
            CreateSheetView(fpSpread1.Sheets[11], 1, 2, 1, 3, "99.00%以下");
            CreateSheetView(fpSpread1.Sheets[11], 1, 2, 1, 5, "99.00-99.5%");
            CreateSheetView(fpSpread1.Sheets[11], 1, 2, 1, 7, "99.00-99.5%");
            CreateSheetView(fpSpread1.Sheets[11], 1, 2, 1, 9, "99.9-99.95%");
            CreateSheetView(fpSpread1.Sheets[11], 1, 2, 1, 11, "99.95%以上");
           
            for (int i = 0; i < 10; i += 2)
            {
                CreateSheetView(fpSpread1.Sheets[11], 1, 1, 2, 3+ i, "个数");
                CreateSheetView(fpSpread1.Sheets[11], 1, 1, 2, 4 + i, "比例");
            }
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 3, 0, "1");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 3, 1, "市辖供电区");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 4, 0, "2");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 4, 1, "县级供电区");

            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 5, 0, "2.1");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 5, 1, "其中：直供直管");

            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 6, 0, "2.2");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 6, 1, "控股");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 7, 0, "2.3");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 7, 1, "参股");

            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 8, 0, "2.4");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 8, 1, "代管");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 9, 0, "3");
            CreateSheetView(fpSpread1.Sheets[11], 1, 1, 9, 1, "全地区");
            for (int i = 0; i < 7; i ++)
            {
                CreateSheetView(fpSpread1.Sheets[11], 1, 1, 3 + i, 2, "10");
                
            }
            Sheet_GridandColor(fpSpread1.Sheets[11], 10, 13);
        }
        //市新农村电气化建设情况表
        private void sxncdqh()
        {
            //table
            sxncdqhtable();
            //数据


        }
        private void sxncdqhtable()
        {
          fpSpread1.Sheets[12].RowCount=0;
            fpSpread1.Sheets[12].ColumnCount=0;
            fpSpread1.Sheets[12].RowCount=8;
            fpSpread1.Sheets[12].ColumnCount=17;
             fpSpread1.Sheets[12].SetValue(0, 0, "2010年铜陵市新农村电气化建设情况");
            fpSpread1.Sheets[12].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[12].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[12].Cells[0, 0].ColumnSpan = 17;
              for (int i = 0; i < 9; i += 3)
            {
                fpSpread1.Sheets[12].Columns[4 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[12].Columns[13].CellType=percentcelltypes;
            fpSpread1.Sheets[12].Columns[15].CellType=percentcelltypes;
            fpSpread1.Sheets[12].Rows[1].CellType=texttype;
            fpSpread1.Sheets[12].Rows[2].CellType=texttype;
            CreateSheetView(fpSpread1.Sheets[12],2,1,1,0,"编号");
            CreateSheetView(fpSpread1.Sheets[12],2,1,1,1,"类型");
             CreateSheetView(fpSpread1.Sheets[12],1,3,1,2,"县");
             CreateSheetView(fpSpread1.Sheets[12], 1, 3, 1, 5, "乡（镇）");
             CreateSheetView(fpSpread1.Sheets[12], 1, 3, 1, 8, "行政村");
             CreateSheetView(fpSpread1.Sheets[12], 1, 5, 1, 11, "农村居民户情况");
             CreateSheetView(fpSpread1.Sheets[12], 2, 1, 1, 16, "县域电网薄弱环节环节个数（个）");
             for (int i = 0; i < 9; i += 3)
             {
                 CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 2 + i, "总数(个)");
                 CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 3 + i, "电气化数(个)");
                 SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 3 + i, "电气化数(个)");
                 CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 4 + i, "比例(%)");
             }
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 11, "总数(户)");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 12, "已通电(户)");
             SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 12, "电气化数(个)");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 13, "通电率(%)");
             SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 13, "通电率(%)");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 14, "其中：低电压户数（户）");
             SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 14, "其中：低电压户数（户）");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 2, 15, "低电压户比例(%)");
             SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 15, "低电压户比例(%)");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 3, 0, "1");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 3, 1, "县级供电区");
             SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 1, "其中：直供直管");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 4, 0, "1.1");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 4, 1, "其中：直供直管");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 5, 0, "1.2");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 5, 1, "控股");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 6, 0, "1.3");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 6, 1, "参股");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 7, 0, "1.3");
             CreateSheetView(fpSpread1.Sheets[12], 1, 1, 7, 1, "代管");
             Sheet_GridandColor(fpSpread1.Sheets[12], 8, 17);
             for (int i = 0; i <= 16;i++ )
             {
                 fpSpread1.Sheets[12].Cells[3, i].Locked = true;
             }
            // fpSpread1.Sheets[12].Rows[3].Locked = true;
             fpSpread1.Sheets[12].Cells[3, 2].Formula = "R5C3+R6C3+R7C3+R8C3";
             fpSpread1.Sheets[12].Cells[3, 3].Formula = "R5C4+R6C4+R7C4+R8C4";
             fpSpread1.Sheets[12].Cells[3, 5].Formula = "R5C6+R6C6+R7C6+R8C6";
             fpSpread1.Sheets[12].Cells[3, 6].Formula = "R5C7+R6C7+R7C7+R8C7";
             fpSpread1.Sheets[12].Cells[3, 8].Formula = "R5C9+R6C9+R7C9+R8C9";
             fpSpread1.Sheets[12].Cells[3, 9].Formula = "R5C10+R6C10+R7C10+R8C10";

             fpSpread1.Sheets[12].Cells[3, 11].Formula = "R5C12+R6C12+R7C12+R8C12";
             fpSpread1.Sheets[12].Cells[3, 12].Formula = "R5C13+R6C13+R7C13+R8C13";
             fpSpread1.Sheets[12].Cells[3, 14].Formula = "R5C15+R6C15+R7C15+R8C15";
             fpSpread1.Sheets[12].Cells[3, 16].Formula = "R5C17+R6C17+R7C17+R8C17";
             fpSpread1.Sheets[12].Cells[3, 4].Formula = "R4C4/R4C3";
             fpSpread1.Sheets[12].Cells[4, 4].Formula = "R5C4/R5C3";
             fpSpread1.Sheets[12].Cells[5, 4].Formula = "R6C4/R6C3";
             fpSpread1.Sheets[12].Cells[6, 4].Formula = "R7C4/R7C3";
             fpSpread1.Sheets[12].Cells[7, 4].Formula = "R8C4/R8C3";

             fpSpread1.Sheets[12].Cells[3, 7].Formula = "R4C7/R4C6";
             fpSpread1.Sheets[12].Cells[4, 7].Formula = "R5C7/R5C6";
             fpSpread1.Sheets[12].Cells[5, 7].Formula = "R6C7/R6C6";
             fpSpread1.Sheets[12].Cells[6,7].Formula = "R7C7/R7C6";
             fpSpread1.Sheets[12].Cells[7, 7].Formula = "R8C7/R8C6";

             fpSpread1.Sheets[12].Cells[3, 10].Formula = "R4C10/R4C9";
             fpSpread1.Sheets[12].Cells[4, 10].Formula = "R5C10/R5C9";
             fpSpread1.Sheets[12].Cells[5, 10].Formula = "R6C10/R6C9";
             fpSpread1.Sheets[12].Cells[6, 10].Formula = "R7C10/R7C9";
             fpSpread1.Sheets[12].Cells[7, 10].Formula = "R8C10/R8C9";

             fpSpread1.Sheets[12].Cells[3, 13].Formula = "R4C13/R4C12";
             fpSpread1.Sheets[12].Cells[4, 13].Formula = "R5C13/R5C12";
             fpSpread1.Sheets[12].Cells[5, 13].Formula = "R6C13/R6C12";
             fpSpread1.Sheets[12].Cells[6, 13].Formula = "R7C13/R7C12";
             fpSpread1.Sheets[12].Cells[7, 13].Formula = "R8C13/R8C12";

             fpSpread1.Sheets[12].Cells[3, 15].Formula = "R4C15/R4C12";
             fpSpread1.Sheets[12].Cells[4, 15].Formula = "R5C15/R5C12";
             fpSpread1.Sheets[12].Cells[5, 15].Formula = "R6C15/R6C12";
             fpSpread1.Sheets[12].Cells[6, 15].Formula = "R7C15/R7C12";
             fpSpread1.Sheets[12].Cells[7, 15].Formula = "R8C15/R8C12";


        }
        //市新农村电气化建设情况附表
        private void xncdqjsfb()
        {
            //table
            xncdqjsfbtable();
            //数据
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            int num = 0;
            foreach (PS_Table_AreaWH ps in list)
            {
                fpSpread1.Sheets[13].Rows.Add(4, 1);
                CreateSheetView(fpSpread1.Sheets[13], 1, 1, 4, 0, ps.Title);
                fpSpread1.Sheets[13].Cells[4, 5].Formula = "R5C5/R5C4";
                fpSpread1.Sheets[13].Cells[4, 8].Formula = "R5C8/R5C7";
                fpSpread1.Sheets[13].Cells[4, 11].Formula = "R5C11/R5C10";
                fpSpread1.Sheets[13].Cells[4, 13].Formula = "R5C13/R5C10";
                num++;
            }
           
            fpSpread1.Sheets[13].Cells[3,3].Formula="SUM(R5C4:R"+(4+num).ToString()+"C4)";
            fpSpread1.Sheets[13].Cells[3,4].Formula="SUM(R5C5:R"+(4+num).ToString()+"C5)";
            fpSpread1.Sheets[13].Cells[3,6].Formula="SUM(R5C7:R"+(4+num).ToString()+"C7)";
            fpSpread1.Sheets[13].Cells[3,7].Formula="SUM(R5C8:R"+(4+num).ToString()+"C8)";
            fpSpread1.Sheets[13].Cells[3,9].Formula="SUM(R5C10:R"+(4+num).ToString()+"C10)";
            fpSpread1.Sheets[13].Cells[3,10].Formula="SUM(R5C11:R"+(4+num).ToString()+"C11)";
            fpSpread1.Sheets[13].Cells[3,12].Formula="SUM(R5C13:R"+(4+num).ToString()+"C13)";
            fpSpread1.Sheets[13].Cells[3,14].Formula="SUM(R5C14:R"+(4+num).ToString()+"C14)";


            fpSpread1.Sheets[13].Cells[3,5].Formula="R4C5/R4C4";
            fpSpread1.Sheets[13].Cells[3,8].Formula = "R4C8/R4C7";
            fpSpread1.Sheets[13].Cells[3,11].Formula = "R4C11/R4C10";
            fpSpread1.Sheets[13].Cells[3, 13].Formula = "R4C13/R4C10";
            fpSpread1.Sheets[13].Rows.Remove(4 + num, 1);
            Sheet_GridandColor(fpSpread1.Sheets[13], 4 + num, 15);
        }
        private void xncdqjsfbtable()
        {
            fpSpread1.Sheets[13].RowCount = 0;
            fpSpread1.Sheets[13].ColumnCount = 0;
            fpSpread1.Sheets[13].RowCount = 5;
            fpSpread1.Sheets[13].ColumnCount = 15;
            fpSpread1.Sheets[13].SetValue(0, 0, "2010年铜陵市新农村电气化建设情况");
            fpSpread1.Sheets[13].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[13].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[13].Cells[0, 0].ColumnSpan = 15;
            for (int i = 0; i < 9; i += 3)
            {
                fpSpread1.Sheets[13].Columns[5 + i].CellType = percentcelltypes;
            }
            fpSpread1.Sheets[13].Columns[13].CellType = percentcelltypes;
            fpSpread1.Sheets[13].Rows[1].CellType = texttype;
            fpSpread1.Sheets[13].Rows[2].CellType = texttype;
            CreateSheetView(fpSpread1.Sheets[13], 2, 1, 1, 0, "县级供电区");
            
            CreateSheetView(fpSpread1.Sheets[13], 2, 1, 1, 1, "供电企业类型");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 1, "供电企业类型");
            CreateSheetView(fpSpread1.Sheets[13], 2, 1, 1, 2, "电气化县（是/否）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 2, "电气化县（是/否）");
            CreateSheetView(fpSpread1.Sheets[13], 1, 3, 1, 3, "乡（镇）");
            CreateSheetView(fpSpread1.Sheets[13], 1, 3, 1, 6, "行政村");
            CreateSheetView(fpSpread1.Sheets[13], 1, 5, 1, 9, "农村居民户情况");
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 1, 14, "县域电网薄弱环节环节个数（个）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 14, "县域电网薄弱环节环节个数（个）");
            for (int i = 0; i < 6; i += 3)
            {
                CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 3 + i, "总数(个)");
                CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 4 + i, "电气化数(个)");
                SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 4 + i, "电气化数(个)");
                CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 5 + i, "比例(%)");
            }
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 9, "总数(户)");
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 10, "已通电(户)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 10, "已通电(户)");
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 11, "通电率(%)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[13], 11, "通电率(%)");
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 12, "其中：低电压户数（户）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 12, "其中：低电压户数（户）");
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 2, 13, "低电压户比例(%)");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[12], 13, "低电压户比例(%)");
            CreateSheetView(fpSpread1.Sheets[13], 1, 1, 3, 0, "合计");
            for (int i = 3; i < 15;i++ )
            {
                fpSpread1.Sheets[13].Cells[3,i].Locked = true;
            }
           
        }
        //夏季和冬季典型日负荷曲线
        private void xddxrqx()
        {
            fpSpread1.Sheets[5].RowCount=0;
            fpSpread1.Sheets[5].ColumnCount=0;
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            int flag = 0; List<PS_Table_AreaWH> areaidlist = new List<PS_Table_AreaWH>();
            //统计哪些区域有这些数据 以及判断标题名称和总的列数 以及控制显示
            foreach (PS_Table_AreaWH area in list)
            {
                con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate) in('" + beginyear + "','"+lastyear+"') and AreaID='" + area.ID+ "' AND Season in('夏季','冬季')";
                IList list1 = Services.BaseService.GetList("SelectBurdenLineByType", con);
                if (list1.Count!=0)
                {
                    flag++;
                    areaidlist.Add(area);
                }
            }
            if (flag!=0)
            {
                fpSpread1.Sheets[5].RowCount = 31;
                fpSpread1.Sheets[5].ColumnCount = 7 * flag;
                for (int i = 0; i < areaidlist.Count;i++ )
                {
                    string tiltle = areaidlist[i].Title + beginyear.ToString()+"年/" + lastyear.ToString() + "年夏季和冬季典型日负荷曲线表";
                    Create_Dxtable(fpSpread1.Sheets[5], 1 + i * 7, tiltle, beginyear, lastyear, areaidlist[i].ID);
                }
            }
        }
        //月最大负荷曲线表
        private void yzdfhqx()
        {
            fpSpread1.Sheets[6].RowCount = 0;
            fpSpread1.Sheets[6].ColumnCount = 0;
            string con = "ProjectID='" + ProjectUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", con);
            int flag = 0; List<PS_Table_AreaWH> areaidlist = new List<PS_Table_AreaWH>();
            //统计哪些区域有这些数据 以及判断标题名称和总的列数 以及控制显示
            foreach (PS_Table_AreaWH area in list)
            {
                con = " uid like '%" + Itop.Client.MIS.ProgUID + "%' and  BurdenYear in('" + beginyear + "','" + lastyear + "') and AreaID='" + area.ID + "' ";
                IList list1 = Services.BaseService.GetList("SelectBurdenMonthByWhere", con);
                if (list1.Count != 0)
                {
                    flag++;
                    areaidlist.Add(area);
                }
            }
            if (flag != 0)
            {
                fpSpread1.Sheets[6].ColumnCount =15;
                fpSpread1.Sheets[6].RowCount =15 * flag;
                for (int i = 0; i < areaidlist.Count; i++)
                {
                    string tiltle = areaidlist[i].Title + beginyear.ToString() + "年/" + lastyear.ToString() + "年月最大负荷";
                    Create_YZDFHtable(fpSpread1.Sheets[6], 1 + i *15, tiltle, beginyear, lastyear, areaidlist[i].ID);
                }
            }
        }
 
        /// <summary>
        /// 构建经济发展预测表的初始表格式
        /// </summary>
        private void jjfzyctable()
        {
            fpSpread1.Sheets[7].RowCount = 0;
            fpSpread1.Sheets[7].ColumnCount = 0;
            fpSpread1.Sheets[7].RowCount = 10;
            fpSpread1.Sheets[7].ColumnCount = 9;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                   
                    //水平和垂直均居中对齐
                    fpSpread1.Sheets[7].Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    fpSpread1.Sheets[7].Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            fpSpread1.Sheets[7].SetValue(0, 0, "经济结构发展预测结果表");
            fpSpread1.Sheets[7].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[7].Cells[0, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            fpSpread1.Sheets[7].SetValue(1, 0, "单位：万元，元/人，%，万人");
           // fpSpread1.Sheets[7].Cells[0, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            fpSpread1.Sheets[7].Cells[1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            fpSpread1.Sheets[7].SetValue(2, 0, "序号");
            fpSpread1.Sheets[7].SetValue(2, 1, "指标名称");
            fpSpread1.Sheets[7].SetValue(2, 3, "2010");
            fpSpread1.Sheets[7].SetValue(2, 4, "2015");
            fpSpread1.Sheets[7].SetValue(2, 5, "2020");
            fpSpread1.Sheets[7].SetValue(2, 6, "十一五");
            fpSpread1.Sheets[7].SetValue(2, 7, "十二五");
            fpSpread1.Sheets[7].SetValue(2, 8, "十三五");
            for (int i = 0; i < 7;i++ )
            {
                fpSpread1.Sheets[7].SetValue(3 + i, 0, (i+1).ToString());
            }
            fpSpread1.Sheets[7].SetValue(3, 1, "全市生产总值");
            fpSpread1.Sheets[7].SetValue(4,1, "人均GDP");
            fpSpread1.Sheets[7].SetValue(5, 1, "第一产业比重");
            fpSpread1.Sheets[7].SetValue(6, 1, "第二产业比重");
            fpSpread1.Sheets[7].SetValue(7, 1, "第三产业比重");
            fpSpread1.Sheets[7].SetValue(8, 1, "全市总人口（万人）");
            SetSheetViewColumnsWhith(fpSpread1.Sheets[7], 1, "全市总人口（万人）");
            fpSpread1.Sheets[7].SetValue(9, 1, "城市人口（万人）");


        }
        /// <summary>

      
        //电网建设项目资金需求汇总表
        private class sdata
        {
            //存放工程名称
            public string title = "";
            //存放其他事项
            public string str1 = "";
            public string str2 = "";

        }
        //sd用于存放表格中那些手工写入的部分，每当更新数据时
        //先把表格中手写部分存起来，更新数据完成后再写回手写部分
        //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
        List<sdata> sd = new List<sdata>();
 
       
        //读取和保存其他中的数据
        private void Sheet_SaveData(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
            //存表2-2中人工输入的其他项，并与项目名称形成关联 title中存项目名称，str1存其他项的内容

            //存放数据时先清空这个列表
            sd.Clear();
            int row = tempsheet.RowCount;
            //循环读取表格中的两项内容，并写入sd中
            for (int i = 4; i < row; i++)
            {
                sdata stest = new sdata();
                stest.title = tempsheet.Cells[i, 0].Value.ToString();
                if (tempsheet.Cells[i, 5].Value != null)
                {
                    stest.str1 = tempsheet.Cells[i, 5].Value.ToString();
                }
                sd.Add(stest);
            }
        }

        private void Sheet_WirteData(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
            //用于更新数据后写回手写部分
            int row = tempsheet.RowCount;
            for (int i = 4; i < row; i++)
            {
                //for (int j = 0; j < sd.Count; j++)
                //{
                    //通过项目名称的比对来回写数据
                    if (tempsheet.Cells[i, 0].Value.ToString() == sd[i-4].title)
                    {
                        tempsheet.SetValue(i, 5, sd[i-4].str1);
                        //比对完与后删除这一项，有利于下次比对提高效率
                        //sd.Remove(sd[j]);
                       // break;
                    }
                //}

            }
        }

        //读取和保存220电力平衡中注记的内容
        List<sdata> phsd1 = new List<sdata>();
        List<sdata> phsd2 = new List<sdata>();
        List<sdata> phsd3 = new List<sdata>();
        private void Sheet_Saveph220Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
            //存表2-2中人工输入的其他项，并与项目名称形成关联 title中存项目名称，str1存其他项的内容

            //存放数据时先清空这个列表
            phsd1.Clear();
            phsd2.Clear();
            phsd3.Clear();
            int row = tempsheet.RowCount;
            int col = tempsheet.ColumnCount;
            //循环读取表格中的两项内容，并写入sd中
            for (int i = 2; i <=8; i++)
            {
                sdata stest1 = new sdata();
                stest1.title = tempsheet.Cells[row-1, 1].Value.ToString();
                //if (tempsheet.Cells[i, 10].Value != null)
                //{
                if (tempsheet.Cells[row-1,i].Value!=null)
                {
                    stest1.str1 = tempsheet.Cells[row - 1, i].Value.ToString();
                }
                
                //}
                phsd1.Add(stest1);
                sdata stest2 = new sdata();
                stest2.title = tempsheet.Cells[row - 4, 1].Value.ToString();
                if (tempsheet.Cells[row - 4, i].Value != null)
                {
                stest2.str1 = tempsheet.Cells[row - 4, i].Value.ToString();
                }
                phsd2.Add(stest2);
                sdata stest3 = new sdata();
                stest3.title = tempsheet.Cells[row - 6, 1].Value.ToString();
                if (tempsheet.Cells[row - 6, i].Value != null)
                {
                stest3.str1 = tempsheet.Cells[row - 6, i].Value.ToString();
                }
                phsd3.Add(stest3);
            }
        }

        private void Sheet_Wirteph220Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
            //用于更新数据后写回手写部分
            int row = tempsheet.RowCount;
            for (int i =2; i < 8; i++)
            {
                //for (int j = 0; j < sd.Count; j++)
                //{
                //通过项目名称的比对来回写数据
                if (tempsheet.Cells[row-1, 1].Value.ToString() == phsd1[i - 2].title)
                {
                    tempsheet.SetValue(row - 1, i, phsd1[i - 2].str1);
                    //比对完与后删除这一项，有利于下次比对提高效率
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 4, 1].Value.ToString() == phsd2[i - 2].title)
                {
                    tempsheet.SetValue(row - 4, i, phsd2[i - 2].str1);
                    //比对完与后删除这一项，有利于下次比对提高效率
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 6, 1].Value.ToString() == phsd3[i - 2].title)
                {
                    tempsheet.SetValue(row - 6, i, phsd3[i - 2].str1);
                    //比对完与后删除这一项，有利于下次比对提高效率
                    //sd.Remove(sd[j]);
                    // break;
                }
                //}

            }
        }
        //读取和保存110电力平衡中注记的内容
        List<sdata> phsd4 = new List<sdata>();
        List<sdata> phsd5 = new List<sdata>();
        List<sdata> phsd6 = new List<sdata>();
        private void Sheet_Saveph110Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
            //存表2-2中人工输入的其他项，并与项目名称形成关联 title中存项目名称，str1存其他项的内容

            //存放数据时先清空这个列表
            phsd4.Clear();
            phsd5.Clear();
            phsd6.Clear();
            int row = tempsheet.RowCount;
            int col = tempsheet.ColumnCount;
            //循环读取表格中的两项内容，并写入sd中
            for (int i = 2; i <= 7; i++)
            {
                sdata stest1 = new sdata();
                stest1.title = tempsheet.Cells[row - 1, 1].Value.ToString();
                //if (tempsheet.Cells[i, 10].Value != null)
                //{
                if (tempsheet.Cells[row - 1, i].Value != null)
                {
                    stest1.str1 = tempsheet.Cells[row - 1, i].Value.ToString();
                }

                //}
                phsd4.Add(stest1);
                sdata stest2 = new sdata();
                stest2.title = tempsheet.Cells[row - 4, 1].Value.ToString();
                if (tempsheet.Cells[row - 4, i].Value != null)
                {
                    stest2.str1 = tempsheet.Cells[row - 4, i].Value.ToString();
                }
                phsd5.Add(stest2);
                sdata stest3 = new sdata();
                stest3.title = tempsheet.Cells[row - 6, 1].Value.ToString();
                if (tempsheet.Cells[row - 6, i].Value != null)
                {
                    stest3.str1 = tempsheet.Cells[row - 6, i].Value.ToString();
                }
                phsd6.Add(stest3);
            }
        }

        private void Sheet_Wirteph110Data(FarPoint.Win.Spread.SheetView tempsheet)
        {
            //每个有手写的表格（不固定）都有两个方法，一个在更新前写入手写数据，一个在更新后写回手写数据
            //用于更新数据后写回手写部分
            int row = tempsheet.RowCount;
            for (int i = 2; i < 7; i++)
            {
                //for (int j = 0; j < sd.Count; j++)
                //{
                //通过项目名称的比对来回写数据
                if (tempsheet.Cells[row - 1, 1].Value.ToString() == phsd4[i - 2].title)
                {
                    tempsheet.SetValue(row - 1, i, phsd4[i - 2].str1);
                    //比对完与后删除这一项，有利于下次比对提高效率
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 4, 1].Value.ToString() == phsd5[i - 2].title)
                {
                    tempsheet.SetValue(row - 4, i, phsd5[i - 2].str1);
                    //比对完与后删除这一项，有利于下次比对提高效率
                    //sd.Remove(sd[j]);
                    // break;
                }
                if (tempsheet.Cells[row - 6, 1].Value.ToString() == phsd6[i - 2].title)
                {
                    tempsheet.SetValue(row - 6, i, phsd6[i - 2].str1);
                    //比对完与后删除这一项，有利于下次比对提高效率
                    //sd.Remove(sd[j]);
                    // break;
                }
                //}

            }
        }
        /// 创建需电量预测表结构和填充数据
        /// </summary>
        /// <param name="obj">SheetView对象</param>

        /// <param name="intrownum">行数</param>
        /// <param name="tiltle">高中低方案</param>
        /// <param name="report">预测方案</param>
        /// <param name="forcastindex">预测方法中的第几个</param>
        private int Create_xdltableAnddata(FarPoint.Win.Spread.SheetView tempsheet, int intrownum, string tiltle,  Ps_forecast_list report, int forcastindex)
        {
            int typeFlag2 = 2;
            int rownum = 0;
            PSP_Types qshydl = null, tdydl = null;
            IList<PSP_Types> qxqshydl = null, qxtdydl = null;
            string con = "ProjectID='" + ProjectUID + "'and Title like'全社会用电量%' and Flag2='" + typeFlag2 + "'";
            qshydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshydl != null)
            {
               
                rownum += 1;
                 tempsheet.Rows.Add(intrownum - 1, 1);
                 //tempsheet.Rows[intrownum - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                 tempsheet.Cells[intrownum - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
                 //tempsheet.Cells[intrownum - 1, 8].CellType = percentcelltypes;
                 tempsheet.Cells[intrownum - 1, 8].Formula = "POWER(R"+(intrownum).ToString()+"C8/R"+(intrownum).ToString()+"C3,0.2)-1";
                 //tempsheet.Cells[intrownum - 1, 10].CellType = percentcelltypes;
                 tempsheet.Cells[intrownum - 1, 10].Formula = "POWER(R" + (intrownum).ToString() + "C10/R" + (intrownum).ToString() + "C8,0.2)-1";
                 tempsheet.SetValue(intrownum - 1, 1, "全社会用电量");
                 SetSheetViewColumnsWhith(tempsheet, 1, "全社会用电量用电量");
                 con = "ForecastID='" + report.ID + "'AND Title Like'全社会用电量%' AND Forecast!='0'AND ID like'%"+qshydl.ID+"%' order by Forecast";
                 IList<Ps_Forecast_Math> ycqshydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                 int forcast = 0;
                 
                 if (ycqshydl.Count >= forcastindex)
                {
                    forcast = ycqshydl[forcastindex - 1].Forecast;
                     for(int i=0;i<6;i++)
                     {
                         string year="y"+(2010+i).ToString();
                         //tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                         tempsheet.SetValue(intrownum - 1, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], year) * 10000);
                     }
                     tempsheet.SetValue(intrownum - 1,9, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], "y2020") * 10000);
                     //tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                }
                else
                 {
                     forcast = 0;
                     for (int i = 0; i < 6; i++)
                     {
                         string year = "y" + (2010 + i).ToString();
                         //tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                         tempsheet.SetValue(intrownum - 1, 2 + i, 0);
                     }
                     tempsheet.SetValue(intrownum - 1, 9, 0);
                     //tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                 }

                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'";
                qxqshydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxqshydl.Count;i++ )
                {
                    rownum++;
                    tempsheet.Rows.Add(intrownum, 1);
                    //tempsheet.Rows[intrownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum , 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //tempsheet.Cells[intrownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum, 8].Formula = "POWER(R" + (intrownum+1).ToString() + "C8/R" + (intrownum+1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum , 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum , 10].Formula = "POWER(R" + (intrownum+1).ToString() + "C10/R" + (intrownum+1).ToString() + "C8,0.2)-1";
                    if (i ==qxqshydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum, 1, "其中：" + qxqshydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum, 1, qxqshydl[i].Title);
                    if (ycqshydl.Count >= forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxqshydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + ycqshydl[forcastindex - 1].ID + "'AND ID like'%" + qxqshydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                            //tempsheet.Cells[intrownum, 9].CellType=numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum, 9, 0);
                           // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                            //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum, 9, 0);
                        //tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;
                    }
                  
                  

                }
                
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'统调用电量%' and Flag2='" + typeFlag2 + "'";
            tdydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (tdydl != null)
            {
                
                tempsheet.Rows.Add(intrownum+qxqshydl.Count, 1);
               
                //tempsheet.Rows[intrownum +qxqshydl.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                tempsheet.Cells[intrownum + qxqshydl.Count, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
               // tempsheet.Cells[intrownum + qxqshydl.Count, 8].CellType = percentcelltypes;
                tempsheet.Cells[intrownum + qxqshydl.Count, 8].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1).ToString() + "C8/R" + (intrownum + qxqshydl.Count + 1).ToString() + "C3,0.2)-1";
               // tempsheet.Cells[intrownum + qxqshydl.Count, 10].CellType = percentcelltypes;
                tempsheet.Cells[intrownum + qxqshydl.Count, 10].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1).ToString() + "C10/R" + (intrownum + qxqshydl.Count + 1).ToString() + "C8,0.2)-1";
                tempsheet.SetValue(intrownum + qxqshydl.Count, 1, "统调用电量");
                con = "ForecastID='" + report.ID + "'AND Title Like'统调用电量%' AND Forecast!='0'AND ID like'%" + tdydl.ID + "%' order by Forecast";
                IList<Ps_Forecast_Math> yctdydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                int forcast = 0;
                if (yctdydl.Count>=forcastindex)
                {
                    forcast = yctdydl[forcastindex - 1].Forecast;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                       // tempsheet.Cells[intrownum + qxqshydl.Count, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + qxqshydl.Count, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], year) * 10000);
                    }
                    tempsheet.SetValue(intrownum + qxqshydl.Count, 9, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], "y2020") * 10000);
                    //tempsheet.Cells[intrownum + qxqshydl.Count, 9].CellType = numberCellTypes1;
                }
                else
                {
                    forcast = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum + qxqshydl.Count, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + qxqshydl.Count, 2 + i, 0);
                    }
                    tempsheet.SetValue(intrownum + qxqshydl.Count, 9, 0);
                    //tempsheet.Cells[intrownum + qxqshydl.Count,9].CellType = numberCellTypes1;
                }
                rownum += 1;

                
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'";
                qxtdydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxtdydl.Count;i++ )
                {
                    rownum++;
                    tempsheet.Rows.Add(intrownum + qxqshydl.Count+1, 1);
                    //tempsheet.Rows[intrownum + qxqshydl.Count + 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum + qxqshydl.Count + 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum + qxqshydl.Count + 1, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + qxqshydl.Count + 1, 8].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C8/R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum + qxqshydl.Count + 1, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + qxqshydl.Count + 1, 10].Formula = "POWER(R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C10/R" + (intrownum + qxqshydl.Count + 1 + 1).ToString() + "C8,0.2)-1";
                    if (i == qxtdydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 1, "其中：" + qxtdydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 1, qxtdydl[i].Title);
                    if (qxtdydl.Count>=forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxtdydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + yctdydl[forcastindex - 1].ID + "'AND ID like'%" + qxtdydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math tydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (tydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum + qxqshydl.Count + 1, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(tydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 9, Gethistroyvalue<Ps_Forecast_Math>(tydl, "y2020") * 10000);
                            //tempsheet.Cells[intrownum + qxqshydl.Count + 1,9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum + qxqshydl.Count + 1, 2 + i].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 2 + i, 0);
                            }
                            tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 9, 0);
                           // tempsheet.Cells[intrownum + qxqshydl.Count + 1,9].CellType = numberCellTypes1;
                        }
                    }
                   else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                            //tempsheet.Cells[intrownum + qxqshydl.Count + 1, 2 + i].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 2 + i, 0);
                        }
                        tempsheet.SetValue(intrownum + qxqshydl.Count + 1, 9, 0);
                        //tempsheet.Cells[intrownum + qxqshydl.Count + 1,9].CellType = numberCellTypes1;
                    }

                }
             
            }
            //tempsheet.Cells[intrownum - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            tempsheet.SetValue(intrownum-1, 0, tiltle);
            tempsheet.Cells[intrownum-1, 0].RowSpan = rownum;           
            return rownum;
        }

        /// 创建最大负荷预测表结构和填充数据
        /// </summary>
        /// <param name="obj">SheetView对象</param>

        /// <param name="intrownum">行数</param>
        /// <param name="tiltle">高中低方案</param>
        /// <param name="report">预测方案</param>
        /// <param name="forcastindex">预测方法中的第几个</param>
        private int Create_zdfhtableAnddata(FarPoint.Win.Spread.SheetView tempsheet, int intrownum, string tiltle, Ps_forecast_list report, int forcastindex)
        {
            int typeFlag2 = 2;
            int rownum = 0;
            PSP_Types qshydl = null, tdydl = null;
            IList<PSP_Types> qxqshydl = null, qxtdydl = null;
            string con = "ProjectID='" + ProjectUID + "'and Title like'全社会最大负荷%' and Flag2='" + typeFlag2 + "'";
            qshydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            if (qshydl != null)
            {

                rownum += 1;
                tempsheet.Rows.Add(intrownum - 1, 1);
               // tempsheet.Rows[intrownum - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                tempsheet.Cells[intrownum - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
               // tempsheet.Cells[intrownum - 1, 8].CellType = percentcelltypes;
                tempsheet.Cells[intrownum - 1, 8].Formula = "POWER(R" + (intrownum).ToString() + "C8/R" + (intrownum).ToString() + "C3,0.2)-1";
               // tempsheet.Cells[intrownum - 1, 10].CellType = percentcelltypes;
                tempsheet.Cells[intrownum - 1, 10].Formula = "POWER(R" + (intrownum).ToString() + "C10/R" + (intrownum).ToString() + "C8,0.2)-1";
                tempsheet.SetValue(intrownum - 1, 1, "全社会最大负荷");
                SetSheetViewColumnsWhith(tempsheet, 1, "全社会用电量用电量");
                con = "ForecastID='" + report.ID + "'AND Title Like'全社会最大负荷%' AND Forecast!='0'AND ID like'%" + qshydl.ID + "%' order by Forecast";
                IList<Ps_Forecast_Math> ycqshydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                int forcast = 0;

                if (ycqshydl.Count >= forcastindex)
                {
                    forcast = ycqshydl[forcastindex - 1].Forecast;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                       // tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum - 1, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], year));
                    }
                    tempsheet.SetValue(intrownum - 1, 9, Gethistroyvalue<Ps_Forecast_Math>(ycqshydl[forcastindex - 1], "y2020") );
                    tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                }
                else
                {
                    forcast = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum - 1, 2 + i, 0);
                    }
                    tempsheet.SetValue(intrownum - 1, 9, 0);
                    //tempsheet.Cells[intrownum - 1, 9].CellType = numberCellTypes1;
                }

                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'and Title !='最大负荷利用小时数'";
                qxqshydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxqshydl.Count; i++)
                {
                    rownum++;
                    tempsheet.Rows.Add(intrownum, 1);
                   // tempsheet.Rows[intrownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum, 8].Formula = "POWER(R" + (intrownum + 1).ToString() + "C8/R" + (intrownum + 1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum, 10].Formula = "POWER(R" + (intrownum + 1).ToString() + "C10/R" + (intrownum + 1).ToString() + "C8,0.2)-1";
                    if (i == qxqshydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum, 1, "其中：" + qxqshydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum, 1, qxqshydl[i].Title);
                    if (ycqshydl.Count >= forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxqshydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + ycqshydl[forcastindex-1].ID + "'AND ID like'%" + qxqshydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;                     
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum, 9, 0);
                           // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;    
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                            //tempsheet.Cells[intrownum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum, 9, 0);
                       // tempsheet.Cells[intrownum, 9].CellType = numberCellTypes1;    
                    }
                   

                }
                tempsheet.Rows.Add(intrownum+rownum-1, 1);
                tempsheet.SetValue(intrownum+rownum-1,1,"同时率");
                for (int i=0;i<6;i++)
                {
                     tempsheet.Cells[intrownum+rownum-1,2+i].Formula="R"+(intrownum ).ToString()+"C"+(3+i).ToString()+"/SUM(R"+(intrownum+1).ToString()+"C"+(3+i).ToString()+":R"+(intrownum+qxqshydl.Count).ToString()+"C"+(3+i).ToString()+")";

                }
                tempsheet.Cells[intrownum+rownum-1,9].Formula="R"+(intrownum ).ToString()+"C10/SUM(R"+(intrownum+1).ToString()+"C10:R"+(intrownum+qxqshydl.Count).ToString()+"C10)";
                rownum++;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + qshydl.ID + "' and Flag2='" + typeFlag2 + "'and Title LIKE'最大负荷利用小时数%'";
                PSP_Types zudfh = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (zudfh!=null)
                {
                    tempsheet.Rows.Add(intrownum+rownum,1);
                    tempsheet.SetValue(intrownum+rownum,1,"最大负荷利用小时数");
                     //tempsheet.Rows[intrownum+rownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum+rownum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //tempsheet.Cells[intrownum+rownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+rownum, 8].Formula = "POWER(R" + (intrownum+rownum + 1).ToString() + "C8/R" + (intrownum+rownum + 1).ToString() + "C3,0.2)-1";
                    //tempsheet.Cells[intrownum+rownum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+rownum, 10].Formula = "POWER(R" + (intrownum+rownum + 1).ToString() + "C10/R" + (intrownum+rownum + 1).ToString() + "C8,0.2)-1";
                    if (ycqshydl.Count>=forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'最大负荷利用小时数%' AND Forecast='" + forcast + "' AND ParentID='" + ycqshydl[forcastindex-1].ID + "'AND ID like'%" + zudfh.ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                string year = "y" + (2010 + i).ToString();
                                //tempsheet.Cells[intrownum + rownum, 2 + i].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int i = 0; i < 6; i++)
                            {
                                string year = "y" + (2010 + i).ToString();
                               // tempsheet.Cells[intrownum + rownum, 2 + i].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + i, 0);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, 0);
                            //tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }
                    }
                   else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            string year = "y" + (2010 + i).ToString();
                            //tempsheet.Cells[intrownum + rownum, 2 + i].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + rownum, 2 + i, 0);
                        }
                        tempsheet.SetValue(intrownum + rownum, 9, 0);
                        //tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                    }
                    rownum++;

                }
                 
               
            }
            con = "ProjectID='" + ProjectUID + "'and Title like'统调最大负荷%' and Flag2='" + typeFlag2 + "'";
            tdydl = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
            int zongnum=rownum;
            if (tdydl != null)
            {

                tempsheet.Rows.Add(intrownum + rownum-1, 1);

                //tempsheet.Rows[intrownum +  rownum-1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                tempsheet.Cells[intrownum +  rownum-1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
               // tempsheet.Cells[intrownum +  rownum-1, 8].CellType = percentcelltypes;
                tempsheet.Cells[intrownum +  rownum-1, 8].Formula = "POWER(R" + (intrownum + rownum-1 + 1).ToString() + "C8/R" + (intrownum + rownum-1 + 1).ToString() + "C3,0.2)-1";
                //tempsheet.Cells[intrownum +  rownum-1, 10].CellType = percentcelltypes;
                tempsheet.Cells[intrownum +  rownum-1, 10].Formula = "POWER(R" + (intrownum + rownum-1 + 1).ToString() + "C10/R" + (intrownum + rownum-1 + 1).ToString() + "C8,0.2)-1";
                tempsheet.SetValue(intrownum +  rownum-1, 1, "统调最大负荷");
                con = "ForecastID='" + report.ID + "'AND Title Like'统调最大负荷%' AND Forecast!='0'AND ID like'%" + tdydl.ID + "%' order by Forecast";
                IList<Ps_Forecast_Math> yctdydl = Services.BaseService.GetList<Ps_Forecast_Math>("SelectPs_Forecast_MathByWhere", con);
                int forcast = 0;
                if (yctdydl.Count >= forcastindex)
                {
                    forcast = yctdydl[forcastindex - 1].Forecast;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum + rownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + rownum-1, 2 + i, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], year) * 10000);
                    }
                    tempsheet.SetValue(intrownum + rownum-1, 9, Gethistroyvalue<Ps_Forecast_Math>(yctdydl[forcastindex - 1], "y2020") * 10000);
                    //tempsheet.Cells[intrownum + rownum - 1, 9].CellType = numberCellTypes1;

                }
                else
                {
                    forcast = 0;
                    for (int i = 0; i < 6; i++)
                    {
                        string year = "y" + (2010 + i).ToString();
                        //tempsheet.Cells[intrownum + rownum - 1, 2 + i].CellType = numberCellTypes1;
                        tempsheet.SetValue(intrownum + rownum-1, 2 + i, 0);
                    }
                    tempsheet.SetValue(intrownum + rownum-1, 9, 0);
                    //tempsheet.Cells[intrownum + rownum - 1, 9].CellType = numberCellTypes1;
                }
                zongnum += 1;

                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'and Title !='最大负荷利用小时数'";
                qxtdydl = Services.BaseService.GetList<PSP_Types>("SelectPSP_TypesByWhere", con);
                for (int i = 0; i < qxtdydl.Count; i++)
                {
                    zongnum++;
                    tempsheet.Rows.Add(intrownum + rownum, 1);
                   // tempsheet.Rows[intrownum + rownum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum +rownum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum + rownum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + rownum, 8].Formula = "POWER(R" + (intrownum +rownum + 1).ToString() + "C8/R" + (intrownum + rownum+ 1).ToString() + "C3,0.2)-1";
                   // tempsheet.Cells[intrownum + rownum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum + rownum, 10].Formula = "POWER(R" + (intrownum + rownum+ 1).ToString() + "C10/R" + (intrownum +rownum + 1).ToString() + "C8,0.2)-1";
                    if (i == qxtdydl.Count-1)
                    {
                        tempsheet.SetValue(intrownum + rownum, 1, "其中：" + qxtdydl[i].Title);
                    }
                    else
                        tempsheet.SetValue(intrownum + rownum, 1, qxtdydl[i].Title);
                    if (yctdydl.Count>=forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'" + qxtdydl[i].Title + "%' AND Forecast='" + forcast + "' AND ParentID='" + yctdydl[forcastindex - 1].ID + "'AND ID like'%" + qxtdydl[i].ID + "%' order by Forecast";
                        Ps_Forecast_Math tydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (tydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                               // tempsheet.Cells[intrownum + rownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(tydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, Gethistroyvalue<Ps_Forecast_Math>(tydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum + rownum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + rownum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum + rownum, 9, 0);
                            //tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                        }

                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                           // tempsheet.Cells[intrownum + rownum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + rownum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum + rownum, 9, 0);
                       // tempsheet.Cells[intrownum + rownum, 9].CellType = numberCellTypes1;
                    }
                   
                }
                 tempsheet.Rows.Add(intrownum+rownum+qxtdydl.Count, 1);
                 tempsheet.SetValue(intrownum + rownum + qxtdydl.Count, 1, "同时率");
                for (int i=0;i<6;i++)
                {
                     tempsheet.Cells[intrownum+rownum+qxtdydl.Count,2+i].Formula="R"+(intrownum + rownum ).ToString()+"C"+(3+i).ToString()+"/SUM(R"+(intrownum + rownum+1).ToString()+"C"+(3+i).ToString()+":R"+(intrownum+rownum+qxtdydl.Count).ToString()+"C"+(3+i).ToString()+")";

                }
                tempsheet.Cells[intrownum+rownum+qxtdydl.Count,9].Formula="R"+(intrownum+ rownum ).ToString()+"C10/SUM(R"+(intrownum+ rownum+1).ToString()+"C10:R"+(intrownum+rownum+qxtdydl.Count).ToString()+"C10)";
                zongnum++;
                con = "ProjectID='" + ProjectUID + "'and ParentID='" + tdydl.ID + "' and Flag2='" + typeFlag2 + "'and Title LIKE'最大负荷利用小时数%'";
                PSP_Types zudfh = (PSP_Types)Services.BaseService.GetObject("SelectPSP_TypesByWhere", con);
                if (zudfh!=null)
                {
                    tempsheet.Rows.Add(intrownum+zongnum,1);
                    tempsheet.SetValue(intrownum+zongnum,1,"最大负荷利用小时数");
                    // tempsheet.Rows[intrownum+zongnum].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[intrownum+zongnum, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                   // tempsheet.Cells[intrownum+zongnum, 8].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+zongnum, 8].Formula = "POWER(R" + (intrownum+zongnum + 1).ToString() + "C8/R" + (intrownum+zongnum+ 1).ToString() + "C3,0.2)-1";
                  //  tempsheet.Cells[intrownum+zongnum, 10].CellType = percentcelltypes;
                    tempsheet.Cells[intrownum+zongnum, 10].Formula = "POWER(R" + (intrownum+zongnum + 1).ToString() + "C10/R" + (intrownum+zongnum+ 1).ToString() + "C8,0.2)-1";
                    if (yctdydl.Count >= forcastindex)
                    {
                        con = "ForecastID='" + report.ID + "'AND Title Like'最大负荷利用小时数%' AND Forecast='" + forcast + "' AND ParentID='" + yctdydl[forcastindex].ID + "'AND ID like'%" + zudfh.ID + "%' order by Forecast";
                        Ps_Forecast_Math qydl = (Ps_Forecast_Math)Services.BaseService.GetObject("SelectPs_Forecast_MathByWhere", con);
                        if (qydl != null)
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum + zongnum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + zongnum, 2 + j, Gethistroyvalue<Ps_Forecast_Math>(qydl, year) * 10000);
                            }
                            tempsheet.SetValue(intrownum + zongnum, 9, Gethistroyvalue<Ps_Forecast_Math>(qydl, "y2020") * 10000);
                           // tempsheet.Cells[intrownum + zongnum, 9].CellType = numberCellTypes1;
                        }
                        else
                        {
                            for (int j = 0; j < 6; j++)
                            {
                                string year = "y" + (2010 + j).ToString();
                                //tempsheet.Cells[intrownum + zongnum, 2 + j].CellType = numberCellTypes1;
                                tempsheet.SetValue(intrownum + zongnum, 2 + j, 0);
                            }
                            tempsheet.SetValue(intrownum + zongnum, 9, 0);
                           // tempsheet.Cells[intrownum + zongnum, 9].CellType = numberCellTypes1;
                        }
                    }
                    else
                    {
                        for (int j = 0; j < 6; j++)
                        {
                            string year = "y" + (2010 + j).ToString();
                           // tempsheet.Cells[intrownum + zongnum, 2 + j].CellType = numberCellTypes1;
                            tempsheet.SetValue(intrownum + zongnum, 2 + j, 0);
                        }
                        tempsheet.SetValue(intrownum + zongnum, 9, 0);
                       // tempsheet.Cells[intrownum + zongnum, 9].CellType = numberCellTypes1;
                    }
                    zongnum++;

                }
                 

            }
            tempsheet.Cells[intrownum - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            tempsheet.SetValue(intrownum - 1, 0, tiltle);
            tempsheet.Cells[intrownum-1, 0].RowSpan =zongnum;
            return zongnum;
        }
        /// 典型日曲线表设置单元格列行的样式 同时添加数据
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        
        /// <param name="colnum">列数</param>
        /// <param name="tiltle">表头名称</param>
        /// 
        /// <param name="firyear">比较第一年</param>
        /// <param name="endyear">比较第二年</param>
        /// <param name="Areaid">区域</param>
        private void Create_Dxtable(FarPoint.Win.Spread.SheetView tempsheet,int colnum,string tiltle,int firyear,int endyear,string Areaid)
        {
            //获取firyear和endyear 冬季和夏季的数据
            string con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + firyear + "' and AreaID='" + Areaid + "' AND Season='夏季'";
            BurdenLine firsumbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + firyear + "' and AreaID='" + Areaid + "' AND Season='冬季'";
            BurdenLine firsnowbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + endyear + "' and AreaID='" + Areaid + "' AND Season='夏季'";
            BurdenLine endsumbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            con = "IsType = '1' and  uid like '%" + Itop.Client.MIS.ProgUID + "%' and  year(BurdenDate)='" + endyear + "' and AreaID='" + Areaid + "' AND Season='冬季'";
            BurdenLine endsnowbl = (BurdenLine)Services.BaseService.GetObject("SelectBurdenLineByType", con);
            for (int i = 0; i < 31; i++)
            {
                for (int j = colnum-1; j < colnum+5; j++)
                {
                    //设表格线
                    tempsheet.Cells[i, j].Border = border;
                    //水平和垂直均居中对齐
                    tempsheet.Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            tempsheet.Cells[0, colnum-1].ColumnSpan = 6;
            tempsheet.Cells[0, colnum - 1].Font = new Font("宋体", 9, FontStyle.Bold);
            tempsheet.SetValue(0, colnum - 1, tiltle);
            tempsheet.Cells[1, colnum - 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            tempsheet.SetValue(1, colnum - 1, "单位：万千瓦");
            tempsheet.Cells[1, colnum - 1].ColumnSpan = 6;
            tempsheet.SetValue(2, colnum - 1, firstyear.ToString() + "年");
            tempsheet.Cells[2, colnum].ColumnSpan = 2;
            tempsheet.SetValue(2, colnum, "典型负荷日");
            tempsheet.SetValue(2, colnum +2,endyear.ToString() + "年");
            tempsheet.Cells[2,colnum+3].ColumnSpan=2;
            tempsheet.SetValue(2, colnum + 3, "典型负荷日");
            tempsheet.SetValue(3,colnum,"夏季");
            tempsheet.SetValue(3,colnum+1,"冬季");
            tempsheet.SetValue(3,colnum+3,"夏季");
            tempsheet.SetValue(3,colnum+4,"冬季");
            tempsheet.SetValue(4,colnum-1,"小时");
            tempsheet.SetValue(4,colnum+2,"小时");
            for (int i=1;i<=24;i++)
            {
              tempsheet.SetValue(4+i,colnum-1,i.ToString());
                if (firsumbl!=null)
                {
                    string name = "Hour" + i.ToString();
                    tempsheet.SetValue(4 + i, colnum, Gethistroyvalue<BurdenLine>(firsumbl,name));
                }
                else
                    tempsheet.SetValue(4 + i, colnum, 0);
                if (firsnowbl!=null)
                {
                    string name = "Hour" + i.ToString();
                    tempsheet.SetValue(4 + i, colnum+1, Gethistroyvalue<BurdenLine>(firsnowbl, name));
                }
                else
                    tempsheet.SetValue(4 + i, colnum+1, 0);
              tempsheet.SetValue(4+i,colnum+2,i.ToString());
              if (endsumbl != null)
              {
                  string name = "Hour" + i.ToString();
                  tempsheet.SetValue(4 + i, colnum+3, Gethistroyvalue<BurdenLine>(endsumbl, name));
              }
              else
                  tempsheet.SetValue(4 + i, colnum+3, 0);
              if (endsnowbl != null)
              {
                  string name = "Hour" + i.ToString();
                  tempsheet.SetValue(4 + i, colnum + 4, Gethistroyvalue<BurdenLine>(endsnowbl, name));
              }
              else
                  tempsheet.SetValue(4 + i, colnum +4, 0);
            }
            tempsheet.Cells[29, colnum].CellType = percentcelltypes;
            tempsheet.Cells[29, colnum+1].CellType = percentcelltypes;
            tempsheet.Cells[29, colnum+3].CellType = percentcelltypes;
            tempsheet.Cells[29, colnum+4].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum + 1].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum + 3].CellType = percentcelltypes;
            tempsheet.Cells[30, colnum + 4].CellType = percentcelltypes;
            tempsheet.SetValue(29,colnum-1,"日平均负荷率");
            SetSheetViewColumnsWhith(tempsheet, colnum - 1, "日平均负荷率");
            tempsheet.SetValue(29, colnum + 2, "日平均负荷率");
            SetSheetViewColumnsWhith(tempsheet, colnum +2, "日平均负荷率");
            tempsheet.SetValue(30, colnum - 1, "日最小负荷率");
            tempsheet.SetValue(30, colnum + 2, "日最小负荷率");
            if (firsumbl!=null)
            {
              tempsheet.SetValue(29,colnum,Gethistroyvalue<BurdenLine>(firsumbl, "DayAverage"));
              tempsheet.SetValue(30, colnum, Gethistroyvalue<BurdenLine>(firsumbl,"MinAverage" ));
            }
          else
            {
                tempsheet.SetValue(29, colnum, 0);
                tempsheet.SetValue(30, colnum, 0);
            }
            if (firsnowbl != null)
            {
                tempsheet.SetValue(29, colnum+1, Gethistroyvalue<BurdenLine>(firsnowbl, "DayAverage"));
                tempsheet.SetValue(30, colnum+1, Gethistroyvalue<BurdenLine>(firsnowbl, "MinAverage"));
            }
            else
            {
                tempsheet.SetValue(29, colnum+1, 0);
                tempsheet.SetValue(30, colnum+1, 0);
            }

            if (endsumbl != null)
            {
                tempsheet.SetValue(29, colnum+3, Gethistroyvalue<BurdenLine>(endsumbl, "DayAverage"));
                tempsheet.SetValue(30, colnum+3, Gethistroyvalue<BurdenLine>(endsumbl, "MinAverage"));
            }
            else
            {
                tempsheet.SetValue(29, colnum+3, 0);
                tempsheet.SetValue(30, colnum+3, 0);
            }
            if (endsnowbl != null)
            {
                tempsheet.SetValue(29, colnum + 4, Gethistroyvalue<BurdenLine>(endsnowbl, "DayAverage"));
                tempsheet.SetValue(30, colnum + 4, Gethistroyvalue<BurdenLine>(endsnowbl, "MinAverage"));
            }
            else
            {
                tempsheet.SetValue(29, colnum + 4, 0);
                tempsheet.SetValue(30, colnum + 4, 0);
            }
          
        }
          /// <summary>
        ///月最大负荷数据表设置单元格列行的样式 同时添加数据
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        
        /// <param name="colnum">行数</param>
        /// <param name="tiltle">表头名称</param>
        /// <param name="firyear">比较第一年</param>
        /// <param name="endyear">比较第二年</param>
        /// <param name="Areaid">区域</param>
        private void Create_YZDFHtable(FarPoint.Win.Spread.SheetView tempsheet,int rownum,string tiltle,int firyear,int endyear,string Areaid)
        {
            //获取数据
            string con = "uid like '%" + Itop.Client.MIS.ProgUID + "%' and BurdenYear='"+firyear+"'and AreaID='"+Areaid+"'";
            BurdenMonth firbm = (BurdenMonth)Services.BaseService.GetObject("SelectBurdenMonthByWhere", con);

            con = "uid like '%" + Itop.Client.MIS.ProgUID + "%' and BurdenYear='" + endyear + "'and AreaID='" + Areaid + "'";
            BurdenMonth endbm = (BurdenMonth)Services.BaseService.GetObject("SelectBurdenMonthByWhere", con);

            for (int i = rownum - 1; i < rownum+13; i++)
            {
                for (int j = 0; j < 15; j++)
                {
                    //设表格线
                    tempsheet.Cells[i, j].Border = border;
                    //水平和垂直均居中对齐
                    tempsheet.Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    tempsheet.Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }
            tempsheet.Cells[rownum-1,0].ColumnSpan = 15;
            tempsheet.Cells[rownum - 1, 0].Font = new Font("宋体", 9, FontStyle.Bold);
            tempsheet.SetValue(rownum - 1, 0, tiltle);
            tempsheet.Cells[rownum, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            tempsheet.SetValue(rownum, 0, "单位：万千瓦");
            tempsheet.Cells[rownum, 0].ColumnSpan = 15;

            tempsheet.SetValue(rownum + 1, 0, "年份");
            tempsheet.Cells[rownum + 1, 0].RowSpan = 2;
            tempsheet.SetValue(rownum + 1, 1, "指标");
            tempsheet.Cells[rownum + 1, 1].RowSpan = 2;
            tempsheet.SetValue(rownum + 1, 2, "单位");
            tempsheet.Cells[rownum + 1, 2].RowSpan = 2;
            tempsheet.SetValue(rownum + 1, 3, "各月数据");
            tempsheet.Cells[rownum + 1, 3].ColumnSpan = 12;
            tempsheet.SetValue(rownum + 3,0,firyear+ "年");
            tempsheet.Cells[rownum + 3, 0].RowSpan = 5;
            tempsheet.SetValue(rownum + 3, 1, "月最大负荷");
            tempsheet.SetValue(rownum + 3, 2, "万千瓦");
            tempsheet.SetValue(rownum + 4, 1, "月平均日负荷率");
            tempsheet.SetValue(rownum + 4, 2, "%");

            tempsheet.SetValue(rownum + 5, 1, "月最小日负荷率");
            tempsheet.SetValue(rownum + 5,2, "%");
            tempsheet.SetValue(rownum + 6, 1, "月最大日峰谷差");
            tempsheet.SetValue(rownum + 6, 2, "万千瓦");

            tempsheet.SetValue(rownum + 7, 1, "月最大日峰谷差率");
            tempsheet.SetValue(rownum + 7, 2, "%");

            tempsheet.SetValue(rownum + 8, 0, endyear + "年");
            tempsheet.Cells[rownum +8, 0].RowSpan = 5;
            tempsheet.SetValue(rownum + 8, 1, "月最大负荷");
            tempsheet.SetValue(rownum + 8, 2, "万千瓦");
            tempsheet.SetValue(rownum + 9, 1, "月平均日负荷率");
            tempsheet.SetValue(rownum + 9, 2, "%");

            tempsheet.SetValue(rownum + 10, 1, "月最小日负荷率");
            tempsheet.SetValue(rownum +10, 2, "%");
            tempsheet.SetValue(rownum + 11, 1, "月最大日峰谷差");
            tempsheet.SetValue(rownum + 11, 2, "万千瓦");

            tempsheet.SetValue(rownum + 12, 1, "月最大日峰谷差率");
            tempsheet.SetValue(rownum + 12, 2, "%");
            for (int i = 1; i <= 12;i++ )
            {
                tempsheet.SetValue(rownum + 2, 2+i,i.ToString());
                tempsheet.Cells[rownum + 3,2+i].CellType = numberCellTypes1;
                if (firbm!=null)
                {
                    tempsheet.SetValue(rownum + 3, 2 + i, Gethistroyvalue<BurdenMonth>(firbm, "Month" + i.ToString()));
                }
                else
                    tempsheet.SetValue(rownum + 3, 2 + i, 0);
                //在此添加第一年数据
                tempsheet.Cells[rownum + 4, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 5, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 6, 2 + i].CellType =numberCellTypes1;
                tempsheet.Cells[rownum +7, 2 + i].CellType = percentcelltypes;

                //在此添加第二年数据
                tempsheet.Cells[rownum + 8, 2 + i].CellType = numberCellTypes1;
                if (endbm != null)
                {
                    tempsheet.SetValue(rownum +8, 2 + i, Gethistroyvalue<BurdenMonth>(endbm, "Month" + i.ToString()));
                }
                else
                    tempsheet.SetValue(rownum + 8, 2 + i, 0);
                tempsheet.Cells[rownum + 9, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 10, 2 + i].CellType = percentcelltypes;
                tempsheet.Cells[rownum + 11, 2 + i].CellType = numberCellTypes1;
                tempsheet.Cells[rownum + 12, 2 + i].CellType = percentcelltypes;
            }

        }
        /// <summary>
        /// 设置单元格列的宽度
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        /// <param name="rownum">行数</param>
        /// <param name="colnum">列数</param>
        private void Sheet_GridandColor(FarPoint.Win.Spread.SheetView tempsheet,int rownum,int colnum)
        {
            //设置表格线
            //设置单元格内容居中
            //int rowcount = tempsheet.Rows.Count;
            //int colcount = tempsheet.Columns.Count;
            //for (int i = 0; i < colnum;i++ )
            //{
            //    tempsheet.Columns[i].Border = border;
            //}
            for (int i = 0; i < rownum; i++)
            {
                for (int j = 0; j < colnum; j++)
                {
                    //设表格线
                    tempsheet.Cells[i, j].Border = border;
                    //水平和垂直均居中对齐
                    //tempsheet.Cells[i, j].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    //tempsheet.Cells[i, j].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                }
            }

        }
        /// <summary>
        /// 设置单元格列的宽度
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        /// <param name="Col">哪个列</param>
        /// <param name="Title">名字</param>
        private void SetSheetViewColumnsWhith(FarPoint.Win.Spread.SheetView obj, int Col, string Title)
        {
            int len = 0;
            const int Pixe = 12;//一个汉字是十个字节这里我多加2个
            len = Title.Length * Pixe;
            obj.SetColumnWidth(Col, len);
        }
        /// <summary>
        ///添加列和添加标题
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        /// <param name="intCol">起始列</param>
        /// <param name="colnum">列的数目</param>
        /// <param name="rowtitle">哪一行为标题行</param>
        /// <param name="Title">列的标题名</param>
        private void AddcolAndtitle(FarPoint.Win.Spread.SheetView obj, int intCol,int colnum,int rowtitle, List<string> Title)
        {
            for (int i = 0; i < colnum;i++ )
            {
                obj.Columns.Add(intCol, 1);
                obj.SetValue(rowtitle, intCol, Title[colnum - 1 - i]);
                //设定宽度
                SetSheetViewColumnsWhith(obj, intCol, Title[colnum - 1 - i]);
            }
            
        }
        /// <summary>
        ///添加列和添加标题
        /// </summary>
        /// <param name="obj">SheetView对象</param>
        /// <param name="intCol">起始列</param>
        /// <param name="rownum">列的数目</param>
        /// <param name="coltitle">哪一列为标题行</param>
        /// <param name="Title">列的标题名</param>
        private void AddrowAndtitle(FarPoint.Win.Spread.SheetView obj, int intRow, int rownum, int coltitle, List<string> Title)
        {
            for (int i = 0; i < rownum; i++)
            {
                obj.Rows.Add(intRow, 1);
                obj.SetValue(intRow, coltitle, Title[rownum - 1 - i]);
                //设定宽度
                SetSheetViewColumnsWhith(obj, coltitle, Title[rownum - 1 - i]);
            }

        }
        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="obj">传入对象</param>
        /// <param name="RowStep">要合并单元格的数量,行</param>
        /// <param name="ColStep">要合并单元格的数量,列</param>
        /// <param name="Row">行</param>
        /// <param name="Col">列</param>
        /// <param name="Title">标题</param>
        public void CreateSheetView(FarPoint.Win.Spread.SheetView obj, int RowStep, int ColStep, int Row, int Col, object Title)
        {
            FarPoint.Win.Spread.Cell acell;
            acell = obj.Cells[Row, Col];
            acell.ColumnSpan = ColStep;
            acell.RowSpan = RowStep;
            obj.SetValue(Row, Col, Title);
            //acell.Text = Title;

            acell.HorizontalAlignment = HAlignment;
            acell.VerticalAlignment = VAlignment;
        }
        /// <summary>
        /// 获得对象中某一属性的数值
        /// </summary>
        /// <param name="Ps">对象</param>
        /// <param name="name">数值</param>
       
        private double Gethistroyvalue<T>(T ps,string name)
        {
            Type type = typeof(T);
            double psvalue = 0.0;
            foreach (PropertyInfo pi in type.GetProperties())
            {
                
                if (pi.Name == name)
                {
                   psvalue =Convert.ToDouble(pi.GetValue(ps,null) );
                   break;
                }
                    //pi.SetValue(dataObject, treeNode.GetValue(pi.Name), null);
                
            }
            return psvalue;
        }
        /// <summary>
        /// 给对象中某一属性赋值数值
        /// </summary>
        /// <param name="Ps">对象</param>
        /// <param name="name">数值</param>
        private void sethistoyvalue<T>(T ps,string name,object obj)
        {
            Type type = typeof(T);
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.Name==name)
                {
                    
                    try
                    {
                        pi.SetValue(ps, obj != DBNull.Value ? obj : 0, null);
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }
               
            }
        }
        /// <summary>
        /// 给对象中某一属性值加到另一对象的值上数值
        /// </summary>
        /// <param name="Ps">对象1</param>
        /// /// <param name="Ps1">对象2</param>
        /// <param name="name">属性数值</param>
        /// 
        private void addhistoyvalue<T>(T ps,T ps1 ,string name)
        {
            Type type = typeof(T);
            double sum = 0;
            foreach (PropertyInfo pi in type.GetProperties())
            {
                
                if (pi.Name == name)
                {

                    try
                    {
                        sum = Convert.ToDouble(pi.GetValue(ps, null));
                        sum += Convert.ToDouble(pi.GetValue(ps1, null));

                        pi.SetValue(ps, sum != null ? sum : 0, null);
                    }
                    catch (Exception err) { MessageBox.Show(err.Message); }
                }

            }
        }
        private void barButtonItemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Econ ec = new Econ();
            ec.UID = "SBJCB";
            ec = Services.BaseService.GetOneByKey<Econ>(ec);
            if (ec==null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                try
                {

                    fpSpread1.Save(ms, false);
                    ec = new Econ();
                    ec.ExcelData = ms.GetBuffer();
                    ec.UID = "SBJCB";
                    Services.BaseService.Create<Econ>(ec);
                }
                catch (Exception ex) { MsgBox.Show(ex.Message); }

            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                try
                {
                   WaitDialogForm wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                    fpSpread1.Save(ms, false);

                    ec.ExcelData = ms.GetBuffer();
                    bts = ec.ExcelData;
                    Services.BaseService.Update<Econ>(ec);
                    wait.Close();
                    MsgBox.Show("保存模板成功");
                }
                catch (Exception ex) { MsgBox.Show(ex.Message); }
            }
            
        }

        private void barButtonSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            //判断文件夹xls是否存在，不存在则创建
            if (!Directory.Exists(System.Windows.Forms.Application.StartupPath + "\\xls"))
            {
                Directory.CreateDirectory(System.Windows.Forms.Application.StartupPath + "\\xls");
            }
            
            if (File.Exists(System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls"))
            {
                if (MessageBox.Show("已经存在此文件，你确定要替换吗？", "删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    WaitDialogForm wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                    //fpSpread1.Save(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls", false);
                    fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                    // 定义要使用的Excel 组件接口
                    // 定义Application 对象,此对象表示整个Excel 程序
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // 定义Workbook对象,此对象代表工作薄
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls";
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
                else
                    return;
            }
            else
            {
                WaitDialogForm wait = null;
                try
                {
                    wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                    //textBox1.Focus();
                    fpSpread1.Update();
                    //fpSpread1.Save(System.Windows.Forms.Application.StartupPath + "\\xls\\SRWGH.xls", true);
                    fpSpread1.SaveExcel(System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls", FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
                    // 定义要使用的Excel 组件接口
                    // 定义Application 对象,此对象表示整个Excel 程序
                    Microsoft.Office.Interop.Excel.Application excelApp = null;
                    // 定义Workbook对象,此对象代表工作薄
                    Microsoft.Office.Interop.Excel.Workbook workBook;
                    // 定义Worksheet 对象,此对象表示Execel 中的一张工作表
                    Microsoft.Office.Interop.Excel.Worksheet ws = null;
                    Microsoft.Office.Interop.Excel.Range range = null;
                    excelApp = new Microsoft.Office.Interop.Excel.Application();
                    string filename = System.Windows.Forms.Application.StartupPath + "\\xls\\SBJCB.xls";
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
                catch (Exception ex)
                {
                    wait.Close();
                    MsgBox.Show("保存失败");
                }
            }
            /*string uid = "Remark='"+ProjectUID+"'";
            EconomyAnalysis obj = (EconomyAnalysis)Services.BaseService.GetObject("SelectEconomyAnalysisByvalue",uid);
            if (obj != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                WaitDialogForm wait = null;
                try
                {
                    wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                    //textBox1.Focus();
                    fpSpread1.Update();
                    fpSpread1.Save(ms, false);

                    obj.Contents = ms.GetBuffer();
                    Services.BaseService.Update("UpdateEconomyAnalysisByContents", obj);
                    // excelstate = false;
                    wait.Close();
                    MsgBox.Show("保存成功");


                }
                catch (Exception ex)
                {
                    wait.Close();
                    MsgBox.Show("保存失败");
                }
            }
            else
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                WaitDialogForm wait = null;
                obj = new EconomyAnalysis();
                obj.Remark= ProjectUID;
                try
                {
                    wait = new WaitDialogForm("", "正在保存数据, 请稍候...");
                    //textBox1.Focus();
                    fpSpread1.Update();
                    fpSpread1.Save(ms, false);
                    obj.CreateDate = DateTime.Now;
                    obj.Contents = ms.GetBuffer();
                    Services.BaseService.Create<EconomyAnalysis>(obj);
                    // excelstate = false;
                    wait.Close();
                    MsgBox.Show("保存成功");


                }
                catch (Exception ex)
                {
                    wait.Close();
                    MsgBox.Show("保存失败");
                }
            }*/

        }

        private void barButtonOut_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (treeList1.FocusedNode == null)
            //    return;


            //string uid = treeList1.FocusedNode["UID"].ToString();
            //EconomyAnalysis obj = Services.BaseService.GetOneByKey<EconomyAnalysis>(uid);




            //System.IO.MemoryStream ms = new System.IO.MemoryStream(obj.Contents);

            //FarPoint.Win.Spread.FpSpread fps=new FarPoint.Win.Spread.FpSpread();
            //fps.Open(ms);

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            string fname = "";
            saveFileDialog1.Filter = "Microsoft Excel (*.xls)|*.xls";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fname = saveFileDialog1.FileName;

                try
                {
                    fpSpread1.SaveExcel(fname);
                    //fps.SaveExcel(fname);
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

        private void barButtonrefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            WaitDialogForm newwait = new WaitDialogForm("", "正在更新数据, 请稍候...");
            fpSpread1.ActiveSheetIndex = 1;
            refreshdata();
            newwait.Close();
            MessageBox.Show("更新数据完成！");
   
        }
        private void refreshdata()
        {
            //如果已经有数据了 读取要进行记载的数据
            bool rsflag = false;
            List<RS> list = new List<RS>();
            if (fpSpread1.Sheets[11].RowCount>0)
            {
                rsflag = true;
                list = getRStable(fpSpread1.Sheets[11]);
               
            }
            //重新读取模板
            Econ ec = new Econ();
            ec.UID = "SBJCB";
            ec = Services.BaseService.GetOneByKey<Econ>(ec);
            if (ec != null)
            {
                System.IO.MemoryStream ms = new System.IO.MemoryStream(ec.ExcelData);
                //by1 = obj.Contents;
                fpSpread1.Open(ms);

            }

              //高压配电网主要运行指标
            gypdyxzb();
             //高压配电网主要运行指标附表

            gypdyxzbfb();
            //变电站最大负载率分布表
            submaxfz();
              //变电站最大负载率分布附表
            submaxfb();
            //高压容载比
            rzbfbb();
            //中压运行指标
            zypdzb();
            //中压配电网主要运行指标附表
            zypdzbfb();
             //中压配变负载率分布表
            zypdfzl();
              //中压配电网配变负载率分布附表
            zypdfb();
             //中压线路最大负载率分布表
            zyxlzdfh();
              //中压配电网线路最大负载率分布附表
            zyxlzdfzfb();
            //供电可靠率（RS-3）分布表
            gdkkl();
            if (rsflag)
            {
                setRStable(fpSpread1.Sheets[11],list);
            }
            //市新农村电气化建设情况表
            sxncdqh();
            //市新农村电气化建设情况附表
            xncdqjsfb();
           
        }
        //控制按钮的显示情况
        private void fpSpread1_TabIndexChanged(object sender, EventArgs e)
        {
           
        }
        int beginyear = 0; int lastyear = 0;  //用为最大典型日的区分
        private void fpSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            switch (e.SheetTabIndex)
            {

               
                default:
                    
                    break;
            }
        }

    }
}