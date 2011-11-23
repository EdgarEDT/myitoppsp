using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client;
using Itop.Client.Common;
using Itop.Client.Base;

namespace ItopVector.Tools
{
    public partial class frmUsePropertySelect : FormBase
    {
        DataRow row=null;
        DataTable propdt;
        private string typeUID;
        private string UseID;
        private string SvgDataUID;

        public frmUsePropertySelect()
        {
            InitializeComponent();
        }

        private void frmUsePropertySelect_Load(object sender, EventArgs e)
        {
           
        }

        private void gridControl2_EmbeddedNavigator_ButtonClick(object sender, DevExpress.XtraEditors.NavigatorButtonClickEventArgs e)
        {

        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            row = gridView2.GetDataRow(gridView2.FocusedRowHandle);
            if (row == null)
                return;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(row==null){
                MessageBox.Show("请选择分类。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return;
            }
            TypeUID = row["UID"].ToString();
            UseRelating _userel=new UseRelating();
            _userel.UseID = UseID;
            
            IList<UseRelating> UseRelList= Services.BaseService.GetList<UseRelating>("SelectUseRelatingByUseID", _userel);
            if(UseRelList.Count>0){
                _userel=(UseRelating)UseRelList[0];
                if(!string.IsNullOrEmpty(_userel.UsePropertyUID)){
                    if(MessageBox.Show("选择的地块已经设置了分类，确定要更换分类么？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes){
                        property _pro=new property();
                        _pro.TypeUID = _userel.UsePropertyUID;
                        _pro.UseUID = _userel.UseID;
                        Services.BaseService.Update("DeletepropertyByTypeAndUseUID", _pro);
                        _userel.UsePropertyUID=TypeUID;
                        Services.BaseService.Update<UseRelating>(_userel);
                    }
                }
                else{
                     _userel.UsePropertyUID=TypeUID;
                     Services.BaseService.Update<UseRelating>(_userel);
                }
            }
            else{
                _userel.UID=Guid.NewGuid().ToString();
                _userel.UseID=UseID;
                _userel.SvgUid = SvgDataUID;
                _userel.UsePropertyUID=TypeUID;
                Services.BaseService.Create<UseRelating>(_userel);
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void InitData(string ID,string SvgUID)
        {
            UseID =ID ;
            SvgDataUID = SvgUID;
            UsepropertyType _propType = new UsepropertyType();
            propdt = Itop.Common.DataConverter.ToDataTable(Services.BaseService.GetList("SelectUsepropertyTypeList", _propType), typeof(UsepropertyType));
            gridControl2.DataSource = propdt;
        }

        public string TypeUID
        {
            set { typeUID = value; }
            get { return typeUID; }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}