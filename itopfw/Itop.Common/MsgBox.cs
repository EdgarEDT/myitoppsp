			
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Itop.Common {
    /// <summary>
    /// 信息提示框
    /// </summary>
    public static class MsgBox {
        static public void Show(string info) {
            MessageBox.Show(info, "提示", MessageBoxButtons.OK , MessageBoxIcon.Information);            
        }

        static public DialogResult ShowYesNo(string info) {
            return ShowYesNo(info, MessageBoxDefaultButton.Button1);
        }

        static public DialogResult ShowYesNo(string info, MessageBoxDefaultButton defaultButton) {
            return MessageBox.Show(info, "提示", MessageBoxButtons.YesNo , MessageBoxIcon.Information, defaultButton);
        }
        static public DialogResult ShowYesNoCancel(string info) {
            return ShowYesNoCancel(info, MessageBoxDefaultButton.Button1);
        }
        static public DialogResult ShowYesNoCancel(string info, MessageBoxDefaultButton defaultButton) {
            return MessageBox.Show(info, "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information, defaultButton);
        }

    }
}
