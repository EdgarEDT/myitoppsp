using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Configuration;
using Itop.ORAInterFace;
using Itop.Common;


namespace Itop.DLGH
{
    public partial class frmInterFace : FormModuleBase
    {
        //private OracleConnection con;
        private string connstr = "";
        public string Treeid = "";
        public frmInterFace()
        {
            InitializeComponent();
        }
        protected override void Add()
        {
            if (!AddRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            FrmInterFaceDialog f = new FrmInterFaceDialog();
            f.ShowDialog();
        }
        protected override void Edit()
        {
            if (!EditRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (ctrlglebeType1.FocusedObject==null) return;
            FrmInterFaceDialog f = new FrmInterFaceDialog();
            f.IsCreate = false;
            f.Object = ctrlglebeType1.FocusedObject;
            f.Uid = ctrlglebeType1.FocusedObject.UID;
          
            f.ShowDialog();
        }
        protected override void Del()
        {
            if (!DeleteRight)
            {
                MessageBox.Show("您没有此权限。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            ctrlglebeType1.Typeid = Treeid;
            ctrlglebeType1.DeleteObject();
        }
        protected override void Print()
        {
            ctrlglebeType1.PrintPreview();
        }
        public void InitData()
        {
            object [] obj=new object[30];
            for (int i = 0; i < 30; i++)
            {
                obj[i] = 2000 + i;
            }
            this.year.Properties.Items.AddRange(obj);
            //ctrlglebeType1.RefreshData();
        }

        private void frmglebeType_Load(object sender, EventArgs e)
        {
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            LoadTree(treeView1.Nodes[0],"0");
            connstr = ConfigurationSettings.AppSettings.Get("ORAConnStr");
            InitData();
            //PSP_bdz_type p = new PSP_bdz_type();
            //p.col1 = " col1=1 order by Name";
            //IList list1 = Services.BaseService.GetList("SelectPSP_bdz_typeByWhere", p);
            //list1.Add(new PSP_bdz_type());
            //DataTable dt = Itop.Common.DataConverter.ToDataTable(list1, typeof(PSP_bdz_type));
            //lookUpEdit1.Properties.DataSource = dt;

            //PSP_bdz_type p2 = new PSP_bdz_type();
            //p2.col1 = " col1=2 order by Name";
            //IList list2 = Services.BaseService.GetList("SelectPSP_bdz_typeByWhere", p2);
            //list2.Add(new PSP_bdz_type());
            //DataTable dt2 = Itop.Common.DataConverter.ToDataTable(list2, typeof(PSP_bdz_type));
            //lookUpEdit2.Properties.DataSource = dt2;
   

            //PSP_bdz_type p3 = new PSP_bdz_type();
            //p3.col1 = " col1=3 order by Name";
            //IList list3 = Services.BaseService.GetList("SelectPSP_bdz_typeByWhere", p3);
            //list3.Add(new PSP_bdz_type());
            //DataTable dt3 = Itop.Common.DataConverter.ToDataTable(list3, typeof(PSP_bdz_type));
            //lookUpEdit3.Properties.DataSource = dt3;
           
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //simpleButton3.Enabled = false;
            if(treeView1.SelectedNode==null){
                MessageBox.Show("请选择分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string sql=" 1=1 ";
            if (treeView1.SelectedNode.Tag.ToString() != "0")
            {
                sql = sql + " and BdzId like '%" + treeView1.SelectedNode.Tag.ToString() + "%'";
            }
            //if (lookUpEdit2.Text != "")
            //{
            //    sql = sql + " and FQId like '%" + lookUpEdit2.EditValue + "%'";
            //}
            //if (lookUpEdit3.Text != "")
            //{
            //    sql = sql + " and FxtId like '%" + lookUpEdit3.EditValue + "%'";
            //}
            if(year.Text!=""){
                sql = sql + " and UYear=" + year.Text;
            }
            if(month.Text!="全部"){
                sql = sql + " and UMonth=" + month.Text;
            }
            sql = sql + " order by UYear,UMonth,Substation_Name,Switch_Id";
            PSP_interface p2 = new PSP_interface();
            p2.col1 = sql;
            IList<PSP_interface> list2 = Services.BaseService.GetList<PSP_interface>("SelectPSP_interfaceByWhere", p2);
            ctrlglebeType1.GridControl.DataSource = list2;
        }
        //private static IOraService oraService;

        //public static IOraService OraService
        //{
        //    get
        //    {
        //        if (oraService == null)
        //        {
        //            oraService = RemotingHelper.GetRemotingService<IOraService>();
        //        }
        //        if (oraService == null) MsgBox.Show("服务没有注册");
        //        return OraService;
        //    }
        //}
      
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            IOraService OraService = RemotingHelper.GetRemotingService<IOraService>();
            try
            {
                FlashWindow frmLoad = new FlashWindow();
                //f.SetText("计算中请等待。");
                frmLoad.Show();
                frmLoad.RefleshStatus("计算中请等待...");
                //Application.DoEvents();
                frmLoad.SplashData();
                string str_sql = "";
                string selYear = year.Text;
                string selMonth = month.Text;

                string StartDate = "";
                string EndDate = "";
                IList list2 = new List<PSP_interface>();
                if (selMonth != "全部")
                {
                    StartDate = selYear + "-" + selMonth + "-01 00:00:00";
                    EndDate = selYear + "-" + selMonth + "-31 23:59:59";
                    str_sql = "select c.name,b.code,b.name,sum(case a.power_Type when 10 then a.total_value else 0 end),sum(case a.power_Type when 11 then a.total_value else 0 end) from mp_day_e" + selYear + " a,switch b,substation c " +
                     "where b.substation_id=c.id and b.id=a.mp_id and (a.power_Type='10' or a.power_Type='11') and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>='" + StartDate +
                     "' and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<='" + EndDate + "' group by c.name,b.code,b.name";

                    
                    //con = GetConn();
                    DataSet d = OraService.GetDataSet(str_sql, connstr); // GetDataSet(str_sql);
                    
                    for (int i = 0; i < d.Tables[0].Rows.Count; i++)
                    {
                        PSP_interface p = new PSP_interface();
                        p.UMonth = month.Text;
                        p.UYear = Convert.ToInt32(year.Text);
                        if (Convert.IsDBNull(d.Tables[0].Rows[i][3]))
                        {
                            p.Number = 0;
                        }
                        else
                        {
                            p.Number =Convert.ToDouble(Math.Round( Convert.ToDouble(d.Tables[0].Rows[i][3])/10000,2));
                        }
                        if (Convert.IsDBNull(d.Tables[0].Rows[i][4]))
                        {
                            p.col1 = "0";
                        }
                        else
                        {
                            
                            p.col1 =Convert.ToString(Math.Round( Convert.ToDouble(d.Tables[0].Rows[i][4])/10000,2));
                        }
                        p.Substation_Name = d.Tables[0].Rows[i][0].ToString();
                        p.Switch_Id = d.Tables[0].Rows[i][1].ToString();
                        p.Switch_Name = d.Tables[0].Rows[i][2].ToString();
                        p.UID = Guid.NewGuid().ToString();
                        list2.Add(p);
                    }
                    str_sql = "select c.name,b.code,b.name,sum(a.value) from mp_measure" + selYear +selMonth+ " a,switch b,substation c " +
                   "where b.substation_id=c.id and b.id=a.mp_id and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>='" + StartDate +
                   "' and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<='" + EndDate + "' group by c.name,b.code,b.name";
                    DataSet d2 = OraService.GetDataSet(str_sql, connstr);
                    for (int i = 0; i < d2.Tables[0].Rows.Count; i++)
                    {
                        PSP_interface p = new PSP_interface();
                        //p.UMonth = month.Text;
                        //p.UYear = Convert.ToInt32(year.Text);
                        //if (Convert.IsDBNull(d.Tables[0].Rows[i][3]))
                        //{
                        //    p.Number = 0;
                        //}
                        //else
                        //{
                        //    p.Number = Convert.ToDouble(d.Tables[0].Rows[i][3]);
                        //}
                        if (Convert.IsDBNull(d2.Tables[0].Rows[i][3]))
                        {
                            p.LoadValue = 0;
                        }
                        else
                        {
                            p.LoadValue = Convert.ToDouble(Math.Round(Convert.ToDouble(d2.Tables[0].Rows[i][3]),2));
                        }
                        //p.Substation_Name = d.Tables[0].Rows[i][0].ToString();
                        //p.Switch_Id = d.Tables[0].Rows[i][1].ToString();
                        //p.Switch_Name = d.Tables[0].Rows[i][2].ToString();
                        //p.UID = Guid.NewGuid().ToString();
                        ((PSP_interface)list2[i]).LoadValue = p.LoadValue; ;
                    }

                    ctrlglebeType1.GridControl.DataSource = list2;
                }
                else
                {
                    try
                    {
                        for (int n = 1; n < 13; n++)
                        {
                            string mm = n.ToString();
                            if (mm.Length < 2) { mm = "0" + mm; }
                            StartDate = selYear + "-" + mm + "-01 00:00:00";
                            EndDate = selYear + "-" + mm + "-31 23:59:59";
                            str_sql = "select c.name,b.code,b.name,sum(case a.power_Type when 10 then a.total_value else 0 end),sum(case a.power_Type when 11 then a.total_value else 0 end) from mp_day_e" + selYear + " a,switch b,substation c " +
                             "where b.substation_id=c.id and b.id=a.mp_id and (a.power_Type='10' or a.power_Type='11') and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>='" + StartDate +
                             "' and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<='" + EndDate + "' group by c.name,b.code,b.name";
                            //con = GetConn();
                            DataSet d = OraService.GetDataSet(str_sql, connstr);
                            for (int i = 0; i < d.Tables[0].Rows.Count; i++)
                            {
                                PSP_interface p = new PSP_interface();
                                p.UMonth = mm;
                                p.UYear = Convert.ToInt32(year.Text);
                                if (Convert.IsDBNull(d.Tables[0].Rows[i][3]))
                                {
                                    p.Number = 0;
                                }
                                else
                                {
                                    p.Number = Convert.ToDouble(Math.Round(Convert.ToDouble(d.Tables[0].Rows[i][3])/10000,2));
                                }
                        
                                if (Convert.IsDBNull(d.Tables[0].Rows[i][4]))
                                {
                                    p.col1 = "0";
                                }
                                else
                                {
                                    p.col1 = Convert.ToString(Math.Round(Convert.ToDouble(d.Tables[0].Rows[i][4]) / 10000, 2));
                                }
                                p.Substation_Name = d.Tables[0].Rows[i][0].ToString();
                                p.Switch_Id = d.Tables[0].Rows[i][1].ToString();
                                p.Switch_Name = d.Tables[0].Rows[i][2].ToString();
                                p.UID = Guid.NewGuid().ToString();
                                list2.Add(p);
                            }
                            str_sql = "select c.name,b.code,b.name,sum(a.value) from mp_measure" + selYear +mm+ " a,switch b,substation c " +
                           "where b.substation_id=c.id and b.id=a.mp_id  and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')>='" + StartDate +
                           "' and TO_CHAR(a.datetime/(24*3600) + TO_DATE('1970-1-1 08:00:00','YYYY-MM-DD HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS')<='" + EndDate + "' group by c.name,b.code,b.name";
                            //con = GetConn();
                            DataSet d2 = OraService.GetDataSet(str_sql, connstr);
                            for (int i = 0; i < d2.Tables[0].Rows.Count; i++)
                            {
                                PSP_interface p = new PSP_interface();
                                //p.UMonth = mm;
                                //p.UYear = Convert.ToInt32(year.Text);
                                //if (Convert.IsDBNull(d.Tables[0].Rows[i][3]))
                                //{
                                //    p.Number = 0;
                                //}
                                //else
                                //{
                                //    p.Number = Convert.ToDouble(d.Tables[0].Rows[i][3]);
                                //}
                                if (Convert.IsDBNull(d2.Tables[0].Rows[i][3]))
                                {
                                    p.LoadValue = 0;
                                }
                                else
                                {
                                    p.LoadValue = Convert.ToDouble(Math.Round(Convert.ToDouble(d2.Tables[0].Rows[i][3]),2));
                                }
                                //p.Substation_Name = d.Tables[0].Rows[i][0].ToString();
                                //p.Switch_Id = d.Tables[0].Rows[i][1].ToString();
                                //p.Switch_Name = d.Tables[0].Rows[i][2].ToString();
                                //p.UID = Guid.NewGuid().ToString();
                                ((PSP_interface)list2[i]).LoadValue=p.LoadValue;
                            }
                        }
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                       
                        ctrlglebeType1.GridControl.DataSource = list2;
                        
                    }
                }
                if (list2.Count > 0) {  }
                else {  }
                frmLoad.Close();
            }
            catch(Exception e1){
                MessageBox.Show("没有找到相关信息，请重新选择时间。"+e1.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //public DataSet GetDataSet(string cmd)
        //{
        //    this.CheckConnection();

        //    DataSet dataSet = new DataSet();

        //    try
        //    {
        //        OracleCommand dataCommand = new OracleCommand(cmd, con);
        //        OracleDataAdapter dataAdapter = new OracleDataAdapter();
        //        dataAdapter.SelectCommand = dataCommand;
        //        dataAdapter.Fill(dataSet, "recordSet");

        //    }
        //    catch (Exception se)
        //    {
        //        throw new Exception("Error in SQL", se);
        //    }
        //    finally
        //    {
        //        closeConn();
        //    }
        //    return dataSet;
            
        //}
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (ctrlglebeType1.GridView.GetSelectedRows() == null || ctrlglebeType1.GridView.GetSelectedRows().Length < 1)
            {
                MessageBox.Show("请选择记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (Treeid=="")
            {
                MessageBox.Show("请指定一个导入的分类。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (lookUpEdit2.Text == "")
            //{
            //    MessageBox.Show("请指定要导入到的分区。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            //if (lookUpEdit3.Text == "")
            //{
            //    MessageBox.Show("请指定要导入到的分系统。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            IList<PSP_interface> list_temp =ctrlglebeType1.GridControl.DataSource as IList<PSP_interface>;
            int[] l=ctrlglebeType1.GridView.GetSelectedRows();
          
            for (int i = 0; i < l.Length;i++ )
            {
                PSP_interface p = ctrlglebeType1.GridView.GetRow(l[i]) as PSP_interface;
                p.BdzId = treeView1.SelectedNode.Tag.ToString();
                //p.FQId = lookUpEdit2.EditValue.ToString();
                //p.FxtId = lookUpEdit3.EditValue.ToString();
                PSP_interface p2 = new PSP_interface();
                p2.col1 = " UYear='" + p.UYear + "' and UMonth='" + p.UMonth + "' and Substation_Name='" + p.Substation_Name + "' and Switch_Id='"+p.Switch_Id+"' ";
                p2 =(PSP_interface) Services.BaseService.GetObject("SelectPSP_interfaceByWhere", p2);
                if (p2 != null)
                {
                    if(!p2.BdzId.Contains(p.BdzId)){
                        p2.BdzId = p2.BdzId + "," + p.BdzId;
                    }
                    //if (!p2.FQId.Contains(p.FQId))
                    //{
                    //    p2.FQId = p2.FQId + "," + p.FQId;
                    //}
                    //if (!p2.FxtId.Contains(p.FxtId))
                    //{
                    //    p2.FxtId = p2.FxtId + "," + p.FxtId;
                    //}
                    Services.BaseService.Update<PSP_interface>(p2);
                }
                else
                {
                    Services.BaseService.Create<PSP_interface>(p);
                }
            }
            MessageBox.Show("导入成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //public void closeConn()
        //{
        //    try
        //    {
        //        if (con.State == ConnectionState.Open)
        //            con.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception("Failed to Open connection.", e);

        //    }
        //}
     
        //private void CheckConnection()
        //{
        //    try
        //    {
        //        con = new OracleConnection(connstr);

        //       // con.ConnectionString = connstr;//"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=Itopdata)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ItopORA)));uid=system;pwd=itop;";

        //  //  "Driver={Oracle in instantclient10_2};" +

        //    //"Server=itop;" +

        //    //"Uid=system;" +

        //    //"Pwd=itop;";

          
        //        if (con.State != ConnectionState.Open)
        //            con.Open();
        //    }
        //    catch (Exception se)
        //    {
        //        //ErrorLog el = new ErrorLog(se);
        //        MessageBox.Show("Oracle数据库打开失败"+se.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
        //}

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (radioGroup1.SelectedIndex == 0)
            //{
            //    lookUpEdit1.Enabled = false;
            //    lookUpEdit2.Enabled = false;
            //    lookUpEdit3.Enabled = false;
            //}
            //if (radioGroup1.SelectedIndex == 1)
            //{
            //    lookUpEdit1.Enabled = true;
            //    lookUpEdit2.Enabled = true;
            //    lookUpEdit3.Enabled = true;
            //}
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                saveFileDialog1.Filter = "Excel文件 (*.xls)|*.xls";
                saveFileDialog1.ShowDialog();
                string fname = saveFileDialog1.FileName;
                if (fname != "")
                {
                    ctrlglebeType1.GridControl.ExportToExcelOld(fname);
                    MessageBox.Show("导出成功。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception e1){
                MessageBox.Show(e1.Message);
            }
        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void 修改ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        public void LoadTree(TreeNode _root, string id)
        {
            PSP_bdz_type p = new PSP_bdz_type();
            p.col1 = " col3='" + id + "' order by Name";
           IList<PSP_bdz_type> list= Services.BaseService.GetList<PSP_bdz_type>("SelectPSP_bdz_typeByWhere", p);
           for(int i=0;i<list.Count;i++){
               PSP_bdz_type _type = list[i];
               TreeNode node = new TreeNode(_type.Name);
               node.Tag = _type.id;
               LoadTree(node, _type.id);
               _root.Nodes.Add(node);
            }
        }
        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
           Treeid= treeView1.SelectedNode.Tag.ToString();

           //string sql = " 1=1 ";
           //if (treeView1.SelectedNode.Tag.ToString() != "0")
           //{
           //    sql = sql + " and BdzId like '%" + treeView1.SelectedNode.Tag.ToString() + "%'";
           //}
       
           //if (year.Text != "")
           //{
           //    sql = sql + " and UYear=" + year.Text;
           //}
           //if (month.Text != "全部")
           //{
           //    sql = sql + " and UMonth=" + month.Text;
           //}
           //sql = sql + " order by UYear,UMonth,Substation_Name,Switch_Id";
           //PSP_interface p2 = new PSP_interface();
           //p2.col1 = sql;
           //IList<PSP_interface> list2 = Services.BaseService.GetList<PSP_interface>("SelectPSP_interfaceByWhere", p2);
           //ctrlglebeType1.GridControl.DataSource = list2;
        }
    }
}