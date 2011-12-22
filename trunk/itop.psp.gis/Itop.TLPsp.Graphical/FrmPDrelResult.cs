using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using Itop.Client.Stutistics;
using System.Xml;
using ItopVector.Tools;
using Itop.Client.Base;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
namespace Itop.TLPsp.Graphical {
    public partial class FrmPDrelResult : DevExpress.XtraEditors.XtraForm {
        public DataTable datatable = new DataTable();//要进行分析的数据
        public PDrelregion ParentObj = new PDrelregion();//分类数据

        public FrmPDrelResult() {
            InitializeComponent();
        }
        private DataTable dt=new DataTable();
        private void init()
        {
            dt=new DataTable();
            
            dt.Columns.Add("AreaName",typeof(string)).Caption="分类";
            dt.Columns.Add("Year", typeof(int)).Caption = "年份";
            dt.Columns.Add("ZB", typeof(string)).Caption = "分析指标";
            dt.Columns.Add("Result", typeof(string)).Caption = "分析结果";

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            FileClass.ExportExcel(this.gridControl1);
        }

        private void FrmPDrelResult_Load(object sender, EventArgs e) {
            init();
            analyst();
        }
        private void analyst()
        {
            double AIHC = 0;//用户平均停电时间
            double avgwb = 0;        //用户平均受外部停电时间
            double avgxd = 0;                 //用户平均停电限电时间
            double sumtdcs = 0;    //用户停电总次数
            double tdzys = 0; //总用户数
            double yhtdcs = 0; //用户停电持续时间
            double ztdl = 0;   //总停电量
            foreach (DataRow dr in datatable.Rows)
            {
                
               AIHC += ((double)dr["TDtime"]) * ((int)dr["PeopleRegion"]);
               sumtdcs += (Convert.ToDouble(dr["S1"])) * ((int)dr["PeopleRegion"]);
                tdzys += ((int)dr["PeopleRegion"]);
                yhtdcs += ((double)dr["TDtime"]) * ((int)dr["PeopleRegion"]);
                ztdl += ((double)dr["TDtime"]) * ((double)dr["AvgFH"]);

                if (dr["TDtype"].ToString() == "外部影响")
                {
                    avgwb += ((double)dr["TDtime"]) * ((int)dr["PeopleRegion"]);
                } else if (dr["TDtype"].ToString() == "系统电源不足限电") 
                {
                    avgxd += ((double)dr["TDtime"]) * ((int)dr["PeopleRegion"]);
                }
               
            }
            if(ParentObj.PeopleSum!=0)
            {
             AIHC=AIHC/ParentObj.PeopleSum;
            avgwb=avgwb/ParentObj.PeopleSum;
            avgxd=avgxd/ParentObj.PeopleSum;
            }
            
            double RS1 = (1 - AIHC / 8760);
            double RS2 = (1 - (AIHC - avgwb) / 8760) ;
            double RS3 = (1 - (AIHC - avgxd) / 8760) ;
            double AITC = (sumtdcs / tdzys) ;
            double AID = (yhtdcs / tdzys) ;
            double AENS = (ztdl / tdzys) ;
            DataRow dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "RS-1";
            dr1["Result"] = RS1.ToString("0.000000");
            dt.Rows.Add(dr1);

            dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "RS-2";
            dr1["Result"] = RS2.ToString("0.000000");
            dt.Rows.Add(dr1);

            dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "RS-3";
            dr1["Result"] = RS3.ToString("0.000000");
            dt.Rows.Add(dr1);

            dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "AIHC";
            dr1["Result"] = AIHC.ToString("0.000");
            dt.Rows.Add(dr1);

            dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "AITC";
            dr1["Result"] = AITC.ToString("0.000");
            dt.Rows.Add(dr1);

            dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "AID";
            dr1["Result"] = AID.ToString("0.000");
            dt.Rows.Add(dr1);

            dr1 = dt.NewRow();
            dr1["AreaName"] = ParentObj.AreaName;
            dr1["Year"] = ParentObj.Year;
            dr1["ZB"] = "AENS";
            dr1["Result"] = AENS.ToString("0.000");
            dt.Rows.Add(dr1);

            gridControl1.DataSource = dt;
        }
    }
}