using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Itop.Common {
    /// <summary>
    /// 半角控制器
    /// </summary>
    public static class ImeController {
        //声明API函数
        [DllImport("imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetOpenStatus(IntPtr himc);
        [DllImport("imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr himc, bool b);
        [DllImport("imm32.dll")]
        public static extern bool ImmGetConversionStatus(IntPtr himc, ref int lpdw, ref int lpdw2);
        [DllImport("imm32.dll")]
        public static extern int ImmSimulateHotKey(IntPtr hwnd, int lngHotkey);
        [DllImport("imm32.dll")]
        public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        public const int IME_CMODE_FULLSHAPE = 0x8;
        public const int IME_CHOTKEY_SHAPE_TOGGLE = 0x11;
        //重载SetIme，传入Form
        public static void SetIme(Form frm) {
            frm.Paint += new PaintEventHandler(frm_Paint);
            ChangeAllControl(frm);
        }
        //重载SetIme，传入Control
        public static void SetIme(Control ctl) {
            ChangeAllControl(ctl);
        }
        //重载SetIme，传入对象句柄
        public static void SetIme(IntPtr Handel) {
            ChangeControlIme(Handel);
        }
        private static void ChangeAllControl(Control ctl) {
            //在控件的的Enter事件中触发来调整输入法状态
            ctl.Enter += new EventHandler(ctl_Enter);
            //遍历子控件，使每个控件都用上Enter的委托处理
            foreach (Control ctlChild in ctl.Controls)
                ChangeAllControl(ctlChild);
        }

        static void frm_Paint(object sender, PaintEventArgs e) {
            
            ChangeControlIme(sender);
        }
        //控件的Enter处理程序
        static void ctl_Enter(object sender, EventArgs e) {
            ChangeControlIme(sender);
        }
        private static void ChangeControlIme(object sender) {
            Control ctl = (Control)sender;
            ChangeControlIme(ctl.Handle);
        }
        //下面这个函数才是真正检查输入法的全角半角状态
        private static void ChangeControlIme(IntPtr h) {
            IntPtr HIme = ImmGetContext(h);
            if (ImmGetOpenStatus(HIme))  //如果输入法处于打开状态
            {
                int iMode = 0;
                int iSentence = 0;
                bool bSuccess = ImmGetConversionStatus(HIme, ref iMode, ref iSentence);  //检索输入法信息
                if (bSuccess) {
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0)   //如果是全角
                        ImmSimulateHotKey(h, IME_CHOTKEY_SHAPE_TOGGLE);  //转换成半角
                }
            }
            ImmReleaseContext(h, HIme);
           
        }
        //不使用
        private static void SetHalfShape(Control c) {
            IntPtr hIme = ImmGetContext(c.Handle);
            if (ImmGetOpenStatus(hIme)) //如果输入法处于打开状态
            {
                int iMode = 0, iSentence = 0;
                bool bSuccess = ImmGetConversionStatus(hIme, ref iMode, ref iSentence); //检索输入法信息
                if (bSuccess) {
                    if ((iMode & IME_CMODE_FULLSHAPE) > 0) {//如果是全角
                        ImmSimulateHotKey(c.Handle, IME_CHOTKEY_SHAPE_TOGGLE); //转换成半角
                        //iMode &= (~IME_CMODE_FULLSHAPE);
                        //bSuccess = ImmSetConversionStatus(hIme, iMode, iSentence); //检索输入法信息
                    }
                }
            }
            ImmReleaseContext(c.Handle, hIme);
        }
    }

}
