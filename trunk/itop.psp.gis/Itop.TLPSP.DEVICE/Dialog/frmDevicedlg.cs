using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.TLPSP.DEVICE
{
    public partial class frmDevicedlg : Itop.Client.Base.FormBase
    {
        public frmDevicedlg() {
            InitializeComponent();
        }
        string projectid;
        public string ProjectID {
            get { return projectid; }
            set { projectid = value; }
        }
        bool isread = true;
        /// <summary>
        /// �Ƿ�ֻ����Ϊtrueʱͼ�β��ܱ���
        /// </summary>
        public bool IsRead {
            get { return isread; }
            set { isread = value; }
        }
        public virtual object DeviceMx { get { return null; } set { } }
    }
}