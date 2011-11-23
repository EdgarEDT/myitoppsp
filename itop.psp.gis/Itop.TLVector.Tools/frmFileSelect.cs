using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client;
using Itop.Client.Common;

using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmFileSelect : FormBase
    {
        public string svgDataUid;
        public string UseUid;
        public string SelSvgUid;
        private bool isLink = false;

        public frmFileSelect()
        {
            InitializeComponent();
            //ctrlFileManager1.MenuClose();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SelSvgUid = ctrlFileManager1.SvgUid;
            if(isLink){
                if (string.IsNullOrEmpty(SelSvgUid))
                {
                    MessageBox.Show("请选择要关联的地图。","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    return;
                }
               
                UseRelating _userel=new UseRelating();
                _userel.UseID = UseUid;
                _userel.SvgUid = svgDataUid;
                IList<UseRelating> UseRelList= Services.BaseService.GetList<UseRelating>("SelectUseRelatingByUseID", _userel);
                if(UseRelList.Count>0){
                    _userel=(UseRelating)UseRelList[0];
                    if(!string.IsNullOrEmpty(_userel.LinkUID)){
                        if(MessageBox.Show("选择的地块已经关联了其他地图，确定要更换关联么？","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes){
                            _userel.LinkUID=SelSvgUid;
                            Services.BaseService.Update<UseRelating>(_userel);
                        }
                    }
                    else{
                         _userel.LinkUID=SelSvgUid;
                         Services.BaseService.Update<UseRelating>(_userel);
                    }
                }
                else{
                    _userel.UID=Guid.NewGuid().ToString();
                    _userel.UseID = UseUid;
                    _userel.SvgUid = svgDataUid;
                    _userel.LinkUID = SelSvgUid;
                    Services.BaseService.Create<UseRelating>(_userel);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void InitData(string ID,string SvgUid,bool _isLink)
        {
            UseUid = ID;
            svgDataUid = SvgUid;
            isLink = _isLink;
        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}