using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmRZ : FormBase
    {
        public FrmRZ()
        {
            InitializeComponent();
        }
        private IList bindList=new ArrayList();
        private bool bRst = true;
        private string rz = "1.9";
        public bool BRst
        {
            get { return bRst; }
            set { bRst = value; }
        }
        public string RZ
        {
            get { return rz; }
            set { rz = value; }
        }
        public IList BindList
        {
            get { return bindList; }
            set { bindList = value; }
        }

        private void FrmRZ_Load(object sender, EventArgs e)
        {
            InitData();
        }

        public void InitData()
        {
            if (bindList.Count > 0)
            {
                if (bRst)
                {
                    object obj = Activator.CreateInstance(bindList[0].GetType());
                    obj.GetType().GetProperty("ID").SetValue(obj, Guid.NewGuid().ToString(), null);
                    obj.GetType().GetProperty("Title").SetValue(obj, "合计", null);
                    obj.GetType().GetProperty("BuildYear").SetValue(obj, rz, null);
                    obj.GetType().GetProperty("Sort").SetValue(obj, 100000, null);
                    bindList.Add(obj);
                }
                gridControl1.DataSource = bindList;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < BindList.Count; i++)
            {
                BindList[i].GetType().GetProperty("BuildYear").SetValue(BindList[i], spinEdit1.Text, null);
            }
            gridControl1.DataSource = bindList;
            gridView1.RefreshData();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            Type t=null;
            if (bindList.Count > 0)
                t = bindList[0].GetType();
            if(bRst)
                rz = bindList[bindList.Count - 1].GetType().GetProperty("BuildYear").GetValue(bindList[bindList.Count - 1], null).ToString();
            for (int i = 0; i < (bRst?bindList.Count - 1:bindList.Count); i++)
            {
                Common.Services.BaseService.Update("Update"+t.Name,bindList[i]);
            }
            DialogResult = DialogResult.OK;
        }


    }
}