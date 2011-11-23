using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
using DevExpress.XtraTreeList.Nodes;
using Itop.Common;
using DevExpress.XtraGrid.Views.Card;


namespace Itop.Client.Table
{
    public partial class frmMainSupplyPower : FormBase
    {
        public frmMainSupplyPower()
        {
            InitializeComponent();
        }

        public void InitSubstation()
        {
            string sql = " L1=" + comboBoxEdit1.Text+" and  AreaID='"+MIS.ProgUID+"'";
            IList<PSP_Substation_Info> list= Services.BaseService.GetList<PSP_Substation_Info>("SelectPSP_Substation_InfoListByWhere",sql);
            treeList1.DataSource = list;
        }
        private void InitData()
        {
            if (comboBoxEdit1.Text != "")
            {

                //Services.BaseService.
                DataSet ds = new DataSet();
                //Add master detail relation ship for the dataset.
                //ds.Relations.Add("OrderDetails",
                //    ds.Tables[tblMaster].Columns["ProductID"],
                //    ds.Tables[tblDetail].Columns["ProductID"]);

                //DataViewManager dvManager = new DataViewManager(ds);
                //DataView dv = dvManager.CreateDataView(ds.Tables[tblMaster]);

                //gridControl1.DataSource = dv;
            }
        }

        private void frmMainSupplyPower_Load(object sender, EventArgs e)
        {
            InitSubstation();
            //InitData();
            //gridView1.SetMasterRowExpanded(0, true);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            InitSubstation();
        }

        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
          

        }

        private void treeList1_Click(object sender, EventArgs e)
        {
            if (treeList1.Selection.Count == 0) return;
            DataSet ds = new DataSet();
            List<PSPDEV> p1 = new List<PSPDEV>();
            List<PSPDEV> p2 = new List<PSPDEV>();
            string str = "";
            string substr ="";
            decimal d1 = 0;
            decimal d2 = 0;
            for (int i = 0; i < treeList1.Selection.Count; i++)
            {
                TreeListNode info = treeList1.Selection[i];
                string uid = info.GetValue("UID").ToString();
                string tit = info.GetValue("Title").ToString();
                d1 =d1+ Convert.ToDecimal(info.GetValue("L2"));
                string sql = " where Type='01' and SvgUID='" + uid + "' and ProjectID='" + MIS.ProgUID + "'";
                IList<PSPDEV> list = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql);
                int num = 0;
                substr = tit;
                for (int j = 0; j < list.Count; j++)
                {
                    PSPDEV dev = list[j];
                    string sql1 = " where Type='05' and (IName='" + dev.Name + "' or JName='" + dev.Name + "') and ProjectID='" + MIS.ProgUID + "'";
                    IList<PSPDEV> list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", sql1);
                    num = num + list1.Count;
                    
                    for (int m = 0; m < list1.Count;m++ )
                    {
                        PSPDEV _p = list1[m];
                        d2 = d2 +Convert.ToDecimal(_p.Burthen) *Convert.ToDecimal(_p.RateVolt)/10000;
                        if(_p.JName==dev.Name){
                            _p.IName = _p.JName;
                        }
                    }
                    p2.AddRange(list1);
                }
                substr = substr + "变电站，包含母线"+list.Count.ToString()+"条。母线上连接的线路"+num.ToString()+"条。\r\n";
                str = str + substr;
                p1.AddRange(list);
            }
            
            if(p1.Count==0){
                p1.Add(new PSPDEV());
            }
            if (p2.Count == 0)
            {
                //p2.Add(new PSPDEV());
            }
            DataTable t1= DataConverter.ToDataTable(p1);
            //DataTable t2= DataConverter.ToDataTable(p2);

            System.Data.DataTable t2 = new DataTable("tb2");
            DataColumn column;
            
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "Name";
            column.ReadOnly = true;
            t2.Columns.Add(column);

            DataColumn column2;
            column2 = new DataColumn();
            column2.DataType = System.Type.GetType("System.Int32");
            column2.ColumnName = "Number";
            column2.ReadOnly = true;
            t2.Columns.Add(column2);

            DataColumn column3;
            column3 = new DataColumn();
            column3.DataType = System.Type.GetType("System.String");
            column3.ColumnName = "IName";
            column3.ReadOnly = true;
            t2.Columns.Add(column3);

            DataColumn column4;
            column4 = new DataColumn();
            column4.DataType = System.Type.GetType("System.Double");
            column4.ColumnName = "Burthen";
            column4.ReadOnly = true;
            t2.Columns.Add(column4);

            for (int i = 0; i < p2.Count; i++)
            {
                DataRow row = t2.NewRow();
                row["Name"] = p2[i].Name;
                row["Number"] = p2[i].Number;
                row["IName"] = p2[i].IName;
                row["Burthen"] = p2[i].Burthen;
                t2.Rows.Add(row);
            }


            ds.Tables.Add(t1);
            ds.Tables[0].TableName = "tb1";
            ds.Tables.Add(t2);
            ds.Tables[1].TableName = "tb2";
            DataColumn[] ds1 = { ds.Tables[0].Columns["Name"] };
            DataColumn[] ds2 = {ds.Tables[1].Columns["IName"]};
            ds.Relations.Add("OrderDetails",
                ds1,
                ds2);

            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables[0]);

            gridControl1.DataSource = dv;
            cardView1.OptionsBehavior.FieldAutoHeight = true;
            string s1 = "";
            if (d1 > d2)
            {
                s1 = "选中的地区供电能力充足，剩余容量为" + Convert.ToString(d1 - d2) + "MVA。可以建设小于剩余容量的线路或下级变电站。";
            }
            else
            {
                s1 = "选中的地区供电能力不足，缺少容量为" + Convert.ToString(Math.Abs((d1 - d2))) + "MVA。";
            }

            memoEdit1.Text = "共选中变电站" + treeList1.Selection.Count + "座。\r\n" + str +
                             "变电站容量之和为："+d1.ToString()+"MVA，现有线路使用容量之和为："+d2.ToString()+"MVA。\r\n"+s1;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
                    }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

     

    }
}