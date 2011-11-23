				
using System;
using System.Collections.Generic;
using System.Text;

using System.Windows.Forms;

namespace Itop.Common {
    /// <summary>
    /// 窗体属性助手类
    /// </summary>
    public static class FormPropertyHelper {
        /// <summary>
        /// 把窗体初始化为对话框样式
        /// </summary>
        /// <param name="form">需要初始化的窗体</param>
        static public void DialogStyle(Form form) {
            form.ShowInTaskbar = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            
            // 宋体、小五
            form.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

            form.MaximizeBox = false;
            form.MinimizeBox = false;

            form.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        /// <summary>
        /// 把窗体初始化为模块主操作界面
        /// </summary>
        /// <param name="form">需要初始化的窗体</param>
        static public void ModuleStyle(Form form) {
            form.ShowInTaskbar = true;
            form.WindowState = FormWindowState.Maximized;


            // 宋体、小五
            form.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
        }
    }
}
