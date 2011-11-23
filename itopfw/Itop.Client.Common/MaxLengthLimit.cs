using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;

namespace Itop.Client.Common
{
    public class MaxLengthLimit
    {
        private MaxLengthLimit()
        {
        }

        public static void Hook(TextEdit ctrl)
        {
            ctrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Ctrl_KeyPress);
        }

        public static void Hook(RepositoryItemTextEdit ctrl)
        {
            ctrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(Ctrl_KeyPress);
        }

        public static void Hook(RepositoryItemCalcEdit ctrl)
        {
            ctrl.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CalcCtrl_KeyPress);
        }

        static void Ctrl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!(e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete))
            {
                TextEdit ctrl = sender as TextEdit;
                if ((ctrl.Text.Length >= ctrl.Properties.MaxLength) 
                    && (ctrl.SelectionLength == 0 || ctrl.SelectedText == ","))
                {
                    e.Handled = true;
                }
            }
        }

        static void CalcCtrl_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (!(e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete))
            {
                CalcEdit ctrl = sender as CalcEdit;
                if ((ctrl.Text.Length >= ctrl.Properties.MaxLength)
                    && (ctrl.SelectionLength == 0 || ctrl.SelectedText == ","))
                {
                    e.Handled = true;
                }
            }
        }
    }
}
