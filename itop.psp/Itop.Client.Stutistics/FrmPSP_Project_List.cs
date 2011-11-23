using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils;
using Itop.Client.Base;
using Itop.Domain.Graphics;
using Itop.Domain.Stutistics;
using System.Collections;
using Itop.Domain.Stutistic;
using Itop.Client.Common;

namespace Itop.Client.Stutistics
{
    public partial class FrmPSP_Project_List : FormBase
    {
        string type = "JSXM";
        string typeFlag = "";

        public FrmPSP_Project_List()
        {
            InitializeComponent();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.AddObjecta(type);
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.UpdateObject();
            ///
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPowerEachList1.DeleteObject();//.DeleteObject("gusuan");
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormAddInfo_TouZiGuSuan tz = new FormAddInfo_TouZiGuSuan();
            tz.Isupdate = false;
            tz.ParentID = "0";
            tz.FlagId = typeFlag;
            tz.Text = "增加项目";
            if (tz.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.ctrlPSP_Project_List1.Flag = typeFlag;
                    this.ctrlPSP_Project_List1.RefreshData();
                }
                catch
                {
                }
            }

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (this.ctrlPSP_Project_List1.FocusedObject == null)
                return ;
            FormAddInfo_TouZiGuSuan tz = new FormAddInfo_TouZiGuSuan();
            tz.Isupdate = true;
            tz.ParentID = "0";
            tz.FlagId = typeFlag;
            tz.Text = "修改项目";
            tz.PowerUId = this.ctrlPSP_Project_List1.FocusedObject.ID;
            if (tz.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.ctrlPSP_Project_List1.Flag = typeFlag;
                    this.ctrlPSP_Project_List1.RefreshData();
                }
                catch
                {
                }
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ctrlPSP_Project_List1.DeleteObject();

        }

        private void FrmPSP_Project_List_Load(object sender, EventArgs e)
        {
            this.ctrlPowerEachList1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlPowerEachList1.IsBTN = true;
            this.ctrlPowerEachList1.RefreshData(type);
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (this.ctrlPowerEachList1.FocusedObject == null)
                return;

           
            WaitDialogForm wait = null;

            try
            {
                wait = new WaitDialogForm("", "正在加载数据, 请稍候...");
                InitSodata();
                this.ctrlPSP_Project_List1.Flag = typeFlag;
                this.ctrlPSP_Project_List1.RefreshData();

                wait.Close();

            }
            catch
            {
                wait.Close();
            }
        }


        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


        public void InitSodata()
        {
            if (ctrlPowerEachList1.FocusedObject == null)
                return;

            string sid = typeFlag = ctrlPowerEachList1.FocusedObject.UID;
            ctrlPSP_Project_List1.Flag = typeFlag;

            Hashtable hs = new Hashtable();
            Hashtable hs1 = new Hashtable();

            IList<LineInfo> listLineInfo = Services.BaseService.GetList<LineInfo>("SelectLineInfoByPowerID", sid);
            foreach (LineInfo l1 in listLineInfo)
            {
                hs.Add(Guid.NewGuid().ToString(), l1.UID);
            }

            IList<substation> listsubstation = Services.BaseService.GetList<substation>("SelectsubstationByPowerID2", sid);
            foreach (substation s1 in listsubstation)
            {
                hs.Add(Guid.NewGuid().ToString(), s1.UID);
            }

            PSP_Project_List psp_Type = new PSP_Project_List();
            psp_Type.Flag2 = sid;
            IList<PSP_Project_List> listProTypes = Common.Services.BaseService.GetList<PSP_Project_List>("SelectPSP_Project_ListByFlag2", psp_Type);
            foreach (PSP_Project_List ps in listProTypes)
            {
                hs1.Add(Guid.NewGuid().ToString(), ps.Code);
            }

            foreach (PSP_Project_List p1 in listProTypes)
            {

                if (p1.Code != "" && !hs.ContainsValue(p1.Code) && p1.ParentID != "0")
                {
                    //删除
                    Services.BaseService.Delete<PSP_Project_List>(p1);
                }
            }

            foreach (LineInfo l2 in listLineInfo)
            {
                if (!hs1.ContainsValue(l2.UID) && l2.Voltage != "")
                {
                    //添加
                    try
                    {
                        PSP_Project_List ps2 = new PSP_Project_List();
                        ps2.ParentID = l2.Voltage.ToUpper().Replace("KV", "");
                        ps2.L3 = l2.LineName;
                        ps2.Code = l2.UID;
                        ps2.Flag = 1;
                        ps2.Flag2 = sid;
                        ps2.L4 = l2.Voltage;
                        ps2.L8 = double.Parse(l2.Length).ToString(); ;
                        ps2.L9 = l2.LineType;
                        if (l2.ObligateField1 == "运行")
                        {
                            ps2.L2 = "扩建";
                        }
                        else if (l2.ObligateField1 == "规划")
                        {
                            ps2.L2 = "新建";
                        }
                        ps2.L15 = l2.ObligateField3;
                        ps2.ID = Guid.NewGuid().ToString();
                        Services.BaseService.Create<PSP_Project_List>(ps2);
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }

                }

                if (hs1.ContainsValue(l2.UID) && l2.Voltage != "")
                {
                    //更新
                    try
                    {
                        PSP_Project_List p2 = new PSP_Project_List();
                        p2.Code = l2.UID;
                        p2.Flag2 = sid;
                        PSP_Project_List ps2 = (PSP_Project_List)Services.BaseService.GetObject("SelectPSP_Project_ListByObject", p2);
                        ps2.ParentID = l2.Voltage.ToUpper().Replace("KV", "");
                        ps2.Flag = 1;
                        if (l2.ObligateField1 == "运行")
                        {
                            l2.ObligateField1 = "扩建";
                        }
                        else if (l2.ObligateField1 == "规划")
                        {
                            l2.ObligateField1 = "新建";
                        }
                        if (double.Parse(l2.Length).ToString() == "" || double.Parse(l2.Length).ToString() == null)
                            l2.Length="0";
                        if (ps2.L3 != l2.LineName || ps2.L4 != l2.Voltage || ps2.L8 != double.Parse(l2.Length).ToString() || ps2.L9 != l2.LineType || l2.ObligateField1 != ps2.L2 || ps2.L15 != l2.ObligateField3)
                        {
                            ps2.L3 = l2.LineName;
                            ps2.L4 = l2.Voltage;
                            ps2.L8 = double.Parse(l2.Length).ToString();
                            ps2.L9 = l2.LineType;
                            if (l2.ObligateField1 == "运行")
                            {
                                ps2.L2 = "扩建";
                            }
                            else if (l2.ObligateField1 == "规划")
                            {
                                ps2.L2 = "新建";
                            }
                            ps2.L15 = l2.ObligateField3;

                            Services.BaseService.Update("UpdatePSP_Project_ListByCode", ps2);
                        
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        System.Console.WriteLine(ex.Message);
                    }

                }
            }

            Project_Sum psp = new Project_Sum();
            psp.S5 = "2";
            IList<Project_Sum> sum = Common.Services.BaseService.GetList<Project_Sum>("SelectProject_SumByS5", psp);


            Hashtable ha = new Hashtable();
            ArrayList al = new ArrayList();
            foreach (substation s2 in listsubstation)
            {

                if (!hs1.ContainsValue(s2.UID) && s2.ObligateField1 != "")
                {

                    ha.Clear();
                    al.Clear();
                    int kk = 0;
                    //添加
                    try
                    {
                        Substation_Info sub = new Substation_Info();
                        sub.Code = s2.UID;
                        //Substation_Info station = (Substation_Info)Common.Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);


                        PSP_Project_List ps3 = new PSP_Project_List();
                        ps3.ParentID = s2.ObligateField1;
                        ps3.L3 = s2.EleName;
                        ps3.Code = s2.UID;
                        ps3.Flag = 2;
                        ps3.Flag2 = sid;
                        ps3.L4 = s2.ObligateField1.ToString();
                        if (s2.ObligateField3 == "运行")
                        {
                            ps3.L2 = "扩建";
                        }
                        else if (s2.ObligateField3 == "规划")
                        {
                            ps3.L2 = "新建";
                        }
                        ps3.L15 = s2.ObligateField5;
                        //if (station != null)
                        //{
                        //    ps3.L5 = station.L3.ToString();
                        //}

                        foreach (Project_Sum ps1 in sum)
                        {
                            if (s2.ObligateField1.ToString() == ps1.S1.ToString())
                            {
                                try
                                {
                                    double mva = double.Parse(s2.Number.ToString());
                                    double t5 = Convert.ToDouble(ps1.T5);//单台容量
                                    int ta = Convert.ToInt32(ps1.T1);//主变台数
                                    if (mva == (t5 * ta))
                                    {
                                        ha.Add(t5, ta);
                                        al.Add(t5);

                                    }
                                }
                                catch { }
                            }
                        }
                        if (al.Count > 0)
                        {
                            double va = Convert.ToDouble(al[0].ToString());
                            for (int ii = 0; ii < al.Count; ii++)
                            {
                                if (va < Convert.ToDouble(al[ii].ToString()))
                                    va = Convert.ToDouble(al[ii].ToString());
                            }
                            ps3.L5 = ha[va].ToString();
                            ps3.L6 = va.ToString();
                        }
                        else
                        {
                            ps3.L5 = "";
                            ps3.L6 = "";
                        }
                        ps3.IsConn = double.Parse(s2.Number.ToString()).ToString();//总容量
                        ps3.ID = Guid.NewGuid().ToString();
                        Services.BaseService.Create<PSP_Project_List>(ps3);
                    }
                    catch { }

                }
                if (hs1.ContainsValue(s2.UID) && s2.ObligateField1 != "")
                {
                    ha.Clear();
                    al.Clear();
                    int kk = 0;
                    //更新
                    try
                    {
                        Substation_Info sub = new Substation_Info();
                        sub.Code = s2.UID;
                        //Substation_Info station = (Substation_Info)Common.Services.BaseService.GetObject("SelectSubstation_InfoByCode", sub);

                        PSP_Project_List p3 = new PSP_Project_List();
                        p3.Code = s2.UID;
                        p3.Flag2 = sid;
                        PSP_Project_List ps3 = (PSP_Project_List)Services.BaseService.GetObject("SelectPSP_Project_ListByObject", p3);
                        ps3.ParentID = s2.ObligateField1;
                        ps3.Flag = 2;
                        if (s2.ObligateField3 == "运行")
                        {
                            s2.ObligateField3 = "扩建";
                        }
                        else if (s2.ObligateField3 == "规划")
                        {
                            s2.ObligateField3 = "新建";
                        }
                        string l5 = "";
                        string l6 = "";
                        foreach (Project_Sum ps1 in sum)
                        {
                            if (s2.ObligateField1.ToString() == ps1.S1.ToString())
                            {
                                try
                                {
                                    double mva=0;
                                    if(s2.Number.ToString()!=""&&s2.Number.ToString()!=null)
                                       mva = double.Parse(s2.Number.ToString());
                                    double t5 = Convert.ToDouble(ps1.T5);//单台容量
                                    int ta = Convert.ToInt32(ps1.T1);//主变台数
                                    if (mva == (t5 * ta))
                                    {
                                        ha.Add(t5, ta);
                                        al.Add(t5);

                                    }
                                }
                                catch { }
                            }
                        }
                        if (al.Count > 0)
                        {
                            double va = Convert.ToDouble(al[0].ToString());
                            for (int ii = 0; ii < al.Count; ii++)
                            {
                                if (va < Convert.ToDouble(al[ii].ToString()))
                                    va = Convert.ToDouble(al[ii].ToString());
                            }
                            l5 = ha[va].ToString();
                            l6 = va.ToString();
                        }
                        else
                        {
                            l5 = "";
                            l6 = "";
                        }

                        if (ps3.L3 != s2.EleName || ps3.L4 != s2.ObligateField1.ToString() || s2.ObligateField3 != ps3.L2 || ps3.L15 != s2.ObligateField5 || ps3.L5 != l5 || ps3.L6!= l6)
                        {
                            ps3.L3 = s2.EleName;
                            ps3.L4 = s2.ObligateField1.ToString();
                            if (s2.ObligateField3 == "运行")
                            {
                                ps3.L2 = "扩建";
                            }
                            else if (s2.ObligateField3 == "规划")
                            {
                                ps3.L2 = "新建";
                            }
                            ps3.L15 = s2.ObligateField5;
                            //if (station != null)
                            //{
                            //    ps3.L5 = station.L3.ToString();
                            //}

                            ps3.IsConn = double.Parse(s2.Number.ToString()).ToString();//总容量
                            Services.BaseService.Update("UpdatePSP_Project_ListByCode", ps3);
                        }
                    }
                    catch { }
                }
            }
           

        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FrmPSP_Project_ListPrint_TouZiGuSuan tzprint = new FrmPSP_Project_ListPrint_TouZiGuSuan();
            PSP_Project_List pp = new PSP_Project_List();

            pp.Flag2 = typeFlag;
            IList list = Services.BaseService.GetList("SelectPSP_Project_ListByFlag2", pp);
            try
            {
                foreach (PSP_Project_List pl in list)
                {
                    if (pl.L5 != "" && pl.L5 != null)
                    {
                        if (int.Parse(pl.L5) > 1)
                        {
                            pl.L6 = pl.L6 + " X " + pl.L5;
                        }
                    }
                 }
            }
            catch { }
            DataTable dt = Itop.Common.DataConverter.ToDataTable(list, typeof(PSP_Project_List));
            tzprint.Text =this.ctrlPowerEachList1.FocusedObject.ListName;
            //tzprint.gridControl1.Text= "4455";
            tzprint.bandedGridView1.GroupPanelText = this.ctrlPowerEachList1.FocusedObject.ListName;
          tzprint.GridDataTable = dt;
          tzprint.Show();
                 
            
        }
    }
}