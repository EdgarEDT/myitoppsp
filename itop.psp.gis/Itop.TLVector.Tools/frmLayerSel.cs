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
    public partial class frmLayerSel : FormBase
    {
        private ArrayList alist =new ArrayList();

        public ArrayList LayList {
            get { return alist; }
        }
        private Hashtable hs;
        public frmLayerSel()
        {
            InitializeComponent();
            hs = new Hashtable();
        }
        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            if (DesignMode) return;
            LayerGrade lay = new LayerGrade();
            IList<LayerGrade> list = Services.BaseService.GetList<LayerGrade>("SelectLayerGradeList", lay);
            for (int i = 0; i < list.Count; i++) {
                ComboBoxItem<LayerGrade> item = new ComboBoxItem<LayerGrade>(list[i],list[i].Name);
                comboBox1.Items.Add(item);
            }
        }     
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {
           
            for(int i=0;i<layerList.Items.Count;i++){
                if(layerList.GetItemChecked(i)){
                    alist.Add(((SVG_LAYER)hs[layerList.GetItemText(i)]));
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

    }
    public class ComboBoxItem<T>
    {
        T _item;
        string _text;

        public string Text {
            get { return _text; }
            set { _text = value; }
        }
        public T Value {
            get { return _item; }
            set { _item = value; }
        }
        public ComboBoxItem(T obj, string text) { _item = obj; _text = text; }
        public override string ToString() {
            return Text;
        }
    }
}