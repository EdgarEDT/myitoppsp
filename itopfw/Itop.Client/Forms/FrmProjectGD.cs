using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Client.Projects;
using System.Collections;
using System.Reflection;
using System.Globalization;
using DevExpress.Utils;
using Itop.Client.Base;
namespace Itop.Client.Forms
{
    public partial class FrmProjectGD : FormBase
    {
        public FrmProjectGD()
        {
            InitializeComponent();
        }

        private void FrmProjectGD_Load(object sender, EventArgs e)
        {
            InitData();
        }

        private void InitData()
        {
            string s = "  IsGuiDang='��' order by CreateDate";
            IList<Project> list = Services.BaseService.GetList<Project>("SelectProjectByWhere", s);
            DataTable dt = Itop.Common.DataConverter.ToDataTable((IList)list, typeof(Project));
            this.gridControl1.DataSource = dt;
        
        
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            I1();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            I2();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            I1();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            I2();
        }

        private void I1()
        {
            DataRow row = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (row == null)
            {
                MessageBox.Show("��ѡ����Ŀ", "��ʾ");
                return;
            }
            string uid = row["UID"].ToString();
            Services.BaseService.Update("UpdateProjectByGuiDangNameNo", uid);
            InitData();
            this.DialogResult = DialogResult.OK;
        }
        private void I2()
        {
            this.Close();
        }

        private void simpleButton3_Click(object sender, EventArgs e) {
            DataRow row = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            if (row == null) {
                MessageBox.Show("��ѡ����Ŀ", "��ʾ");
                return;
            }
            if (Itop.Common.MsgBox.ShowYesNo("�˲���������ɾ�����ݣ��Ƿ������") == DialogResult.Yes) {
                string uid = row["UID"].ToString();
                WaitDialogForm newwaite = new WaitDialogForm("", "����ɾ�������ݣ����Ժ�...");
                DeleteData(uid);
                Services.BaseService.DeleteByKey<Project>(uid);
                newwaite.Close();
                InitData();
                MessageBox.Show("ɾ����ɣ�");
            }
        }
        private void DeleteData(string uid)
        {

            Assembly asm = Assembly.LoadFrom(Application.StartupPath + "\\Itop.Client.DataCopy.dll");
            Type type = asm.GetType("Itop.Client.DataCopy.FormModuleList", true);

            Type[] ptypes = new Type[2];
            ptypes[0] = typeof(string);
            object classInstance = null;
            object result = null;

            object[] paramValues = new object[1];
            paramValues.SetValue(uid, 0);
           
            //MethodInfo method = type.GetMethod("DeleteData"); 
            //result = method.Invoke(classInstance, paramValues);



            Object obj = type.InvokeMember(

        null, 

        BindingFlags.DeclaredOnly | 

        BindingFlags.Public | BindingFlags.NonPublic | 

        BindingFlags.Instance | BindingFlags.CreateInstance, 

        null, 

        null,

        null );



    // Call member function by name

    type.InvokeMember("DeleteData", 

        BindingFlags.DeclaredOnly | 

        BindingFlags.Public | BindingFlags.NonPublic | 

        BindingFlags.Instance | BindingFlags.InvokeMethod, 

        null, 

        obj,

      paramValues);
           
        }
       
    }
}