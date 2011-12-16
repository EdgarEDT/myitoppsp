using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Itop.TLPsp.Graphical {
    public partial class XtraPDrelfrm : DevExpress.XtraEditors.XtraForm {
        public XtraPDrelfrm() {
            InitializeComponent();
            ucPdtype1.init();
           ucPdtype1.FocusedNodeChanged+=new UcPdtype.SendDataEventHandler<Itop.Domain.Graphics.PDrelregion>(ucPdtype1_FocusedNodeChanged);
        }
        void ucPdtype1_FocusedNodeChanged(object sender, Itop.Domain.Graphics.PDrelregion e)
        {
           ucPddate1.ParentObj = e;
        }
    }
}