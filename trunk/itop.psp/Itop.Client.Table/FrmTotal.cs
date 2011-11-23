using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmTotal : FormBase
    {
        public FrmTotal()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            string conn="SortId=1";
            IList tList = Common.Services.BaseService.GetList("SelectPs_Table_YdListByConn", conn);
            DataTable dataTable = Itop.Common.DataConverter.ToDataTable(tList, typeof(Ps_Table_Yd));
            dataGridView1.DataSource = dataTable;
        }
    }
}