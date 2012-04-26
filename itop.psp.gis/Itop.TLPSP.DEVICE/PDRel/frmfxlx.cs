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
using Itop.Client.Base;

namespace Itop.TLPSP.DEVICE
{
    public partial class frmfxlx : DevExpress.XtraEditors.XtraForm
    {
        public frmfxlx()
        {
            InitializeComponent();
        }
     
        private DataTable dt1 = new DataTable();

        public DataTable DT1
        {
            get { return dt1; }
        }

        private void init()
        {
            dt1.Columns.Add("A", typeof(string));
            dt1.Columns.Add("B", typeof(bool));
            dt1.Columns.Add("C", typeof(string));
            DataRow row = dt1.NewRow();
            row["A"] = "单端供电、手动切换故障段的配电方式";
            row["B"] = false;
            row["C"] = "方式1";
            dt1.Rows.Add(row);
            row = dt1.NewRow();
            row["A"] = "有手动投入备用电源，并没倒闸操作时间为1h";
            row["B"] = false;
            row["C"] = "方式2";
            dt1.Rows.Add(row);
            row = dt1.NewRow();
            row["A"] = "有备用电源自动投入装置，负荷转移概率为0.5的配点方式";
            row["B"] = false;
            row["C"] = "方式3";
            dt1.Rows.Add(row);
            row = dt1.NewRow();
            row["A"] = "有备用电源自动投入装置，但分支线死接在干线上";
            row["B"] = false;
            row["C"] = "方式4";
            dt1.Rows.Add(row);
            row = dt1.NewRow();
            row["A"] = "有备用电源自动投入装置，但分支线故障率消除概率为0.9";
            row["B"] = false;
            row["C"] = "方式4";
            dt1.Rows.Add(row);
            gridControl2.DataSource = dt1;
        }

        private void checkEdit2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (DataRow dr in dt1.Rows)
                dr["B"] = checkEdit2.Checked;
        }

        private void frmorgRySelect_Load(object sender, EventArgs e)
        {
            dt1.Rows.Clear();
            init();
        }
          
    }
}