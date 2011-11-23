using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using ItopVector.Tools;
using Itop.Client.Common;
using Itop.Domain.Graphics;
using System.Configuration;
using System.IO;
using System.Threading;
using Itop.Domain.RightManager;
using Itop.Client;

namespace Itop.DLGH
{
    public partial class frmTopView : FormBase
    {
       static string stype = "";
       
        public frmTopView()
        {
            InitializeComponent();
        }

        private void frmTopView_Load(object sender, EventArgs e)
        {
            
            string svguid = ConfigurationSettings.AppSettings.Get("SvgID");
            CkImage(svguid);
            ctrlSvgView1.OpenFromDatabase(svguid);//("c5ec3bc7-9706-4cbd-9b8b-632d3606f933");
            
             //ctrlSvgView1.Init();
             if(smdgroup.upd=="1")
             {
                 ctrlSvgView1.EditRight = true;
             }
             
             ctrlSvgView1.LayerManagerShow();
             
             ctrlSvgView1.frmlar.Owner = this;
        }
        public void CkImage(string sid)     
        {
            SVG_IMAGE img = new SVG_IMAGE();
            img.svgID = sid;
            IList<SVG_IMAGE> list = Services.BaseService.GetList<SVG_IMAGE>("SelectSVG_IMAGEBySvgID", img);
            for (int i = 0; i < list.Count; i++)
            {
                if (!System.IO.File.Exists(Application.StartupPath + "\\" + list[i].SUID + "." + list[i].col1))
                {
                    FileStream fs = new FileStream(Application.StartupPath + "\\" + list[i].SUID + "." + list[i].col1, FileMode.Create, FileAccess.Write, FileShare.None);
                    fs.Write(list[i].image, 0, list[i].image.Length);
                    fs.Close();
                }

            }
        }
        public void dl()
        {
            stype="地理信息层";
            this.Text = "地理信息";
            ctrlSvgView1.progtype = stype;
            //ctrlSvgView1.comboSel.Visible = false;
            this.Show();
            string t = "";
            string title = "";
            getProjName(MIS.ProgUID, ref title);

        

            this.Text = title + " " + t + " 地理信息";
        }
        public void csgh()
        {
            stype = "城市规划层";
            this.Text = "城市规划";
            ctrlSvgView1.progtype = stype;
            this.Show();
            string t = "";
            string title = "";
            getProjName(MIS.ProgUID, ref title);

        

            this.Text = title + " " + t + " 城市规划";
        }
        public void dwgh()
        {
            stype = "电网规划层";
           // this.Text = "电网规划";
            ctrlSvgView1.progtype = stype;
            this.Show();
           
        }
        public void getProjName(string uid, ref string title)
        {
            Project sm = Services.BaseService.GetOneByKey<Project>(uid);
            if (sm != null)
            {
                title = sm.ProjectName + " " + title;
                if (sm.ProjectManager == sm.UID) { return; }
                getProjName(sm.ProjectManager, ref title);
            }
            //return title;
        }
        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void frmTopView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(CtrlSvgView.MapType == "所内接线图"){
                CtrlSvgView.MapType = "接线图";
            }
            e.Cancel = false;
            //ctrlSvgView1.frmlar.Owner.Close();
            //ctrlSvgView1.frmlar.Close();
            //this.Close();
        }
    }
}