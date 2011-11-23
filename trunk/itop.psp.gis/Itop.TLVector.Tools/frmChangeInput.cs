using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmChangeInput : FormBase
    {
        ArrayList strlist = new ArrayList();
        public IList<glebeType> list2 = new List<glebeType>();
        public IList<glebeType> list3 = new List<glebeType>();
        public frmChangeInput()
        {
            InitializeComponent();
        }

        private void frmChangeInput_Load(object sender, EventArgs e)
        {
           
            RefreshData();
        }
        public void InitData(ArrayList _list)
        {
            strlist = _list;
        }
        public IList<glebeType> getList()
        {
            return list2;
        }
        public bool RefreshData()
        {
            try
            {
                list2.Clear();
                IList<glebeType> list = Services.BaseService.GetStrongList<glebeType>();
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].ObjColor = Color.FromArgb(Convert.ToInt32(list[i].ObligateField1));
                }

                for (int i = 0; i < strlist.Count; i++)
                {
                    foreach (glebeType t in list)
                    {
                        if (t.TypeName == strlist[i].ToString())
                        {
                            list2.Add(t);
                            break;
                        }
                    }
                }
                this.gridControl.DataSource = list2;
            }
            catch (Exception exc)
            {
                //  Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }
     

     

        private void spinEdit1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt4_Click(object sender, EventArgs e)
        {

        }

        private void spinEdit1_EditValueChanged(object sender, EventArgs e)
        {
            decimal d = Convert.ToDecimal(spinEdit1.Text);
            RefreshData();
            if (d == 0) return;
            foreach (glebeType t in list2)
            {
                t.TypeStyle = Convert.ToString(Convert.ToDecimal(t.TypeStyle) * d);
            }
            gridControl.RefreshDataSource();
        }

    }
}