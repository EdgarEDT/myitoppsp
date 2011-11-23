using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client;
using Itop.Client.Common;

using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmUseProperty : FormBase
    {
        private property _prop = new property();
        DataTable dt;
        private bool IsCreate;

        public frmUseProperty()
        {
            InitializeComponent();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmUseProperty_Load(object sender, EventArgs e)
        {

        }
        public void InitData(UseRelating UseRel)
        {
            _prop.UseUID = UseRel.UseID;
            _prop.TypeUID = UseRel.UsePropertyUID;
            dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectpropertyByTypeAndUseUID", _prop), typeof(property));
           if (dt.Rows.Count > 0)
           {
               gridControl1.DataSource = dt;
               IsCreate = false;
           }
           else
           {
               dt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectpropertyByTypeUID", _prop), typeof(property));
               IsCreate = true;
           }
           gridControl1.DataSource = dt;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            panelControl1.Focus();
            if (IsCreate)
            {
                foreach (DataRow row2 in dt.Rows)
                {
                    property _property=new property();
                   _property = Itop.Common.DataConverter.RowToObject<property>(row2);
                   _property.UID = Guid.NewGuid().ToString();
                   _property.UseUID = _prop.UseUID;
                   Services.BaseService.Create<property>(_property);
                }
            }
            else
            {
                foreach (DataRow row2 in dt.Rows)
                {
                    if (row2.RowState == DataRowState.Modified)
                    {
                        Services.BaseService.Update<property>(Itop.Common.DataConverter.RowToObject<property>(row2));
                    }
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}