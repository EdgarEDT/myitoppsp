using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using Itop.Client.Base;
namespace ItopVector.Tools {
    public partial class frmGoLocation : FormBase {
        private string svgUid = "";
        ArrayList list = new ArrayList();
        private string eleID = "";
        private string strSelLayer = "";

        public string StrSelLayer {
            get { return strSelLayer; }
            set { strSelLayer = value; }
        }

        public string EleID {
            get { return eleID; }
            set { eleID = value; }
        }
        public string SvgUid {
            get { return svgUid; }
            set { svgUid = value; }
        }

        public frmGoLocation() {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            IList svglist;
            if (textEdit1.Text == "") {
                MessageBox.Show("名称不能为空。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            gtlist.Items.Clear();
            list.Clear();
            if (radioGroup1.SelectedIndex == 0) {

                string str = "  Title like '%" + textEdit1.Text + "%' and AreaID ='" + Itop.Client.MIS.ProgUID + "' order by AreaName ";
                svglist = Services.BaseService.GetList("SelectPSP_Substation_InfoListByWhere", str);
                for (int i = 0; i < svglist.Count; i++) {
                    if (((PSP_Substation_Info)svglist[i]).LayerID == null) {
                        continue;
                    }
                    if (strSelLayer.Contains(((PSP_Substation_Info)svglist[i]).LayerID)) {
                        gtlist.Items.Add(((PSP_Substation_Info)svglist[i]).Title);
                        list.Add(((PSP_Substation_Info)svglist[i]).UID);
                    }
                }
            }
            if (radioGroup1.SelectedIndex == 1) {
                //LineInfo line = new LineInfo();
                //line.LineName = " SvgUID='" + svgUid + "' and LineName like '%" + textEdit1.Text + "%' order by LineName ";
                //svglist = Services.BaseService.GetList("SelectLineInfoByWhere", line);
                //PSPDEV line = new PSPDEV();
                //string line = " where SvgUID='" + svgUid + "' and Name like '%" + textEdit1.Text + "%' order by Name ";
                string line = " where  Name like '%" + textEdit1.Text + "%' and ProjectID ='" + Itop.Client.MIS.ProgUID + "' order by Name ";
                svglist = Services.BaseService.GetList("SelectPSPDEVByCondition", line);

                for (int i = 0; i < svglist.Count; i++) {
                    //if(((PSPDEV)svglist[i]).LayerID!=null){
                    //    if (strSelLayer.Contains(((PSPDEV)svglist[i]).LayerID))
                    //    {
                    gtlist.Items.Add(((PSPDEV)svglist[i]).Name);
                    list.Add(((PSPDEV)svglist[i]).SUID);
                    //    }
                    //}
                }
            }

        }

        private void simpleButton3_Click(object sender, EventArgs e) {
            int i = gtlist.SelectedIndex;
            if (i == -1) {
                MessageBox.Show("请选择图元。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            eleID = list[i].ToString();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}