				
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.Client.Base {
    /// <summary>
    /// MDI子窗体基类
    /// </summary>
    public partial class MDIChildForm : XtraForm {
        protected string m_actionId;
        protected string m_progId;

        public MDIChildForm() {
            InitializeComponent();

            FormClosed += delegate {
                MIS.RemoveOpenedForm(m_actionId);
                MIS.SaveLog("应用程序", "关闭应用程序");
            };
        }
        public virtual bool Execute() {
            MIS.OpenMDIChildForm<MDIChildForm>(m_progId, "");
            
            return true;
        }
    }
}