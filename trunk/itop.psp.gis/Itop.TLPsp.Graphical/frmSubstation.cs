using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Graphics;
using System.Collections;
using Itop.Client.Common;

namespace Itop.TLPsp.Graphical
{
    public partial class frmSubstation : FormBase
    {
        public frmSubstation()
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
        public void InitData()
        {  
            try
            {
                IList list = Services.BaseService.GetList("SelectPSP_Substation_InfoListByParentid", this.ProjectID);
                Itop.Common.DataConverter.ToDataTable(list,typeof(PSP_Substation_Info));
                comboBox1.DataSource = list;
                comboBox1.DisplayMember = "Title";
            }
            catch (System.Exception ex)
            {
            	
            }
        }
        protected string substationName;
        public string SubstationName
        {
            get{
                return comboBox1.Text;
            }
            set{
                comboBox1.Text = value;
            }
        }
    }
}