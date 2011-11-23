using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Xml;
using DevComponents.DotNetBar;
using System.Configuration;
using Itop.Client;
using Itop.Domain.Graphics;
using ItopVector.DrawArea;
using ItopVector.Core;
using ItopVector.Core.Func;
using ItopVector.Core.Document;
using ItopVector.Core.Figure;
using ItopVector.Core.Interface.Figure;
using Itop.Domain.Stutistic;
using Itop.Client.Base;
using System.IO;
using System.Threading;
using ItopVector.Tools;
using ItopVector.Core.Interface;
using System.Xml.XPath;
using ItopVector.Core.Types;
using System.Diagnostics;
using Itop.MapView;

namespace Itop.TLPsp
{
    public partial class chaoliuResult : FormBase
    {
        public chaoliuResult()
        {
            InitializeComponent();
            //gridControl1.DataError += new DataGridViewDataErrorEventHandler(dataGridView1_DataError);
            //dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(dataGridView1_CellValueChanged);

            WireCategory wirewire = new WireCategory();
            IList list1 = Services.BaseService.GetList("SelectWireCategoryList", wirewire);
            foreach (WireCategory sub in list1)
            {
                repositoryItemComboBox2.Items.Add(sub.WireType);
                //repositoryItemComboBox2.Items.
                //repositoryItemComboBox2.add
                //repositoryItemComboBox1
                //lineTypeDataGridViewTextBoxColumn.Items.Add(sub.WireType);
                //colLineType.ColumnEdit.
                
            }
            string svgUID = "78ea0682-ad15-423b-8c61-68e7d5a6c29b";
            InitData(svgUID);
        }
        public void InitData(string svgUID)
        {
            PSPDEV pspDev = new PSPDEV();
            pspDev.SvgUID = svgUID;
            pspDev.Type = "Polyline";
            IList list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            foreach (PSPDEV dev in list)
            {
                if (dev.LineType != null)
                {
                    //if (!lineTypeDataGridViewTextBoxColumn.Items.Contains(dev.LineType))
                    //{
                    //    dev.LineType = "";
                    //}
                }
                Services.BaseService.Update<PSPDEV>(dev);
            }
            list = Services.BaseService.GetList("SelectPSPDEVBySvgUIDAndType", pspDev);
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSPDEV));

            gridControl1.DataSource = dt; 
        }

        private void hyperLinkEdit1_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}