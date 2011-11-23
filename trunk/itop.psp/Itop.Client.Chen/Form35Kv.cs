using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.GM;
using Itop.Domain.Chen;
using System.Web.Services.Description;
using Itop.Client.Common;
using Itop.Common;
using System.Diagnostics;
using DevExpress.XtraGrid.Columns;

namespace Itop.Client.Chen
{
    public partial class Form35Kv : Itop.Client.Base.FormBase
    {
        string type = "";

        public Form35Kv()
        {
            InitializeComponent();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
            this.ctrlCommon_Type1.AddObject();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlCommon_Type1.UpdateObject();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlCommon_Type1.DeleteObject();
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //添加地区
            PSP_35KVPingHeng p35 = new PSP_35KVPingHeng();
            p35.Type = type;

            using (FrmPSP_35KVPingHengDialog dlg = new FrmPSP_35KVPingHengDialog())
			{
				dlg.IsCreate = true;    //设置新建标志
                dlg.Object = p35;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
                InitData();
			}


        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //修改地区
            object obj=this.gridView.GetRow(this.gridView.FocusedRowHandle);
            if (obj == null)
                return;

            PSP_35KVPingHeng objCopy = (PSP_35KVPingHeng)obj;
            objCopy.Title = objCopy.DiQu;

            using (FrmPSP_35KVPingHengDialog dlg = new FrmPSP_35KVPingHengDialog())
            {
                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                InitData();
            }

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //删除地区
            //获取焦点对象
            //修改地区
            object obj = this.gridView.GetRow(this.gridView.FocusedRowHandle);
            if (obj == null)
                return;

            PSP_35KVPingHeng objCopy = (PSP_35KVPingHeng)obj;

            objCopy.Title = objCopy.DiQu;

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.Update("UpdatePSP_35KVPingHengByTypeAndTitleByDelete", obj);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }

            InitData();

        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //添加年
            try
            {
                FrmNewClass frm = new FrmNewClass();
                frm.Type = type;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    InitVisuble();
                }

            }
            catch (Exception ex) { MsgBox.Show(ex.Message); }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //删除年


            GridColumn gc = this.gridView.FocusedColumn;
            
            if (gc == null)
                return;
            if (gc.FieldName.Substring(0, 1) != "L")
            {
                MsgBox.Show("不能删除固定列");
                return;
            }

            if (MsgBox.ShowYesNo("是否删除 " + gc.Caption + " 的所有数据？") != DialogResult.Yes)
            {
                return;
            }


            foreach (GridColumn gc1 in this.gridView.Columns)
            {
                try
                {
                    if (gc1.Name == gc.Name)
                    {
                        gc1.Visible = false;
                    }
                }
                catch { }
            }



            gc.OptionsColumn.ShowInCustomizationForm = false;
            PSP_35KVPingHeng si = new PSP_35KVPingHeng();
            si.Title = gc.FieldName + "=0";
            si.Type = type;
            Itop.Client.Common.Services.BaseService.Update("UpdatePSP_35KVPingHengByTypeWhere", si);

            PSP_35KVStyle psl = new PSP_35KVStyle();
            psl.ClassType = gc.FieldName;
            psl.Type = type;
            psl.Title = gc.Caption;
            Itop.Client.Common.Services.BaseService.Update("DeletePSP_35KVStyleByAll", psl);


        }

        private void FrmLineInfo_Load(object sender, EventArgs e)
        {
            this.ctrlCommon_Type1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlCommon_Type1.Type = "35KV";
            this.ctrlCommon_Type1.RefreshData();






            InitVisuble();

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //打印
            ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //关闭
            this.Close();
        }

        private void Form35Kv_Load(object sender, EventArgs e)
        {
            this.ctrlCommon_Type1.GridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(GridView_FocusedRowChanged);
            this.ctrlCommon_Type1.Type = "35KV";
            this.ctrlCommon_Type1.RefreshData();
            
        }

        void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            object obj = this.ctrlCommon_Type1.GridView.GetRow(this.ctrlCommon_Type1.GridView.FocusedRowHandle);
            if (obj == null)
                return;

            Common_Type ct = (Common_Type)obj;
            type = ct.ID;

            InitData();
            InitVisuble();

        }

        private void InitData()
        {
            IList<PSP_35KVPingHeng> list = Services.BaseService.GetList<PSP_35KVPingHeng>("SelectPSP_35KVPingHengByType", type);
            //this.gridControl.DataSource = list;

            string s1 = "";
            string s2 = "";




            

            int cou = list.Count;
            int ss = cou / 4;

            DateTime dtime = DateTime.Now;
            PSP_35KVPingHeng p31 = new PSP_35KVPingHeng();
            p31.SortID1 = "全县";
            p31.DiQu="全县";
            p31.Title = "最大供电负荷";
            p31.SortID2 = 1;
            p31.CreateTime = dtime;

            PSP_35KVPingHeng p32 = new PSP_35KVPingHeng();
            p32.SortID1 = "全县";
            p32.DiQu = "全县";
            p32.Title = "本地平衡负荷";
            p32.SortID2 = 2;
            p32.CreateTime = dtime;

            PSP_35KVPingHeng p33 = new PSP_35KVPingHeng();
            p33.SortID1 = "全县";
            p33.DiQu = "全县";
            p33.Title = "需要35千伏受电电力";
            p33.SortID2 = 3;
            p33.CreateTime = dtime;

            PSP_35KVPingHeng p34 = new PSP_35KVPingHeng();
            p34.SortID1 = "全县";
            p34.DiQu = "全县";
            p34.Title = "需要35千伏变电容量";
            p34.SortID2 = 4;
            p34.CreateTime = dtime;

            PSP_35KVPingHeng p35 = new PSP_35KVPingHeng();
            p35.SortID1 = "全县";
            p35.DiQu = "全县";
            p35.Title = "已有35千伏变电容量";
            p35.SortID2 = 5;
            p35.CreateTime = dtime;

            PSP_35KVPingHeng p36 = new PSP_35KVPingHeng();
            p36.SortID1 = "全县";
            p36.DiQu = "全县";
            p36.Title = "需新增35千伏变电容量";
            p36.SortID2 = 6;
            p36.CreateTime = dtime;





            for (int i = 0; i<ss; i++)
            {
                PSP_35KVPingHeng p1 = list[4 * i];
                PSP_35KVPingHeng p2 = list[4 * i+1];
                PSP_35KVPingHeng p3 = list[4 * i+2];
                PSP_35KVPingHeng p4 = list[4 * i+3];

                PSP_35KVPingHeng p5 = new PSP_35KVPingHeng();
                p5.UID = Guid.NewGuid().ToString();
                p5.SortID1 = p1.SortID1;
                p5.SortID2 = 3;
                p5.DiQu = p1.DiQu;
                p5.Title = "需要35千伏受电电力";
                p5.CreateTime = p1.CreateTime;

                PSP_35KVPingHeng p6 = new PSP_35KVPingHeng();
                p6.UID = Guid.NewGuid().ToString();
                p6.SortID1 = p1.SortID1;
                p6.SortID2 = 6;
                p6.DiQu = p1.DiQu;
                p6.Title = "需新增35千伏变电容量";
                p6.CreateTime = p1.CreateTime;

                for (int j = 1; j <= 40; j++)
                {
                    double d1=0;
                    double d2=0;
                    double d3=0;
                    double d4=0;

                    double d31 = 0;
                    double d32 = 0;
                    double d33 = 0;
                    double d34 = 0;
                    double d35 = 0;
                    double d36 = 0;

                    try{
                     d1=(double)p1.GetType().GetProperty("L"+j).GetValue(p1,null);
                    }catch{}
                     try{
                         d2 = (double)p2.GetType().GetProperty("L" + j).GetValue(p2, null);
                    }catch{}
                         try{
                             d3 = (double)p3.GetType().GetProperty("L" + j).GetValue(p3, null);
                    }catch{}
                             try{
                                 d4 = (double)p4.GetType().GetProperty("L" + j).GetValue(p4, null);
                    }catch{}

                    try
                    {
                        d31 = (double)p31.GetType().GetProperty("L" + j).GetValue(p31, null);
                    }
                    catch { }

                    try
                    {
                        d32 = (double)p32.GetType().GetProperty("L" + j).GetValue(p32, null);
                    }
                    catch { }

                    try
                    {
                        d33 = (double)p33.GetType().GetProperty("L" + j).GetValue(p33, null);
                    }
                    catch { }

                    try
                    {
                        d34 = (double)p34.GetType().GetProperty("L" + j).GetValue(p34, null);
                    }
                    catch { }

                    try
                    {
                        d35 = (double)p35.GetType().GetProperty("L" + j).GetValue(p35, null);
                    }
                    catch { }

                    try
                    {
                        d36 = (double)p36.GetType().GetProperty("L" + j).GetValue(p36, null);
                    }
                    catch { }

                    p5.GetType().GetProperty("L" + j).SetValue(p5, d1 - d2,null);
                    p6.GetType().GetProperty("L" + j).SetValue(p6, d3 - d4,null);


                    p31.GetType().GetProperty("L" + j).SetValue(p31, d31+d1, null);
                    p32.GetType().GetProperty("L" + j).SetValue(p32, d32 + d2, null);
                    p33.GetType().GetProperty("L" + j).SetValue(p33, d33 + d1-d2, null);
                    p34.GetType().GetProperty("L" + j).SetValue(p34, d34 + d3, null);
                    p35.GetType().GetProperty("L" + j).SetValue(p35, d35 + d4, null);
                    p36.GetType().GetProperty("L" + j).SetValue(p36, d36 + d3-d4, null);
                }
                list.Add(p5);
                list.Add(p6);
            
            }

            list.Add(p31);
            list.Add(p32);
            list.Add(p33);
            list.Add(p34);
            list.Add(p35);
            list.Add(p36);

            gridControl.DataSource = list;
            InitVisuble();

        }





        private void InitVisuble()
        {
            IList<PSP_35KVStyle> li = Itop.Client.Common.Services.BaseService.GetList<PSP_35KVStyle>("SelectPSP_35KVStyleByType", type);
            foreach (GridColumn gc1 in this.gridView.Columns)
            {
                if (gc1.FieldName.Substring(0, 1) == "L")
                {
                    gc1.Visible = false;
                    foreach (PSP_35KVStyle pss in li)
                    {

                        if (gc1.FieldName == pss.ClassType)
                        {
                            gc1.Caption = pss.Title;
                            gc1.Visible = true;
                            gc1.OptionsColumn.ShowInCustomizationForm = true;
                        }
                    }
                }

            }

        }

        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            object obj = this.gridView.GetRow(this.gridView.FocusedRowHandle);
            if (obj == null)
                return;

            PSP_35KVPingHeng p35 = (PSP_35KVPingHeng)obj;
            Services.BaseService.Update<PSP_35KVPingHeng>(p35);
            InitData();
        }

        private void gridView_ShowingEditor(object sender, CancelEventArgs e)
        {
            object obj = this.gridView.GetRow(this.gridView.FocusedRowHandle);
            if (obj == null)
                return;

            PSP_35KVPingHeng p35 = (PSP_35KVPingHeng)obj;
            if (p35.Title == "全县" || p35.SortID2 == 3 || p35.SortID2 == 6)
                e.Cancel = true;


        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FileClass.ExportExcel(this.gridView.GroupPanelText,"", this.gridControl);
        }

    }
}