using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;

namespace Itop.TLPSP.DEVICE
{
    public partial class PSPProject : Itop.Client.Base.FormBase
    {
        public PSPProject()
        {
            InitializeComponent();            
        }
        protected string projectID;
        public string ProjectID
        {
            get{
                return projectID;
            }
            set{
                projectID = value;
            }
        }
        protected string fileName;
        DataTable dataSvg = new DataTable();
        public string FileName
        {
            get
            {
                if (gridView1.FocusedRowHandle > -1)
                {
                    fileSUID = Itop.Common.DataConverter.RowToObject<PSP_ELCPROJECT>(gridView1.GetDataRow(gridView1.FocusedRowHandle)).Name.ToString();
                    return fileSUID;
                }
                else
                {
                    return null;
                }
            }
        }
        protected string fileSUID;
        public string FileSUID
        {
            get{
                if (gridView1.FocusedRowHandle>-1)
                {
                    fileSUID = Itop.Common.DataConverter.RowToObject<PSP_ELCPROJECT>(gridView1.GetDataRow(gridView1.FocusedRowHandle)).ID.ToString();
                    return fileSUID;
                }     
                else
                {
                    return null;
                }
            }
            set{
                fileSUID = value;
            }
        }
        public PSP_ELCPROJECT Parentobj
        {
            get
            {
                if (gridView1.FocusedRowHandle > -1)
                {
                    return Itop.Common.DataConverter.RowToObject<PSP_ELCPROJECT>(gridView1.GetDataRow(gridView1.FocusedRowHandle));
                    
                }
                else
                {
                    return null;
                }
            }
        }
        public void Initdata()
        {
            PSP_ELCPROJECT pr = new PSP_ELCPROJECT();
            pr.ProjectID = this.ProjectID;
            IList list = Services.BaseService.GetList("SelectPSP_ELCPROJECTList", pr);
            dataSvg = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_ELCPROJECT));
            gridControl1.DataSource = dataSvg;
        }
        bool pspflag = false;
        public void Initdata(bool flag)         
        {
            pspflag = flag;
            PSP_ELCPROJECT pr = new PSP_ELCPROJECT();
            pr.ProjectID = this.ProjectID;
            if (flag)
            {
                pr.FileType = "短路";
            }
            else
                pr.FileType="潮流";
            IList list = Services.BaseService.GetList("SelectPSP_ELCPROJECTByProjectIDandfiletype", pr);
            dataSvg= Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_ELCPROJECT));
            gridControl1.DataSource = dataSvg;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            frmNewProject frmprojectDLG = new frmNewProject();
            frmprojectDLG.flag = pspflag;
            frmprojectDLG.init();
            frmprojectDLG.Name = "";
            if (frmprojectDLG.ShowDialog() == DialogResult.OK)
            {
                PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                pd.Name = frmprojectDLG.Name;
                pd.FileType = frmprojectDLG.FileType;
                pd.Class = System.DateTime.Now.ToString();
                pd.BelongYear = frmprojectDLG.BelongYear;
                pd.ProjectID = Itop.Client.MIS.ProgUID;
                UCDeviceBase.DataService.Create<PSP_ELCPROJECT>(pd);
                DataRow row = dataSvg.NewRow();
                Itop.Common.DataConverter.ObjectToRow(pd, row);
                dataSvg.Rows.Add(row);
            }
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          DataRow node =gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (node != null)
            {
                string id = node["ID"].ToString();
                PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                pd.ID = id;
                pd.Name = node["Name"].ToString();
                pd.Class = node["Class"].ToString();
                pd.FileType = node["FileType"].ToString();
                pd.BelongYear = node["BelongYear"].ToString();
                pd.ProjectID = node["ProjectID"].ToString();
                frmNewProject frmprojectDLG = new frmNewProject();
                if (pd.FileType == "潮流")
                {
                    frmprojectDLG.flag = false;
                }
                else
                {
                    frmprojectDLG.flag = true;
                }
                
                frmprojectDLG.Name = pd.Name;
                frmprojectDLG.FileType = pd.FileType;
                frmprojectDLG.BelongYear = pd.BelongYear;
                frmprojectDLG.init();
                if (frmprojectDLG.ShowDialog() == DialogResult.OK)
                {
                    node["Name"] = frmprojectDLG.Name;
                    pd.Name = frmprojectDLG.Name;
                    pd.FileType = frmprojectDLG.FileType;
                    pd.BelongYear = frmprojectDLG.BelongYear;
                    UCDeviceBase.DataService.Update("UpdatePSP_ELCPROJECT", pd);
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID = pd.ID;
                    svgFile = (SVGFILE)Services.BaseService.GetObject("SelectSVGFILEByKey", svgFile);
                    if (svgFile != null)
                    {
                        svgFile.FILENAME = pd.Name;
                        UCDeviceBase.DataService.Update<SVGFILE>(svgFile);

                    }

                  
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataRow node = gridView1.GetDataRow(gridView1.FocusedRowHandle); ;
            if (node != null)
            {
                if (MessageBox.Show("确定删除么?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    string id = node["ID"].ToString();
                    dataSvg.Rows.Remove(node);
                    PSP_ELCPROJECT pd = new PSP_ELCPROJECT();
                    pd.ID = id;
                    UCDeviceBase.DataService.Delete<PSP_ELCPROJECT>(pd);
                    //删除和其相连的设备记录
                    string con = "where ProjectSUID = '" + pd.ID + "'";
                    IList list = Services.BaseService.GetList("SelectPSP_ElcDeviceByCondition", con);
                    foreach (PSP_ElcDevice pe in list)
                    {
                        UCDeviceBase.DataService.Delete<PSP_ElcDevice>(pe);
                    }
                    //删除和其相关的图形记录
                    SVGFILE svgFile = new SVGFILE();
                    svgFile.SUID = pd.ID;
                    svgFile = (SVGFILE)Services.BaseService.GetObject("SelectSVGFILEByKey", svgFile);
                    if (svgFile != null)
                    {
                        UCDeviceBase.DataService.Delete<SVGFILE>(svgFile);
                    }
                    
                   
                }
            }
        }


    }
}