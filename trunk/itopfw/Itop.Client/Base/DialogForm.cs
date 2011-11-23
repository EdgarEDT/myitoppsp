
				
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Itop.Client.Base {
    /// <summary>
    /// Dialog基类
    /// </summary>
    public partial class DialogForm : FormBase {
        public DialogForm() {
            InitializeComponent();

            Itop.Common.FormPropertyHelper.DialogStyle(this);
        }
    }
}