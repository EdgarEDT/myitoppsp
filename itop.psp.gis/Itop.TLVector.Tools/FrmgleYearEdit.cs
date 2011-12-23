using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Common;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Domain.Table;

namespace ItopVector.Tools {
    public partial class FrmgleYearEdit : DevExpress.XtraEditors.XtraForm {
        public FrmgleYearEdit() {
            InitializeComponent();
           
        }
        private void init()
        {
            for (int i = 0; i < 60;i++ ) {
                comboBoxEdit1.Properties.Items.Add(2011 + i);
            }
        }
        public glebeProperty ParentObj=new glebeProperty();
        private glebeYearValue rowdata = new glebeYearValue();
        public glebeYearValue RowData
        {
            get
            {
                rowdata.FHmdTz = Convert.ToDouble(spinEdit1.Value);
                rowdata.Year = Convert.ToInt32(comboBoxEdit1.Text);
                rowdata.Burthen = Convert.ToDouble(ParentObj.Burthen) * rowdata.FHmdTz;
                rowdata.AvgFHmd = Convert.ToDouble(ParentObj.Burthen / (ParentObj.Area + Convert.ToDecimal(ParentObj.ObligateField10)))*rowdata.FHmdTz;
                return rowdata;
            }
            set
            {
              if (value!=null)
              {
                  rowdata = value;
                  comboBoxEdit1.Text = rowdata.Year.ToString();
                  spinEdit1.Value = Convert.ToDecimal(rowdata.FHmdTz);
              }
            }
        }

    }
}