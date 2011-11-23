using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistic;
using System.Collections;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmLayerInPut : FormBase
    {
        public frmLayerInPut()
        {
            InitializeComponent();
        }
        public frmLayerInPut(string SvgDataUid)
        {
            InitializeComponent();  
            InitData(SvgDataUid);      
        }
        public void InitData(string SvgDataUid)
        {
             LayerFile temp = new LayerFile();
            temp.SvgDataUid = SvgDataUid; 
            IList layerList = Services.BaseService.GetList("SelectLayerFileBySvgDataUid", temp);
            //DataTable dt = Itop.Common.DataConverter.ToDataTable(layerList, typeof(LayerFile));
            //ln.Properties.DataSource = dt;
            //ln.Properties.DisplayMember = "LayerFileName";
            checkedListBox1.Items.Clear();
            checkedListBox1.BeginUpdate();
            foreach (LayerFile lay in layerList)
            {
                checkedListBox1.Items.Add(lay);
            }
            checkedListBox1.DisplayMember = "LayerFileName";
            checkedListBox1.EndUpdate();     
        }
        //public string InputText
        //{
        //    get
        //    {
        //        return ln.Text;
        //    }         
        //}
        public IList InputLayerList
        {
            get
            {
                return checkedListBox1.CheckedItems;
            }
        }

    }
}