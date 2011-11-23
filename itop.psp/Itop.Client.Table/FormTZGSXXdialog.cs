using System;
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
using Itop.Domain.Graphics;

namespace Itop.Client.Table
{
    public partial class FormTZGSXXdialog : FormBase
    {
        public FormTZGSXXdialog()
        {
            InitializeComponent();
            dt.Columns.Clear();
            dt.Columns.Add("A", typeof(string));
            dt.Columns.Add("B", typeof(double));
           
        }
        public string type;
        public double volt; //电压等级
        Ps_Table_TZMX _obj=new Ps_Table_TZMX();
        DataTable dt = new DataTable();
        public int buildyear;
        public int buildend;
        public Ps_Table_TZMX tzmx
        {
            get
            {
                _obj.Title = textEdit1.Text;
                _obj.Vol = (double)spinEdit1.Value;
                _obj.Num = (int)spinEdit2.Value;
                _obj.Linetype = comboBoxEdit8.Text;
                dt = gridControl1.DataSource as DataTable;
                for (int i = buildyear; i <=buildend; i++)
                {
                     string y="y"+i.ToString();
                     string year = "静态投资" + i.ToString() + "年";
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["A"].ToString().Contains(year))
                        {
                            _obj.GetType().GetProperty(y).SetValue(_obj, dr["B"], null);
                        }
                    }
                }
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["A"] == "建设期贷款利息")
                    {
                        _obj.GetType().GetProperty("LendRate").SetValue(_obj, dr["B"], null);
                    }
                    else if (dr["A"] == "价格预备费用")
                    {
                        _obj.GetType().GetProperty("PrepChange").SetValue(_obj, dr["B"], null);
                    }
                    else if (dr["A"] == "动态投资")
                    {
                        _obj.GetType().GetProperty("DynInvest").SetValue(_obj, dr["B"], null);
                    }
                }
                return _obj;
            }
            set
            {

                _obj = value;
                textEdit1.Text = _obj.Title;
                spinEdit1.Value = (decimal)_obj.Vol;
                spinEdit2.Value = (decimal)_obj.Num;
                comboBoxEdit8.Text = _obj.Linetype;
                for (int i = buildyear; i <= buildend;i++ )
                {
                    DataRow dr = dt.NewRow();
                    string y="y"+i.ToString();
                    dr["A"] = "静态投资" + i.ToString()+"年";
                    dr["B"] = _obj.GetType().GetProperty(y).GetValue(_obj, null);
                    dt.Rows.Add(dr);
                }
                DataRow dw = dt.NewRow();
                dw["A"] = "建设期贷款利息";
                dw["B"] = _obj.GetType().GetProperty("LendRate").GetValue(_obj, null);
                dt.Rows.Add(dw);
                dw = dt.NewRow();
                dw["A"] = "价格预备费用";
                dw["B"] = _obj.GetType().GetProperty("PrepChange").GetValue(_obj, null);
                dt.Rows.Add(dw);
                dw = dt.NewRow();
                dw["A"] = "动态投资";
                dw["B"] = _obj.GetType().GetProperty("DynInvest").GetValue(_obj, null);
                dt.Rows.Add(dw);

            }
        }
        private void initcom()
        {
            if (type=="line")
            {
                label3.Text = "线路名称";
                label2.Text = "线路长度";
                label15.Visible = true;
                comboBoxEdit8.Visible = true;
                label1.Visible = false;
                spinEdit2.Visible = false;
                WireCategory wirewire = new WireCategory();
                IList list1 = null;
                if (volt != 0)
                {
                    wirewire.WireLevel = ((int)volt).ToString();
                    list1 = Services.BaseService.GetList("SelectWireCategoryListBYWireLevel", wirewire);
                }
                else
                    list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
                foreach (WireCategory sub in list1)
                {
                  
                        comboBoxEdit8.Properties.Items.Add(sub.WireType);
                  
                }

            }
            else if (type=="sub")
            {
                label3.Text = "变电站名称";
                label2.Text = "容量";
                label15.Visible = false;
                comboBoxEdit8.Visible = false;
                label1.Visible = true;
                spinEdit2.Visible = true;
            }
            this.gridControl1.DataSource = dt;
        }

        private void FormTZGSXXdialog_Load(object sender, EventArgs e)
        {
            initcom();
        }

    }
}