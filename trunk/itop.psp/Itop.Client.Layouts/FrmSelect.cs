using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Layouts;
using System.Collections;
using Itop.Common;
using System.Diagnostics;
using DevExpress.Utils;
using Itop.Domain.RightManager;
using Itop.Client.Base;

namespace Itop.Client.Layouts
{
    public partial class FrmSelect : FormBase
    {
        DataTable dttable = null;

        public FrmSelect()
        {
            InitializeComponent();
        }
        public DataTable getdatatable(DataTable dtb )
        {
            dttable = dtb;
            this.fpSpread1.DataSource = dttable;
            return  null;

        }


    }
}