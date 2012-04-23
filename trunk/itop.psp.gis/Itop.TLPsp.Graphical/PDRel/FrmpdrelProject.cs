﻿using System;
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
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes;
using Itop.TLPSP.DEVICE;
using System.Reflection;
namespace Itop.TLPsp.Graphical.PDRel
{
    public partial class FrmpdrelProject : FormBase
    {
        public FrmpdrelProject()
        {
            InitializeComponent();
            ucPdreltype1.FocusedNodeChanged += new UcPdreltype.SendDataEventHandler<Ps_pdreltype>(ucPdtype1_FocusedNodeChanged);
        }
        void ucPdtype1_FocusedNodeChanged(object sender, Ps_pdreltype e)
        {
            ucPdtypenode1.ParentObj = e;
        }
        public void init()
        {
            ucPdreltype1.init();
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

        }
    }
}
