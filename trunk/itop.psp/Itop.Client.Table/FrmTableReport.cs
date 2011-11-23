using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Table;
using Itop.Server;
using Itop.Server.Interface;
namespace Itop.Client.Table
{
    public partial class FrmTableReport :  Itop.Client.Base.FormBase
    {
        public FrmTableReport()
        {
            InitializeComponent();
            repositoryItemPictureEdit1.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
        }
        private void FrmTableReport_Load(object sender, EventArgs e)
        {
            InitData();
        }
        private void InitData()
        {
            string con = " ProjectID='" + MIS.ProgUID + "'";
            IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
            gridControl1.DataSource = templist;
        }
        //���
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!base.AddRight)
            {
                MessageBox.Show("��û��Ȩ�޽��д��������");
                return;
            }
            FrmTableReportEdit frm = new FrmTableReportEdit();
            Ps_Table_Report newptr = new Ps_Table_Report();
            newptr.ProjectID = MIS.ProgUID;
            newptr.Num1 =Convert.ToInt32( Common.Services.BaseService.GetObject("SelectPs_Table_ReportMaxNum1", " ProjectID='" + MIS.ProgUID + "'") )+ 1;
            frm.ptr = newptr;
            frm.Text = "��ӱ��������Ϣ";
            frm.ShowDialog();
            if (frm.DialogResult==DialogResult.OK)
            {
                try
                {
                    Common.Services.BaseService.Create<Ps_Table_Report>(frm.ptr);
                    InitData();
                }
                catch (Exception)
                {
                    
                    throw;
                }
               
            }
        }
        //�޸�
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            if (!base.EditRight)
            {
                MessageBox.Show("��û��Ȩ�޽��д��������");
                return;
            }
            FrmTableReportEdit frm = new FrmTableReportEdit();
            frm.ptr = (Ps_Table_Report)gridView1.GetRow(gridView1.FocusedRowHandle);
            frm.Text = "�޸ı��������Ϣ";
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                try
                {
                    Common.Services.BaseService.Update<Ps_Table_Report>(frm.ptr);
                    InitData();
                }
                catch (Exception)
                {

                    throw;
                }

            }
        }
        //ɾ��
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle < 0)
            {
                return;
            }

            if (!base.DeleteRight)
            {
                MessageBox.Show("��û��Ȩ�޽��д��������");
                return;
            }
            try
            {
                if (MessageBox.Show("ȷ��Ҫɾ�����ݣ�","ѯ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    Ps_Table_Report newptr = (Ps_Table_Report)gridView1.GetRow(gridView1.FocusedRowHandle);
                    Common.Services.BaseService.Delete<Ps_Table_Report>(newptr);
                    InitData();
                }
               

            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportExcel.ExportToExcelOld(this.gridControl1, "���������Ϣ", "");
            //FileClass.ExportExcel(this.gridControl1);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
        //��������
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            IList<string> filedList = new List<string>();
            IList<string> capList = new List<string>();
            for (int i = 0; i < gridView1.Columns.Count; i++)
            {
                if (gridView1.Columns[i].Visible == true)
                {
                    capList.Add(gridView1.Columns[i].Caption);
                    filedList.Add(gridView1.Columns[i].FieldName);
                }
            }
            OpenFileDialog op = new OpenFileDialog();
            op.Filter = "Excel�ļ�(*.xls)|*.xls";
            string Message = "";
            if (op.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    DataTable table = OperTable.GetExcel(op.FileName, filedList, capList);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        if (table.Rows[i][0].ToString().IndexOf("����ʱ��") >= 0)
                            continue;
                        Ps_Table_Report area = new Ps_Table_Report();
                        area.ProjectID = MIS.ProgUID;
                        foreach (DataColumn col in table.Columns)
                        {
                            if (col.ColumnName == "Num1" || col.ColumnName == "image1" || col.ColumnName == "image2")
                            {
                                //area.GetType().GetProperty(col.ColumnName).SetValue(area, Convert.ToInt32(table.Rows[i][col]), null);
                            }
                            else if (col.ColumnName == "TableID")
                            {
                               
                                string con = " TableID='" + table.Rows[i][col].ToString() +"' and ProjectID='" + MIS.ProgUID + "'";
                                IList<Ps_Table_Report> templist = Common.Services.BaseService.GetList<Ps_Table_Report>("SelectPs_Table_ReportListByConn", con);
                                if (templist.Count > 0)
                                {
                                    Message = "[" + table.Rows[i][col].ToString() + "]";
                                }
                                else
                                {
                                    area.GetType().GetProperty(col.ColumnName).SetValue(area, table.Rows[i][col].ToString(), null);
                                }
                            }
                            else
                            {
                                area.GetType().GetProperty(col.ColumnName).SetValue(area, table.Rows[i][col], null);

                            }
                        }
                        area.Num1 = Convert.ToInt32(Common.Services.BaseService.GetObject("SelectPs_Table_ReportMaxNum1", " ProjectID='" + MIS.ProgUID + "'")) + 1;
                        area.image1 = new byte[0];
                        area.image2 = new byte[0];
                        Common.Services.BaseService.Create<Ps_Table_Report>(area);
                    }
                    InitData();
                    MessageBox.Show("������ɣ�");
                    if (Message.Length != 0)
                    {
                        MessageBox.Show("���ʶ" + Message + " �Ѿ����ڣ�δ�ظ���ӣ�");

                    }
                }
                catch { MessageBox.Show("��ʽ���󣬵���ʧ�ܣ�"); }
                
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

      
    }
}