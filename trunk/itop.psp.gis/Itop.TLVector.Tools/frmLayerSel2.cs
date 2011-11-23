using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using ItopVector.Core.Figure;
using Itop.Domain.Graphics;
using Itop.Client.Common;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    
    /// <summary>
    /// 图层选择器，可多选
    /// </summary>
    public partial class frmLayerSel2 : FormBase
    {
        private ArrayList alist =new ArrayList();

        public ArrayList LayList {
            get { return alist; }
            set { alist = value; }
        }
        private Hashtable hs;
        public frmLayerSel2()
        {
            InitializeComponent();
            hs = new Hashtable();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (DesignMode) return;
            hs.Clear();
            foreach (Layer layer in LayList) {
                layerList.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(layer.Label));
                hs.Add(layer.Label, layer);
            }
        }     
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            alist.Clear();
            for(int i=0;i<layerList.Items.Count;i++){
                if(layerList.GetItemChecked(i)){
                    alist.Add(hs[layerList.GetItemText(i)]);
                }
            }            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            ComboBoxItem<LayerGrade> item = comboBox1.SelectedItem as ComboBoxItem<LayerGrade>;
            if (item == null) return;
            
            string uid =Itop.Common.ConfigurationHelper.GetValue("SvgID");
            if (string.IsNullOrEmpty(uid)) return;
            SVG_LAYER lar = new SVG_LAYER();
            lar.svgID = uid;
            lar.YearID = "'" + item.Value.SUID + "'";
            IList<SVG_LAYER> larlist = Services.BaseService.GetList<SVG_LAYER>("SelectSVG_LAYERByYearID", lar);
            layerList.Items.Clear();
            hs.Clear();
            foreach (SVG_LAYER layer in larlist) {
                layerList.Items.Add(new DevExpress.XtraEditors.Controls.CheckedListBoxItem(layer.NAME));
                hs.Add(layer.NAME,layer);
            }
        }

        private void bSelectA_Click(object sender, EventArgs e) {
            for (int i = 0; i < layerList.ItemCount; i++) {
                layerList.SetItemChecked(i, true);
            }
        }

        private void bDselectA_Click(object sender, EventArgs e) {
            for (int i = 0; i < layerList.ItemCount; i++) {
                layerList.SetItemChecked(i, false);
            }
        }

    }

}