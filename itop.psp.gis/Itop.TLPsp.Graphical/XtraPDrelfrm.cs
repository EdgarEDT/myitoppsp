using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using System.Collections;
using Itop.Common;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using Itop.Client.Stutistics;
using System.Xml;
using ItopVector.Tools;
using Itop.Client.Base;
namespace Itop.TLPsp.Graphical {
    public partial class XtraPDrelfrm :FormBase {
        public XtraPDrelfrm() {
            InitializeComponent();
            ucPdtype1.init();
           ucPdtype1.FocusedNodeChanged+=new UcPdtype.SendDataEventHandler<PDrelregion>(ucPdtype1_FocusedNodeChanged);
        }
        void ucPdtype1_FocusedNodeChanged(object sender,PDrelregion e)
        {
           ucPddate1.ParentObj = e;
        }
    }
}