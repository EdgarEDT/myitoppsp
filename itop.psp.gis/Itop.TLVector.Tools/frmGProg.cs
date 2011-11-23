using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using Itop.Domain.Graphics;
using Itop.Common;
using Itop.Client.Common;
using System.Collections;
using Itop.Client.Base;
namespace ItopVector.Tools
{
    public partial class frmGProg : FormBase
    {
        private string key = "";
        private string name = "";

        public string Name1
        {
            get { return name; }
            set { name = value; }
        }

        public string Key
        {
            get { return key; }
            set { key = value; }
        }

        public bool pspflag = true;   //如果是原先的电网规划 关联数据不显示 现在的就显示它
        public frmGProg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ctrlPSP_GProg1.AddObject();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ctrlPSP_GProg1.DeleteObject();
        }

        private void frmPlanList_Load(object sender, EventArgs e)
        {
            ctrlPSP_GProg1.RefreshData();
            button5.Visible = pspflag;
        }

      

        private void button5_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ctrlPSP_GProg1.FocusedObject == null)
            {
                MessageBox.Show("请选择记录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            key = ctrlPSP_GProg1.FocusedObject.UID;
            name = ctrlPSP_GProg1.FocusedObject.ProgName;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private string svguid = ConfigurationSettings.AppSettings.Get("SvgID");
        private void button4_Click(object sender, EventArgs e)
        {
           
            frmYear f = new frmYear();
            f.uid = svguid;
            f.Show();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            frmGHDeviceList frmDevList = new frmGHDeviceList();
            frmDevList.ProjectID = Itop.Client.MIS.ProgUID;
            frmDevList.ProjectSUID = ctrlPSP_GProg1.FocusedObject.UID;
            frmDevList.Init();
            if (frmDevList.ShowDialog() == DialogResult.OK)
            {
                foreach (DataRow row in frmDevList.DT.Rows)
                {
                    try
                    {
                        if ((bool)row["C"])
                        {
                            PSP_GprogElevice elcDevice = new PSP_GprogElevice();
                            elcDevice.DeviceSUID = row["A"].ToString();
                            elcDevice.GprogUID = ctrlPSP_GProg1.FocusedObject.UID;
                            elcDevice =(PSP_GprogElevice) Services.BaseService.GetObject("SelectPSP_GprogEleviceByKey", elcDevice);
                            if (elcDevice!=null)
                            {
                                elcDevice.Type = row["D"].ToString();
                                elcDevice.L2 = "0";
                                Services.BaseService.Update<PSP_GprogElevice>(elcDevice);
                            }
                            else
                            {
                                elcDevice = new PSP_GprogElevice();
                                elcDevice.DeviceSUID = row["A"].ToString();
                                elcDevice.GprogUID = ctrlPSP_GProg1.FocusedObject.UID;
                                elcDevice.Type = row["D"].ToString();
                                elcDevice.L2 = "0";
                                Services.BaseService.Create<PSP_GprogElevice>(elcDevice);
                            }
                           
                        }
                        else
                        {
                            PSP_GprogElevice elcDevice = new PSP_GprogElevice();
                            elcDevice.DeviceSUID = row["A"].ToString();
                            elcDevice.GprogUID = ctrlPSP_GProg1.FocusedObject.UID;

                            Services.BaseService.Delete<PSP_GprogElevice>(elcDevice);
                        }
                    }
                    catch (System.Exception ex)
                    {

                    }
                }
            }
            else
                return;
            //在此处将其选择的元件设备的属性进行更改
            LayerGrade l1 = new LayerGrade();
             l1.Type = "1";
             l1.SvgDataUid = svguid;
             IList ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
             int yy1 = System.DateTime.Now.Year, yy2 = System.DateTime.Now.Year, yy3 = System.DateTime.Now.Year;
             if (ttlist.Count > 0)
              {
                LayerGrade n1 = (LayerGrade)ttlist[0];
                yy1 = Convert.ToInt32(n1.Name.Substring(0, 4));
               }
             l1.Type = "2";
             l1.SvgDataUid = svguid;
             ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
             if (ttlist.Count > 0)
             {
                 LayerGrade n1 = (LayerGrade)ttlist[0];
                 yy2 = Convert.ToInt32(n1.Name.Substring(0, 4));
             }
             l1.Type = "3";
             l1.SvgDataUid = svguid;
             ttlist = Services.BaseService.GetList("SelectLayerGradeList5", l1);
             if (ttlist.Count > 0)
             {
                 LayerGrade n1 = (LayerGrade)ttlist[0];
                 yy3 = Convert.ToInt32(n1.Name.Substring(0, 4));
             }
           string con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '变电站'";
            
            IList list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            foreach (PSP_GprogElevice pg in list)  
            {

                PSP_Substation_Info ps = new PSP_Substation_Info();
                ps.UID = pg.DeviceSUID;
                ps = (PSP_Substation_Info)Services.BaseService.GetObject("SelectPSP_Substation_InfoByKey", ps);
                if (ps != null)
                {
                    int s2 = 0;
                    if (!string.IsNullOrEmpty(ps.S2))
                    {
                        s2 = Convert.ToInt32(ps.S2);
                    }
                    if (s2 <= System.DateTime.Now.Year)
                    {
                        pg.L1 = "现行";
                    }
                    else if (s2 > System.DateTime.Now.Year && Convert.ToInt32(ps.S2) <= yy1)
                    {
                        pg.L1 = "近期";
                    }
                    else if (s2 > yy1 && Convert.ToInt32(ps.S2) <=yy2)
                    {
                        pg.L1 = "中期";
                    }
                    else if (s2 > yy2 && Convert.ToInt32(ps.S2) <= yy3)
                    {
                        pg.L1 = "远期";
                    }
                    Services.BaseService.Update<PSP_GprogElevice>(pg);
                }
                else
                    Services.BaseService.Delete<PSP_GprogElevice>(pg);
              
            }
              con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '电源'";
            
           list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            foreach (PSP_GprogElevice pg in list)  
            {
                PSP_PowerSubstation_Info ps = new PSP_PowerSubstation_Info();
                ps.UID = pg.DeviceSUID;
                ps = (PSP_PowerSubstation_Info)Services.BaseService.GetObject("SelectPSP_PowerSubstation_InfoByKey", ps);
                if (ps!=null)
                {
                    int s2 = 0;
                    if (!string.IsNullOrEmpty(ps.S3))
                    {
                        s2 = Convert.ToInt32(ps.S3);
                    }
                    if (s2 <= System.DateTime.Now.Year)
                    {
                        pg.L1 = "现行";
                    }
                    else if (s2 > System.DateTime.Now.Year && Convert.ToInt32(ps.S3) <= yy1)
                    {
                        pg.L1 = "近期";
                    }
                    else if (s2 > yy1 && Convert.ToInt32(ps.S3) <= yy2)
                    {
                        pg.L1 = "中期";
                    }
                    else if (s2 >yy2 && Convert.ToInt32(ps.S3) <= yy3)
                    {
                        pg.L1 = "远期";
                    }
                    Services.BaseService.Update<PSP_GprogElevice>(pg);
                }
                else
                    Services.BaseService.Delete<PSP_GprogElevice>(pg);
                }
               //变电站里判断两绕和三绕是哪个时期的
              con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '变电站'" ;

               list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
                foreach (PSP_GprogElevice pg in list)  
                {
                    con = "c.UID='" + pg.DeviceSUID + "'and a.Type='02'and a.ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                    IList uidlist = Services.BaseService.GetList("SelectPSPDEV_byqSUID", con);
                    foreach (string uid in uidlist)
                    {
                    
                        con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '两绕组变压器'AND DeviceSUID='" + uid + "'";
                        PSP_GprogElevice pglr = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByCondition", con);
                        if (pglr!=null)
                        {
                            pglr.L1 = pg.L1;
                            Services.BaseService.Update<PSP_GprogElevice>(pglr);
                        }
                        
                    }
                    con = "c.UID='" + pg.DeviceSUID + "'and a.Type='03'and a.ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                    uidlist = Services.BaseService.GetList("SelectPSPDEV_byqSUID", con);
                    foreach (string uid in uidlist)
                    {

                        con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '三绕组变压器'AND DeviceSUID='" + uid + "'";
                        PSP_GprogElevice pglr = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByCondition", con);
                        if (pglr != null)
                        {
                            pglr.L1 = pg.L1;
                            Services.BaseService.Update<PSP_GprogElevice>(pglr);
                        }

                    }
                }
               //发电厂里两绕和三绕组是哪个时期的
                con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '电源'";

                list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
                foreach (PSP_GprogElevice pg in list)
                {
                    con = "c.UID='" + pg.DeviceSUID + "'and a.Type='02'and a.ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                    IList uidlist = Services.BaseService.GetList("SelectPSPDEV_byqSUID", con);
                    foreach (string uid in uidlist)
                    {

                        con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '两绕组变压器'AND DeviceSUID='" + uid + "'";
                        PSP_GprogElevice pglr = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByCondition", con);
                        if (pglr != null)
                        {
                            pglr.L1 = pg.L1;
                            Services.BaseService.Update<PSP_GprogElevice>(pglr);
                        }

                    }
                    con = "c.UID='" + pg.DeviceSUID + "'and a.Type='03'and a.ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                    uidlist = Services.BaseService.GetList("SelectPSPDEV_byqSUID", con);
                    foreach (string uid in uidlist)
                    {

                        con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '三绕组变压器'AND DeviceSUID='" + uid + "'";
                        PSP_GprogElevice pglr = (PSP_GprogElevice)Services.BaseService.GetObject("SelectPSP_GprogEleviceByCondition", con);
                        if (pglr != null)
                        {
                            pglr.L1 = pg.L1;
                            Services.BaseService.Update<PSP_GprogElevice>(pglr);
                        }

                    }
                }
            //判断设备参数中有没有在规划设备中的两绕，三绕组变压器，发电机和负荷 如果没有则删掉
                con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND (Type= '两绕组变压器'or Type= '三绕组变压器'or Type= '负荷'or Type= '发电机')";

                list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
                foreach (PSP_GprogElevice pg in list) 
                {
                    PSPDEV ps = new PSPDEV();
                    ps.SUID = pg.DeviceSUID;
                    ps = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", ps);
                    if (ps==null)
                    {
                        Services.BaseService.Delete<PSP_GprogElevice>(pg);
                    }
                }
            //线路信息
               con = "GprogUID = '" + ctrlPSP_GProg1.FocusedObject.UID + "' AND Type= '线路'";
            
            list = Services.BaseService.GetList("SelectPSP_GprogEleviceByCondition", con);
            foreach (PSP_GprogElevice pg in list)  
            { 
                PSPDEV ps = new PSPDEV();
                ps.SUID = pg.DeviceSUID;
                ps = (PSPDEV)Services.BaseService.GetObject("SelectPSPDEVByKey", ps);
                if (ps!=null)
                {
                    int s2 = 0;
                    if (!string.IsNullOrEmpty(ps.OperationYear))
                    {
                        s2 = Convert.ToInt32(ps.OperationYear);
                    }
                    if (s2 <= System.DateTime.Now.Year)
                    {
                        pg.L1 = "运行";
                        ps.LineStatus = "运行";
                    }
                    else
                    {
                        pg.L1 = "待选";
                        ps.LineStatus = "待选";

                    }
                    Services.BaseService.Update<PSP_GprogElevice>(pg);
                    Services.BaseService.Update<PSPDEV>(ps);
                }
                else
                    Services.BaseService.Delete<PSP_GprogElevice>(pg);
              
            }

        }
    }
}