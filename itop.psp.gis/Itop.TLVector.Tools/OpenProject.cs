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

namespace ItopVector.Tools
{
    public partial class OpenProject : Itop.Client.Base.FormBase
    {     
        public OpenProject()
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
        public void Initdata()
        {
            PSP_ELCPROJECT pr = new PSP_ELCPROJECT();
            pr.ProjectID = this.ProjectID;
            IList list = Services.BaseService.GetList("SelectPSP_ELCPROJECTList", pr);
            DataTable dataSvg = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_ELCPROJECT));
            gridControl1.DataSource = dataSvg;
        }
        public void Initdata(bool flag)         
        {
            PSP_ELCPROJECT pr = new PSP_ELCPROJECT();
            pr.ProjectID = this.ProjectID;
            if (flag)
            {
                pr.FileType = "短路";
            }
            else
                pr.FileType="潮流";
            IList list = Services.BaseService.GetList("SelectPSP_ELCPROJECTByProjectIDandfiletype", pr);
            DataTable dataSvg = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_ELCPROJECT));
            gridControl1.DataSource = dataSvg;
        }

    }
}