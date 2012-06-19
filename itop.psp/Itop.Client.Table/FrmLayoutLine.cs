using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Stutistics;
using DevExpress.XtraGrid.Columns;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using DevExpress.XtraGrid.Views.BandedGrid;
using Itop.Common;
using System.IO;
using Itop.Domain.Stutistic;
using System.Xml;
using Itop.Domain.BaseData;
using Itop.Domain.Layouts;
using Itop.Client.Table;
using Itop.Domain.Table;
using Itop.Domain.Graphics;

namespace Itop.Client.Table
{
    public partial class FrmLayoutLine : Itop.Client.Base.FormBase
    {

        IList<PSPDEV> valuelist = null;
        private IList<PSPDEV> fu_list = null;
        private List<PSPDEV> fu_list_no = new List<PSPDEV>(); 
        private string ProjectID=Itop.Client.MIS.ProgUID;
        private string nowyear=DateTime.Now.Year.ToString();
        public FrmLayoutLine()
        {
            InitializeComponent();
        }
        string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
    
        private void FrmLayoutLine_Load(object sender, EventArgs e)
        {
            AddArea();
            InitRight();
            InitData();
           
        }
        public void Linestatic(string year)
        {
            if (!string.IsNullOrEmpty(year))
            {
                nowyear = year;
            }
            this.ShowDialog();
        }
        private void AddArea()
        {
            string constr = " ProjectID='" + MIS.ProgUID + "'";
            IList<PS_Table_AreaWH> lt = Common.Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", constr);
            repositoryItemLookUpEdit1.DataSource = lt;
        }
        private void InitRight()
        {
            if (!AddRight)
            {
                barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barAdd1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

            if (!EditRight)
            {
                barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barEdit1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                //barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
               
            }

            if (!DeleteRight)
            {
                barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barDel1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem11.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!AddRight && !EditRight)
            {
              

            }
            if (!PrintRight)
            {
                barSubItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            }

        }
        public IList<double> DY()
        {
            string constr ="   ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear+" and Type='05'  order by RateVolt desc";
            return Common.Services.BaseService.GetList<double>("SelectPSPDEV_RateVolt_distinct", constr);
            
        }
        public IList<string> GetAreaID(double tempdb)
        {

            string constr = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + tempdb + " order by AreaID ";
            return Common.Services.BaseService.GetList<string>("SelectPSPDEV_GroupAreaID_DIs", constr);
        }
        public void InitData()
        {
            string constrvalue = " where  ProjectID='@@@@@#@@'";
            valuelist = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", constrvalue);

            int m = 1;
            IList<double> templist = DY();
            if (templist.Count != 0)
            {
                for (int i = 0; i < templist.Count; i++)
                {
                    PSPDEV tempdev=new PSPDEV ();
                    tempdev.ProjectID = que[i];
                    tempdev.Name = templist[i] + "KV线路";
                    tempdev.RateVolt = templist[i];
                    tempdev.Type = "T";
                    m = 1;

                    string lenthstr = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + templist[i];
                    double linelength =(double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", lenthstr);
                    double length2 = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", lenthstr);
                    tempdev.LineLength = linelength;
                    tempdev.Length2 = length2;
                    valuelist.Add(tempdev);
                    for (int j = 0; j < GetAreaID(templist[i]).Count; j++)
                    {
                        PSPDEV tempdev1 = new PSPDEV();
                        tempdev1.ProjectID = Convert.ToChar(j + 65).ToString().ToLower(); ;
                        tempdev1.RateVolt = templist[i];
                        tempdev1.AreaID = GetAreaID(templist[i])[j];
                        string lenthstra = "  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "  and Type='05'  and RateVolt=" + templist[i] + "  and AreaID='" + GetAreaID(templist[i])[j] + "'";
                        double linelengtha = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLineLength", lenthstra);
                        double length2a = (double)Common.Services.BaseService.GetObject("SelectPSPDEV_SUMLength2", lenthstra);
                        tempdev1.LineLength = linelengtha;
                        tempdev1.Length2 = length2a;
                        tempdev1.Type = "A";
                        valuelist.Add(tempdev1);

                        string constr = " where  ProjectID='" + ProjectID + "' and  year(cast(OperationYear as datetime))<" + nowyear + "   and Type='05' and RateVolt=" + templist[i] + " and AreaID='" + GetAreaID(templist[i])[j] + "'  order by Name";
                          IList<PSPDEV> linelist = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", constr);
                          for (int k = 0; k < linelist.Count; k++)
                          {
                              linelist[k].ProjectID = m.ToString();
                              m++;
                              valuelist.Add(linelist[k]);
                          }
                        
                    }
                }
            }
            gridControl1.DataSource = valuelist;
            fu_list = valuelist;

            list_copy(valuelist, fu_list_no);
            att_list(fu_list_no);

        }
        private void list_copy(IList<PSPDEV> list1, IList list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                list2.Add(list1[i]);
            }
        }

        private void att_list(IList templist)
        {
            
            for (int i = 0; i < templist.Count; i++)
            {
                if (char.IsLower(((PSPDEV)templist[i]).ProjectID, 0))
                {
                    templist.RemoveAt(i);
                    i--;
                }
               
            }
        }
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            //if (gridView1.GetRowCellValue(e.RowHandle,"Type")=="T")
            //{
            //    e.r

            //}

               object dr = this.gridView1.GetRow(e.RowHandle);
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c1 = Color.FromArgb(175, 238, 238);
            Color c2 = Color.FromArgb(175, 238, 238);
            Color c3 = Color.FromArgb(245, 222, 180);
            Color c4 = Color.FromArgb(245, 222, 180);
           
            if (dr == null)
                return;
            int tempsum=0;
            if (gridView1.GetRowCellValue(e.RowHandle, "Type") != null)
            {
                if (gridView1.GetRowCellValue(e.RowHandle, "Type")=="T")
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    e.Graphics.FillRectangle(brush, r);
                }
                else if (gridView1.GetRowCellValue(e.RowHandle, "Type")=="A" &&e.Column.Name=="NUM")
                {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                        e.Graphics.FillRectangle(brush, r);
                }
            }

        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        public void AddCol(ref IList<string> col, ref IList<string> area)
        {
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Visible!=false)
                {
                    col.Add(gridView1.Columns[i].Caption);
                }
                
            }
            IList<PSPDEV> list = Common.Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", " where  ProjectID='" + ProjectID + "' and Type='05'");
            area.Add("空");
            foreach (PSPDEV info in list)
            {
                area.Add(info.AreaID);
            }
        }
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridControl1);
            //IList<string> col = new List<string>();//SelectAreaNameGroupByConn
            //IList<string> area = new List<string>();
            //FrmOutput frm = new FrmOutput();
            //AddCol(ref col, ref area);
            //frm.Column = col; frm.Area = area;
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    for (int i = 0; i < gridView1.Columns.Count; i++)
            //    {
            //        for (int j = 0; j < frm.Column.Count; j++)
            //        {
            //            if (frm.Column[j] == gridView1.Columns[i].Caption)
            //                gridView1.Columns[i].Visible = false;
            //        }
            //    }
            //    string conn = " and (";
            //    for (int j = 0; j < frm.VolLev.Count; j++)
            //    {
            //        if (j == frm.VolLev.Count - 1)
            //            conn += "L1=" + frm.VolLev[j] + ")";
            //        else
            //            conn += "L1=" + frm.VolLev[j] + " or ";
            //    }
            //    if (frm.Area.Count > 0)
            //        conn += " and (";
            //    for (int i = 0; i < frm.Area.Count; i++)
            //    {

            //        if (frm.Area[i] == "空")
            //            frm.Area[i] = "";
            //        if (i == frm.Area.Count - 1)
            //            conn += "AreaName='" + frm.Area[i] + "')";
            //        else
            //        {
            //            conn += "AreaName='" + frm.Area[i] + "' or ";
            //        }
            //    }
            //    //this.ctrlSubstation_Info1.RefreshDataOut(conn);
            //    //this.ctrlSubstation_Info1.CalcTotal(conn);
            //    FileClass.ExportExcel(this.gridControl1);
            //    for (int i = 0; i < gridView1.Columns.Count; i++)
            //    {
            //        gridView1.Columns[i].Visible = true;
            //    }
            //    //this.ctrlSubstation_Info1.RefreshData1();
            //    //this.ctrlSubstation_Info1.CalcTotal();
            //}
            
        }

        private void barPrint1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // this.ctrlSubstation_Info1.PrintPreview();
            ComponentPrint.ShowPreview(this.gridControl1, "线路数据表");
        }

        
        void repositoryItemCheckEdit1_CheckedChanged(object sender, System.EventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit chk = (DevExpress.XtraEditors.CheckEdit)sender;
            Is_Fu(chk.Checked);
        }

        public void Is_Fu(bool tempbool)
        {
            if (tempbool)
            {
                gridControl1.DataSource = fu_list;
            }
            else
            {
                gridControl1.DataSource = fu_list_no;
            }
        }

        private void barEdit1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}