using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Client.Base;
using Itop.Domain;
using System.IO;
namespace Itop.Client
{
    public partial class FrmSysDataFileAdd : FormBase
    {
        public SysDataFiles file; 
        public FrmSysDataFileAdd()
        {
            InitializeComponent();
        }


        private void FrmSysDataFileAdd_Load(object sender, EventArgs e)
        {
            txtFileName.Text = file.FileName;
            txtFileDesc.Text = file.FileDesc;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            file.FileName = txtFileName.Text.Trim();
            file.FileDesc = txtFileDesc.Text.Trim();
            this.DialogResult = DialogResult.OK;
        }
        private string filePath = string.Empty;
        private FileStream fsBLOBFile = null;
        private void btnopenfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog frm = new OpenFileDialog();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                filePath = frm.FileName;

                file.FileName = Path.GetFileName(filePath);
                txtFileName.Text = file.FileName;
                fsBLOBFile = new FileStream(filePath, FileMode.Open, FileAccess.Read, System.IO.FileShare.ReadWrite);
                Byte[] bytBLOBData = new Byte[fsBLOBFile.Length];
                int flength = bytBLOBData.Length;
                fsBLOBFile.Read(bytBLOBData, 0, flength);
                fsBLOBFile.Close();
                file.Files = bytBLOBData;
                file.FileSize = bytBLOBData.Length;
                file.CreateDate = DateTime.Now;
                fsBLOBFile.Close();
            }
        }

        private void btnCanser_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}