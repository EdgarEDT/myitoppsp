using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
using DevExpress.XtraGrid;
namespace Itop.TLPsp.Graphical {
    public partial class UcPddate : DevExpress.XtraEditors.XtraUserControl {
        public UcPddate() {
            InitializeComponent();
        }
        private DataTable datatable = new DataTable();
        private string parentID;
        public string ParentID {
            get { return parentID; }
            set {
                parentID = value;
                if (!string.IsNullOrEmpty(value)) {
                    RefreshData(" where jckyID='" + value + "' order by CreateDate");
                }
            }
        }
        private void RefreshData(string con)
        {
         
        }
          private void AddFixColumn()
        {
            
           
        }
    }
}
