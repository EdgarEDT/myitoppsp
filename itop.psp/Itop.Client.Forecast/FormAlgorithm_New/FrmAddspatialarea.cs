using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Common;
using Itop.Client.Base;
using System.Collections;
using System.Reflection;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.Domain.Forecast;
using System.Xml;
using Itop.Domain.Table;
using Itop.Client.Common;

namespace Itop.Client.Forecast.FormAlgorithm_New {
    public partial class FrmAddspatialarea : DevExpress.XtraEditors.XtraForm {
        public FrmAddspatialarea() {
            InitializeComponent();
        }
        public void init()
        {
          
            string conn = "ProjectID='" + Itop.Client.MIS.ProgUID + "' order by Sort";
            IList<PS_Table_AreaWH> list = Services.BaseService.GetList<PS_Table_AreaWH>("SelectPS_Table_AreaWHByConn", conn);
            foreach (PS_Table_AreaWH area in list) {
                this.comboBoxEdit1.Properties.Items.Add(area.Title);
            }
        }
        public string Areatitle
        {
            get
            {
                return comboBoxEdit1.Text;
            }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }

    }
}