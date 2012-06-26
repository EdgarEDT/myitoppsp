﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.HistoryValue;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Client.Chen;
using Itop.Domain.Table;
using Itop.Client.Table;
using Itop.Client.Forecast;
using Itop.Client.Stutistics;
using Itop.Domain.Stutistics;
using Itop.Client.Common;

namespace Itop.Client.Table
{
    public partial class FrmTzgsBB2pw : Itop.Client.Base.FormBase
    {
        DataTable dataTable;

   
        private DataCommon dc = new DataCommon();
   
        private OperTable oper = new OperTable();
   
     
        DevExpress.XtraGrid.GridControl _gcontrol = null;

        public DevExpress.XtraGrid.GridControl Gcontrol
        {
            get { return _gcontrol; }
            set { _gcontrol = value; }
        }

        public FrmTzgsBB2pw()
        {
            InitializeComponent();
            barSubItem2.Glyph = Itop.ICON.Resource.新建;
        }

        private void HideToolBarButton()
        {
            if (!base.AddRight)
            {
                barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem4.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.EditRight)
            {
                barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!base.DeleteRight)
            {
                barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }

        public string GetProjectID
        {
            get { return ProjectUID; }
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            HideToolBarButton();

            LoadData();
            this.Cursor = Cursors.Default;
        }



        public void LoadData()
        {
            try
            {
                string sql = " and a.ProjectID='" + GetProjectID + "'";
                IList<Ps_Table_TZGS> list = Services.BaseService.GetList<Ps_Table_TZGS>("SelectPs_Table_TZGSByListWherepw", sql);
                for (int i = 0; i < list.Count;i++ )
                {
                    Ps_Table_TZGS p = list[i];
                    p.Sort = (i + 1);
                    p.FromID = p.LineInfo.Split("@".ToCharArray())[0];
                    p.ProjectID = "蚌埠";
                    if (p.DQ == "市辖供电区")
                    {
                        p.ParentID = "蚌埠市区";
                    }
                    else
                    {
                        p.ParentID = "蚌埠县区";
                    }

                }
                this.gridControl.DataSource =list;
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.Message);
            }
        }
        public Ps_Table_TZGS FocusedObject
        {
            get { return this.advBandedGridView1.GetRow(this.advBandedGridView1.FocusedRowHandle) as Ps_Table_TZGS; }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmAddTzgsWH2pw frm = new FrmAddTzgsWH2pw();
            frm.Text = "增加";
           // frm.Stat = focusedNode.GetValue("Col2").ToString();
            // frm.SetLabelName = "子分类名称";
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_Table_TZGS table1 = new Ps_Table_TZGS();
                table1.ID += "|" + GetProjectID;
                frm.tzgspwid = table1.ID;
                
                table1.Title = frm.ParentName;
                table1.ParentID = "0";// focusedNode.GetValue("ID").ToString();
                table1.ProjectID = GetProjectID;
                table1.BuildYear = frm.StartYear;
                table1.BuildEd = frm.FinishYear;
                table1.FromID = "0";
                //table1.AllVolumn = frm.AllVol;
                //table1.AftVolumn = frm.AllVol;
                table1.LineInfo = frm.LineInfo;
                table1.BianInfo = frm.BianInfo;
                table1.AreaName = frm.AreaName;
                table1.Length = frm.LineLen;
                table1.Length2 = frm.LineLen2;
                table1.Sort = OperTable.GetTZGSMaxSort() + 1;
                table1.Col3 = frm.Col3;
                table1.Col1 = frm.BieZhu;
                table1.DQ = frm.DQ;
                table1.JGNum = frm.JGNum;
                table1.WGNum = frm.WGNum;
                table1.ProgType = frm.ProgType;
                table1.Amount = frm.Amount;
                table1.Num6 = frm.Num6;
                table1.Col4 = "pw";
                frm.StrType = "pw";
                table1.Amount = frm.Amount;
                try
                {
                    string pid = table1.ID;
                    string tit = table1.Title;

                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);
                    frm.StrType = "pw-pb";
                    table1.Title = tit + "-配变";
                    table1.Col4 = "pw-pb";
                    //table1.Volumn = frm.Vol;
                    table1.ParentID = pid;
                    table1.ID = Guid.NewGuid().ToString();
                    table1.Num1 = frm.Num1;
                    table1.Num2 = frm.Num2;
                    table1.Num3 = frm.Num3;
                    table1.Num4 = frm.Num4;
                    table1.Num5 = frm.Num5;
                    table1.Num6 = frm.Num6;
                    table1.Amount = frm.Amount;
                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);
                    frm.StrType = "pw-kg";
                    table1.Title = tit + "-开关";
                    table1.Col4 = "pw-kg";
                    table1.ParentID = pid;
                    table1.ID = Guid.NewGuid().ToString();
                    table1.Num1 = frm.Num1;
                    table1.Num2 = frm.Num2;
                    table1.Num3 = frm.Num3;
                    table1.Num4 = frm.Num4;
                    table1.Amount = frm.Amount;
                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);
                    frm.StrType = "pw-line";
                    table1.Title = tit + "-线路";
                    table1.Col4 = "pw-line";
                    table1.Length = frm.LineLen;
                    table1.ParentID = pid;
                    table1.ID = Guid.NewGuid().ToString();
                    table1.LineInfo = frm.LineInfo;
                    table1.Num1 = frm.Num1;
                    table1.Num2 = frm.Num2;
                    table1.Num3 = frm.Num3;
                    table1.Num4 = frm.Num4;
                    table1.Num5 = frm.Num5;
                    table1.Num6 = frm.Num6;
                    table1.Length = frm.LineLen;
                    table1.Length2 = frm.LineLen2;
                    table1.Amount = frm.Amount;
                    Common.Services.BaseService.Create("InsertPs_Table_TZGS", table1);
                 
                    LoadData();
                  
                }
                catch (Exception ex)
                {
                    MsgBox.Show("增加工程出错：" + ex.Message);
                }
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (advBandedGridView1.RowCount==0)
            {
                return;
            }
            FrmAddTzgsWH2pw frm = new FrmAddTzgsWH2pw();
            Ps_Table_TZGS table = new Ps_Table_TZGS();
            table = Common.Services.BaseService.GetOneByKey<Ps_Table_TZGS>(FocusedObject.ID);
            frm.tzgspwid = table.ID;
            frm.ParentName = table.Title; //treeList1.FocusedNode.GetValue("Title").ToString();
            frm.Text = "修改工程";
            //frm.Stat = treeList1.FocusedNode.ParentNode.GetValue("Col2").ToString();

            frm.AreaName = table.AreaName;
            frm.BianInfo = table.BianInfo;
            frm.LineInfo = table.LineInfo;
            frm.LineLen = table.Length;
            //frm.Vol = table.Volumn;
            frm.StartYear = table.BuildYear;
            frm.FinishYear = table.BuildEd;
            frm.LineLen = table.Length;
            frm.LineLen2 = table.Length2;
            frm.BieZhu = table.Col1;
            frm.Col3 = table.Col3;
            frm.StrType = table.Col4;
            frm.JGNum = table.JGNum;
            frm.Amount = table.Amount;
            frm.ProgType = table.ProgType;
            frm.WGNum = table.WGNum;
            frm.DQ = table.DQ;
            frm.Num6=table.Num6;
            frm.StrType = "pw";
            frm.Amount = table.Amount;

            frm.StrType = "pw-pb";
            Ps_Table_TZGS t1 = new Ps_Table_TZGS();
            string sql1 = " ParentID='" + table.ID + "' and Col4='pw-pb'";
            t1 = (Ps_Table_TZGS)Common.Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", sql1);
            frm.Num1 = t1.Num1;
            frm.Num2 = t1.Num2;
            frm.Num3 = t1.Num3;
            frm.Num4 = t1.Num4;
            frm.Num5 = t1.Num5;
            frm.Num6 = t1.Num6;
            frm.Amount = t1.Amount;

            frm.StrType = "pw-kg";
            Ps_Table_TZGS t2 = new Ps_Table_TZGS();
            string sql2 = " ParentID='" + table.ID + "' and Col4='pw-kg'";
            t2 = (Ps_Table_TZGS)Common.Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", sql2);
            frm.Num1 = t2.Num1;
            frm.Num2 = t2.Num2;
            frm.Num3 = t2.Num3;
            frm.Num4 = t2.Num4;
            frm.Amount = t2.Amount;
            frm.StrType = "pw-line";
            Ps_Table_TZGS t3 = new Ps_Table_TZGS();
            string sql3 = " ParentID='" + table.ID + "' and Col4='pw-line'";
            t3 = (Ps_Table_TZGS)Common.Services.BaseService.GetObject("SelectPs_Table_TZGSByConn", sql3);
            frm.Num1 = t3.Num1;
            frm.Num2 = t3.Num2;
            frm.Num3 = t3.Num3;
            frm.Num4 = t3.Num4;
            frm.Num5 = t3.Num5;
            frm.Num6 = t3.Num6;
            frm.LineLen = t3.Length;
            frm.LineLen2 = t3.Length2;
            frm.Amount = t3.Amount;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //AddChildVol(table, false);
                table.Title = frm.ParentName;
                table.BuildYear = frm.StartYear;
                table.BuildEd = frm.FinishYear;
                table.Length=frm.LineLen ;
                table.Length2 = frm.LineLen2;
              
                //table.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).SetValue(table, temp + double.Parse(table.GetType().GetProperty("y" + Convert.ToString(yAnge.StartYear + 1)).GetValue(table, null).ToString()), null);
                table.Col1 = frm.BieZhu;
                table.Col3 = frm.Col3;
                //table.Col4 = frm.StrType;
                table.BianInfo = frm.BianInfo;
                table.LineInfo = frm.LineInfo;
                table.JGNum = frm.JGNum;
                table.Amount = frm.Amount;
                table.ProgType = frm.ProgType;
                table.WGNum = frm.WGNum;
                table.DQ = frm.DQ;
                frm.StrType = "pw";
                table.Amount = frm.Amount;
                table.AreaName = frm.AreaName;
                frm.StrType = "pw-pb";
                t1.Num1 = frm.Num1;
                t1.Num2 = frm.Num2;
                t1.Num3 = frm.Num3;
                t1.Num4 = frm.Num4;
                t1.Num5 = frm.Num5;
                t1.Num6 = frm.Num6;
                t1.Amount = frm.Amount;
                t1.AreaName = frm.AreaName;
                t1.Col3 = frm.Col3;
                t1.DQ = frm.DQ;
                t1.ProgType = frm.ProgType;
                t1.BuildYear = frm.StartYear;
                t1.BuildEd = frm.FinishYear;

                frm.StrType = "pw-kg";
                t2.Num1 = frm.Num1;
                t2.Num2 = frm.Num2;
                t2.Num3 = frm.Num3;
                t2.Num4 = frm.Num4;
                t2.Amount = frm.Amount;
                t2.AreaName = frm.AreaName;
                t2.Col3 = frm.Col3;
                t2.DQ = frm.DQ;
                t2.ProgType = frm.ProgType;
                t2.BuildYear = frm.StartYear;
                t2.BuildEd = frm.FinishYear;

                frm.StrType = "pw-line";
                t3.Num1 = frm.Num1;
                t3.Num2 = frm.Num2;
                t3.Num3 = frm.Num3;
                t3.Num4 = frm.Num4;
                t3.Num5 = frm.Num5;
                t3.Num6 = frm.Num6;
                t3.Length = frm.LineLen;
                t3.Length2 = frm.LineLen2;
                t3.Amount = frm.Amount;
                t3.AreaName = frm.AreaName;
                t3.Col3 = frm.Col3;
                t3.DQ = frm.DQ;
                t3.ProgType = frm.ProgType;
                t3.BuildYear = frm.StartYear;
                t3.BuildEd = frm.FinishYear;
                try
                {
                    Common.Services.BaseService.Update<Ps_Table_TZGS>(table);
                    Common.Services.BaseService.Update<Ps_Table_TZGS>(t1);
                    Common.Services.BaseService.Update<Ps_Table_TZGS>(t2);
                    Common.Services.BaseService.Update<Ps_Table_TZGS>(t3);
                    LoadData();
                   
                }
                catch { }
            }
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmProject_SumWH fsum = new FrmProject_SumWH();
            fsum.Type = "2";
            fsum.Text = "变电站造价信息";
            fsum.ShowDialog();
        }

        private void barButtonItem17_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmProject_SumWH fsum = new FrmProject_SumWH();
            fsum.Type = "1";
            fsum.Text = "线路造价信息";
            fsum.ShowDialog();
        }

        private void barButtonItem18_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmProject_SumWH fsum = new FrmProject_SumWH();
            fsum.Type = "3";
            fsum.Text = "其他造价信息";
            fsum.ShowDialog();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if( FocusedObject==null) return;
            if (!base.DeleteRight)
            {
                MsgBox.Show("您没有权限进行此项操作！");
                return;
            }
            if (MsgBox.ShowYesNo("是否删除选中的工程？") == DialogResult.Yes)
            {
                Ps_Table_TZGS table1 = new Ps_Table_TZGS();
                table1.ID = FocusedObject.ID;
                Services.BaseService.Update("DeletePs_Table_TZGS", table1);
            }
            LoadData();
        }

    }
}