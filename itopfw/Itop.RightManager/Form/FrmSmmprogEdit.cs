using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.RightManager;
using Itop.Client.Base;

namespace Itop.RightManager.UI {
    public partial class FrmSmmprogEdit : FormBase {
        public FrmSmmprogEdit() {
            InitializeComponent(); 
        }

        private void FrmPersonEdit_Load(object sender, EventArgs e) {
            if (dataClass == null)
                dataClass = new Smmprog();
            else {
               
            }

            tbProgId.DataBindings.Add("Text", dataClass, "progid");
            tbProgName.DataBindings.Add("Text", dataClass, "progname");
            tbRemark.DataBindings.Add("Text", dataClass, "remark");
            tbAssemblyName.DataBindings.Add("Text", dataClass, "AssemblyName");
            tbClassName.DataBindings.Add("Text", dataClass, "ClassName");
            tbMethodName.DataBindings.Add("Text", dataClass, "MethodName");
            tbIndex.DataBindings.Add("Text", dataClass, "Index");
            Tico.DataBindings.Add("Text", dataClass, "ProgIco");
            //tbProgType.DataBindings.Add("Text", dataClass, "ProgType");
            //tbProgModuleType.DataBindings.Add("Text", dataClass, "ProgModuleType");
            switch (dataClass.ProgType)
            { 
                case "m":
                    tbProgType.SelectedIndex = 0;
                    break;
                case "f":
                    tbProgType.SelectedIndex = 1;
                    break;
                default:
                    tbProgType.SelectedIndex = 0;
                    break;
            }

            //switch (dataClass.ProgModuleType)
            //{
            //    case "0":
            //        tbProgModuleType.SelectedIndex = 0;
            //        break;
            //    case "1":
            //        tbProgModuleType.SelectedIndex = 1;
            //        break;
            //    default:
            //        tbProgModuleType.SelectedIndex = 0;
            //        break;
            //}

        }
        Smmprog dataClass;

        public Smmprog Smmprog {
            get { return dataClass; }
            set { dataClass = value; }
        }

        private void button1_Click(object sender, EventArgs e) {
            try {                

            } catch { MessageBox.Show("数据格式有误"); return; }
            DialogResult = DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {

        }

        private void tbProgType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tbProgType.SelectedIndex)
            { 
                case 0:
                    dataClass.ProgType = "m";
                    break;

                case 1:
                    dataClass.ProgType = "f";
                    break;
            }
        }

        private void tbProgModuleType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (tbProgModuleType.SelectedIndex)
            //{
            //    case 0:
            //        dataClass.ProgModuleType = "0";
            //        break;
            //    case 1:
            //        dataClass.ProgModuleType = "1";
            //        break;
            //}
        }

        private void btSelectImage_Click(object sender, EventArgs e) {
            FrmResources dlg = new FrmResources();
            dlg.StartPosition = FormStartPosition.CenterScreen;
            if (dlg.ShowDialog() == DialogResult.OK) {
                //MessageBox.Show(dlg.SelectedImageKey);
                Tico.Text = dlg.SelectedImageKey;
                dataClass.ProgIco = dlg.SelectedImageKey;
            }
        }
    }
}