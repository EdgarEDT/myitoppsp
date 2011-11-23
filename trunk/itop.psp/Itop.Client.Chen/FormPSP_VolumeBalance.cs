using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Chen;
using System.Collections;
using DevExpress.XtraGrid.Columns;
using Itop.Client.Common;
using Itop.Domain.HistoryValue;
using Itop.Domain.Stutistic;
using DevExpress.XtraTreeList.Columns;

namespace Itop.Client.Chen
{
    public partial class FormPSP_VolumeBalance : Itop.Client.Base.FormModuleBase
    {
        private string TypeID = "";
        private string Volumecalc = "Volumecalc";
        private string loadrate = "loadrate";
        private string Flag = "";
        private PowerEachList forecastReport = null;
        //private DevExpress.XtraBars.BarSubItem barSub1;
        private DevExpress.XtraBars.BarButtonItem barA1;
        private DevExpress.XtraBars.BarButtonItem barA2;
        private DevExpress.XtraBars.BarButtonItem barA3;
        private DevExpress.XtraBars.BarButtonItem barA4;
        private DevExpress.XtraBars.BarButtonItem barA5;
        private DevExpress.XtraBars.BarButtonItem barA6;
        private DevExpress.XtraBars.BarButtonItem barA7;
        private DevExpress.XtraBars.BarButtonItem barA8;
        private DevExpress.XtraBars.BarButtonItem barA9;
        private DevExpress.XtraBars.BarButtonItem barA10;
        bool _canEdit = true;
        public bool CanEdit
        {
            get { return _canEdit; }
            set { _canEdit = value; }
        }
        bool _canPrint = true;
        bool _isSelect = false;
        private bool AddRight = false;
        private bool EditRight = false;
        private bool DeleteRight = false;
        private bool PrintRight = false;
        public bool IsSelect
        {
            get { return _isSelect; }
            set { _isSelect = value; }
        }
        bool IS = false;
        public bool ADdRight
        {
            get { return AddRight; }
            set { AddRight = value; }
        }
        public bool EDitRight
        {
            get { return EditRight; }
            set { EditRight = value; }
        }
        public bool DEleteRight
        {
            get { return DeleteRight; }
            set { DeleteRight = value; }
        }
        public bool PRintRight
        {
            get { return PrintRight; }
            set { PrintRight = value; }
        }
        public FormPSP_VolumeBalance(PowerEachList fr)
        {
            InitializeComponent();

            //this.barSub1 = new DevExpress.XtraBars.BarSubItem();
            this.barA1 = new DevExpress.XtraBars.BarButtonItem();
            this.barA2 = new DevExpress.XtraBars.BarButtonItem();
            this.barA3 = new DevExpress.XtraBars.BarButtonItem();
            this.barA4 = new DevExpress.XtraBars.BarButtonItem();
            this.barA5 = new DevExpress.XtraBars.BarButtonItem();
            this.barA6 = new DevExpress.XtraBars.BarButtonItem();
            this.barA7 = new DevExpress.XtraBars.BarButtonItem();
            this.barA8 = new DevExpress.XtraBars.BarButtonItem();
            this.barA9 = new DevExpress.XtraBars.BarButtonItem();
            this.barA10 = new DevExpress.XtraBars.BarButtonItem();
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
                 //this.barSub1,
            this.barA1,
            this.barA2,
            this.barA3,
            this.barA4,
            this.barA5,
            this.barA6,
            this.barA7,
            this.barA8,
            this.barA9,
          this.barA10
            });
            forecastReport = fr;
            Flag = fr.UID;
            if (fr.Types.Contains("110"))
            {
                string s = Guid.NewGuid().ToString();
                TypeID = s;
                this.ctrlPSP_VolumeBalance1.Type = "110";
                TypeID = "110";
                this.ctrlPSP_VolumeBalance1.colL1.Caption = "分区综合最高负荷";
                this.ctrlPSP_VolumeBalance1.colL2.Caption = "本区220kV主变35kV、10KV侧可供负荷";
                this.ctrlPSP_VolumeBalance1.colL4.Caption = "本区110kV用户变和110kV及以下小电源等直接供电负荷";
                this.ctrlPSP_VolumeBalance1.Flag = Flag;
                this.ctrlPSP_VolumeBalance1.Volumecalc0 = EnsureVolumecalc().ToString();
                this.ctrlPSP_VolumeBalance1.Loadrate = EnsureVolumecalc().ToString();
                this.ctrlPSP_VolumeBalance1.RefreshData();
              //  this.Show();
                    //bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
                    //barButtonItem1.ImageIndex = 19;
                
            }
            else if (fr.Types.Contains("220"))
            {
                Volumecalc = "Volumecalc220";
               
                this.ctrlPSP_VolumeBalance1.Type = "220";
                this.ctrlPSP_VolumeBalance1.Flag = Flag;
                TypeID = "220";
                this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText = "220千伏变电容量平衡表 ";
                this.ctrlPSP_VolumeBalance1.colL1.Caption = "综合最高负荷";
                this.ctrlPSP_VolumeBalance1.colL2.Caption = "直接供电负荷";
                this.ctrlPSP_VolumeBalance1.colL4.Caption = "外网供电";
                //this.ctrlPSP_VolumeBalance1.colL3.Visible = true;
                //this.ctrlPSP_VolumeBalance1.colL4.VisibleIndex=4;
                this.ctrlPSP_VolumeBalance1.colL4.Visible = false;
                this.ctrlPSP_VolumeBalance1.colL5.Caption = "需220kV降压供电负荷";
                this.ctrlPSP_VolumeBalance1.colL6.Caption = "现有220kV降压变电容量";
                this.ctrlPSP_VolumeBalance1.colL7.Caption = "220kV容载比";
                this.ctrlPSP_VolumeBalance1.colL8.Caption = "需220kV变电容量(容载比)";
                this.ctrlPSP_VolumeBalance1.colL14.Visible = false;
                this.ctrlPSP_VolumeBalance1.colL15.Visible = false;
                this.ctrlPSP_VolumeBalance1.colL16.Visible = false;
                this.ctrlPSP_VolumeBalance1.Volumecalc0 = EnsureVolumecalc().ToString();
                this.ctrlPSP_VolumeBalance1.RefreshData();
             //   this.Show();
                //bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
                //barButtonItem1.ImageIndex = 19;


            }
            else if (fr.Types.Contains("35"))
            {
                Volumecalc = "Volumecalc35";
                this.ctrlPSP_VolumeBalance1.Type = "35";
                this.ctrlPSP_VolumeBalance1.Flag = Flag;
                TypeID = "35";
                loadrate = "loadrate35";
                this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText = "220千伏变电容量平衡表 ";
                this.ctrlPSP_VolumeBalance1.colL1.Caption = "本区负荷";
                this.ctrlPSP_VolumeBalance1.colL2.Caption = "本地平衡负荷";
                //this.ctrlPSP_VolumeBalance1.colL4.Caption = "外网供电";
                //this.ctrlPSP_VolumeBalance1.colL3.Visible = true;
                //this.ctrlPSP_VolumeBalance1.colL4.VisibleIndex=4;
                this.ctrlPSP_VolumeBalance1.colL4.Visible = false;
                this.ctrlPSP_VolumeBalance1.colL5.Caption = "需要35千伏变电电力";
                this.ctrlPSP_VolumeBalance1.colL6.Caption = "现有35千伏变电容量";
                this.ctrlPSP_VolumeBalance1.colL7.Caption = "本区容载比";
                this.ctrlPSP_VolumeBalance1.colL8.Caption = "需要35千伏变电容量(容载比2.1)";
                this.ctrlPSP_VolumeBalance1.colL9.Caption = "本区35千伏变电容量盈亏";
                this.ctrlPSP_VolumeBalance1.colL12.Caption = "35千伏变电容量合计";
                //this.ctrlPSP_VolumeBalance1.colL14.Visible = false;
                this.ctrlPSP_VolumeBalance1.colL16.Visible = false;
                this.ctrlPSP_VolumeBalance1.Volumecalc0 = EnsureVolumecalc().ToString();
                this.ctrlPSP_VolumeBalance1.Loadrate = Ensureloadrate().ToString();
                this.ctrlPSP_VolumeBalance1.RefreshData();
             //   this.Show();
               
            }
            if (forecastReport.ListName.Contains(TypeID + "千伏变电容量平衡表"))
            {
                //this.Text = forecastReport.ListName;
                this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText = forecastReport.ListName;
            }
            else
            {
                //this.Text = forecastReport.ListName + TypeID + "千伏变电容量平衡表";
                this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText = forecastReport.ListName + TypeID + "千伏变电容量平衡表";
            }

            //this.bar.InsertItem(bar.ItemLinks[4], barSub1);

            //// 
            //// barSub1
            //// 
            //this.barSub1.Caption = "下级维护";
            //this.barSub1.Id = 280;
            //this.barSub1.ImageIndex = 31;

            //this.barSub1.Name = "barSub1";
            //this.barSub1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            //// 
      
            // 
            // barA3
            // 
           
            //if (TypeID == "110")
            //{
            //    barA1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            //}
            //this.barSub1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            //new DevExpress.XtraBars.LinkPersistInfo(this.barA1),
            //new DevExpress.XtraBars.LinkPersistInfo(this.barA2),
            //new DevExpress.XtraBars.LinkPersistInfo(this.barA3)});
        }
        private void Hidbar()
        {
            if (!AddRight)
            {
                barA6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                
            
            }
            if (!EditRight)
            {
                barA7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barA1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barA2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barA10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.ctrlPSP_VolumeBalance1.AllowUpdate = false;
            }
            if (!DeleteRight)
            {
                barA8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
               

            }
            if (!PrintRight)
            {
                barA9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


            }
        
        }
        private void FormPSP_VolumeBalance_Load(object sender, EventArgs e)
        {
        
            //bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
            //barButtonItem1.ImageIndex = 19;
            //this.ctrlPSP_VolumeBalance1.Type = "110";
            //TypeID = "110";
            //this.ctrlPSP_VolumeBalance1.RefreshData();
            // barA1
            // 

            
            this.barA1.Caption = "取最高负荷";
            this.barA1.Id = 29;
            this.barA1.ImageIndex = 20;
            this.barA1.Name = "barA1";
            this.barA1.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA1_ItemClick);
            //// 
            //// barA2
            //// 
            this.barA2.Caption = "设定容载比";
            this.barA2.Id = 30;
            this.barA2.ImageIndex = 23;
            this.barA2.Name = "barA2";
            this.barA2.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA2.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA2_ItemClick);
            this.barA3.Caption = "直接供电负荷明细";
            this.barA3.Id = 31;
            this.barA3.ImageIndex =19;
            this.barA3.Name = "barA3";
            this.barA3.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA3.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA3_ItemClick);
            this.barA4.Caption = "已立项目";
            this.barA4.Id = 32;
            this.barA4.ImageIndex = 14;
            this.barA4.Name = "barA4";
            this.barA4.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA4.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA4_ItemClick);
            this.barA5.Caption = "新增项目";
            this.barA5.Id = 33;
            this.barA5.ImageIndex = 17;
            this.barA5.Name = "barA5";
            this.barA5.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA5_ItemClick);
            this.barA6.Caption = "添加";
            this.barA6.Id =1;
            this.barA6.ImageIndex = 0;
            this.barA6.Name = "barA6";
            this.barA6.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA6.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA6_ItemClick);
            this.barA7.Caption = "修改";
            this.barA7.Id = 2;
            this.barA7.ImageIndex = 1;
            this.barA7.Name = "barA7";
            this.barA7.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA7.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA7_ItemClick);
            this.barA8.Caption = "删除";
            this.barA8.Id = 3;
            this.barA8.ImageIndex =2;
            this.barA8.Name = "barA8";
            this.barA8.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA8.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA8_ItemClick);
            this.barA9.Caption = "打印";
            this.barA9.Id = 4;
            this.barA9.ImageIndex = 3;
            this.barA9.Name = "barA9";
            this.barA9.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA9.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA9_ItemClick);
            this.barA10.Caption = "设定负荷同时率";
            this.barA10.Id = 4;
            this.barA10.ImageIndex = 21;
            this.barA10.Name = "barA10";
            this.barA10.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            this.barA10.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barA10_ItemClick);
            this.bar.InsertItem(bar.ItemLinks[4], barA1);
            this.bar.InsertItem(bar.ItemLinks[5], barA2);
            this.bar.InsertItem(bar.ItemLinks[6], barA10);
            this.bar.InsertItem(bar.ItemLinks[7], barA3);
            this.bar.InsertItem(bar.ItemLinks[8], barA4);
            this.bar.InsertItem(bar.ItemLinks[9], barA5);
            this.bar.InsertItem(bar.ItemLinks[0], barA6);
            this.bar.InsertItem(bar.ItemLinks[1], barA7);
            this.bar.InsertItem(bar.ItemLinks[2], barA8);
            this.bar.InsertItem(bar.ItemLinks[3], barA9);
            bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
            barButtonItem1.ImageIndex = 19;

            if(TypeID!="220")
                this.barA3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            if (TypeID != "35")
                this.barA10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barAdd.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barEdit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barDel.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barPrint.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ctrlPSP_VolumeBalance1.ADdRight = AddRight;
            this.ctrlPSP_VolumeBalance1.EDitRight = EditRight;
            this.ctrlPSP_VolumeBalance1.DEleteRight = DeleteRight;
            this.ctrlPSP_VolumeBalance1.PRintRight = PrintRight;
            Hidbar();                                        
        }                                                     
                                                            
        //public void VolumeBalance_110()
        //{

            
        //    this.ctrlPSP_VolumeBalance1.Type = "110";
        //    TypeID = "110";
        //    this.ctrlPSP_VolumeBalance1.Volumecalc0 = EnsureVolumecalc().ToString();
        //    this.ctrlPSP_VolumeBalance1.RefreshData();
        //    this.Show();
        //    bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
        //    barButtonItem1.ImageIndex = 19;
        //}
        //public void VolumeBalance_220()
        //{
        //    Volumecalc = "Volumecalc220";
        //    this.ctrlPSP_VolumeBalance1.Type = "220";
        //    TypeID = "220";
        //    this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText = "220千伏变电容量平衡表 ";
        //    this.ctrlPSP_VolumeBalance1.colL1.Caption = "综合最高负荷";
        //    this.ctrlPSP_VolumeBalance1.colL2.Caption = "直接供电负荷";
        //    this.ctrlPSP_VolumeBalance1.colL4.Caption = "外网供电";
        //    //this.ctrlPSP_VolumeBalance1.colL3.Visible = true;
        //    //this.ctrlPSP_VolumeBalance1.colL4.VisibleIndex=4;
        //    //this.ctrlPSP_VolumeBalance1.colL4.Visible = false;
        //      this.ctrlPSP_VolumeBalance1.colL5.Caption="需220kV降压供电负荷";
        //      this.ctrlPSP_VolumeBalance1.colL6.Caption="现有220kV降压变电容量";
        //      this.ctrlPSP_VolumeBalance1.colL7.Caption="220kV容载比";
        //      this.ctrlPSP_VolumeBalance1.colL8.Caption="需220kV变电容量(容载比)";
        //      this.ctrlPSP_VolumeBalance1.colL14.Visible = false;
        //      this.ctrlPSP_VolumeBalance1.Volumecalc0 = EnsureVolumecalc().ToString();
        //      this.ctrlPSP_VolumeBalance1.RefreshData();
        //      this.Show();
        //      bar.InsertItem(bar.ItemLinks[4], barButtonItem1);
        //      barButtonItem1.ImageIndex = 19;
        //}
        private void barA6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            this.ctrlPSP_VolumeBalance1.AddObject();
        }

        private void barA7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance1.UpdateObject();
        }

        private void barA8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance1.DeleteObject();
        }

        private void barA9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ctrlPSP_VolumeBalance1.PrintPreview();
        }
        #region 统计
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PSP_VolumeBalance vol = new PSP_VolumeBalance();
            vol.TypeID = TypeID;
            vol.Flag = Flag;
            IList<PSP_VolumeBalance> listTypes = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", vol);
            FormChooseYears frm = new FormChooseYears();
            foreach (PSP_VolumeBalance year in listTypes)
                frm.ListYearsForChoose.Add(year.Year);
           
            frm.NoIncreaseRate = true;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                int k = 0;
                PSP_VolumeBalance vb = new PSP_VolumeBalance();
                vb.TypeID = TypeID;
                vb.Flag = Flag;
                IList<PSP_VolumeBalance> pspvb = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", vb);
                string str = "";
                PSP_VolumeBalance_Calc vbc = new PSP_VolumeBalance_Calc();
                try
                {
                    vbc.Flag = pspvb[0].UID;
                }
                catch { }
                vbc.Type = "1";
                IList<PSP_VolumeBalance_Calc> vbclist = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbc);
  
                DataTable dt = new DataTable();
                //dt.Columns.Add("Sort", typeof(int));
                dt.Columns.Add("序号", typeof(int));
                dt.Columns.Add("项目", typeof(string));

                int isort = 0;
                if (TypeID == "220")
                {
                    foreach (GridColumn i in ctrlPSP_VolumeBalance1.GridView.Columns)
                    {
                        if (i.Caption != "年度" && i.Caption != "小电厂需备用容量" && i.Caption != "外网供电" && i.FieldName != "S2" && i.FieldName != "S3")
                        {
                            if (i.FieldName == "S4")
                                i.Caption = "注：";
                            if (isort == 2)
                            {
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist)
                                {
                                    DataRow drow = dt.NewRow();
                                   
                                    //drow["序号"] = isort + 1;
                                    drow["项目"] = pspvbc.Title;
                                    dt.Rows.Add(drow);

                                    DataRow drow1 = dt.NewRow();
                                    drow1["项目"] = "注：";
                                    dt.Rows.Add(drow1);
                                }

                                isort++;
                                k = isort + vbclist.Count;
                            }
                            else if (isort == 10)
                            {
                                DataRow drow = dt.NewRow();
                                //drow["序号"] = isort + 1;
                                drow["项目"] = "注：";
                                dt.Rows.Add(drow);
                                isort++;
                            }
                            else if (isort == 12)
                            {
                                DataRow drow = dt.NewRow();
                                //drow["序号"] = isort + 1;
                                drow["项目"] = "注：";
                                dt.Rows.Add(drow);
                                isort++;
                            }
                            DataRow dr = dt.NewRow();
                            //dr["序号"] = isort + 1;
                            dr["项目"] = i.Caption;
                            //dr["Sort"] = isort + 1;
                            dt.Rows.Add(dr);
                            isort++;
                        }
                    }
                }
                if (TypeID == "35")
                {
                    foreach (GridColumn i in ctrlPSP_VolumeBalance1.GridView.Columns)
                    {
                        if (i.Caption != "年度" && i.Caption != "小电厂需备用容量" && i.Caption != "110kV及以下小电源直接供电负荷" && i.FieldName != "S3")
                        {
                            if(i.FieldName== "S2"|| i.FieldName == "S4")
                                i.Caption = "注：";
                            if (isort == 10)
                            {
                                DataRow drow = dt.NewRow();
                                //drow["序号"] = isort + 1;
                                drow["项目"] = "注：";
                                dt.Rows.Add(drow);
                                isort++;
                            }
                            else if (isort == 12)
                            {
                                DataRow drow = dt.NewRow();
                                //drow["序号"] = isort + 1;
                                drow["项目"] = "注：";
                                dt.Rows.Add(drow);
                                isort++;
                            }
                            DataRow dr = dt.NewRow();
                            //dr["序号"] = isort + 1;
                            
                            dr["项目"] = i.Caption;
                            if (dr["项目"]!=null)
                            if (dr["项目"].ToString().Contains("(负荷同时率" + Ensureloadrate() + ")"))
                                 dr["项目"]=dr["项目"].ToString().Replace("(负荷同时率" + Ensureloadrate() + ")", "");
                            //dr["Sort"] = isort + 1;
                            dt.Rows.Add(dr);
                            isort++;
                        }
                    }
                }
                if (TypeID == "110")
                {
                    foreach (GridColumn i in ctrlPSP_VolumeBalance1.GridView.Columns)
                    {
                        if (i.Caption != "年度" && i.Caption != "小电厂需备用容量" )
                        {
                            if (i.FieldName == "S2" || i.FieldName == "S4" || i.FieldName == "S3")
                                i.Caption = "注：";
                            if (isort == 12)
                            {
                                DataRow drow = dt.NewRow();
                                //drow["序号"] = isort + 1;
                                drow["项目"] = "注：";
                                dt.Rows.Add(drow);
                                isort++;
                            }
                            else if (isort == 14)
                            {
                                DataRow drow = dt.NewRow();
                                //drow["序号"] = isort + 1;
                                drow["项目"] = "注：";
                                dt.Rows.Add(drow);
                                isort++;
                            }
                            DataRow dr = dt.NewRow();
                            //dr["序号"] = isort + 1;
                            dr["项目"] = i.Caption;
                            //dr["Sort"] = isort + 1;
                            dt.Rows.Add(dr);
                            isort++;
                        }
                    }
                }
                isort = 1;
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    row["序号"] = isort;
                    isort++;
                }

                foreach (ChoosedYears i in frm.ListChoosedYears)
                {
                    dt.Columns.Add(i.Year + "年", typeof(string));
                }
                //if (TypeID == "220")
                //{
                //    dt.Rows.RemoveAt(vbclist.Count + 14);
                //}
                foreach (ChoosedYears i in frm.ListChoosedYears)
                {
                 
                    foreach (PSP_VolumeBalance balan in pspvb)
                    {
                        if (balan.Year == i.Year)
                        {
                            int j = 0;
                            dt.Rows[0][i.Year + "年"] = balan.L1;
                            dt.Rows[1][i.Year + "年"] = balan.L2;
                            PSP_VolumeBalance_Calc vbcalc = new PSP_VolumeBalance_Calc();
                            vbcalc.Flag = balan.UID;
                            vbcalc.Type = "1";
                            IList<PSP_VolumeBalance_Calc> vbclist1 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
  
                            if (TypeID == "220")
                            {
                              
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist1)
                                {
                                    dt.Rows[j+2][i.Year + "年"] = pspvbc.Vol.ToString();
                                    j++;
                                    dt.Rows[j + 2][i.Year + "年"] = pspvbc.Col2.ToString();
                                    j++;
                                }
                                dt.Rows[j + 2][i.Year + "年"] = balan.L5;
                                dt.Rows[j + 3][i.Year + "年"] = balan.S4;
                                dt.Rows[j + 4][i.Year + "年"] = balan.L6;
                                dt.Rows[j + 5][i.Year + "年"] = balan.L7;
                                dt.Rows[j + 6][i.Year + "年"] = balan.L8;
                                dt.Rows[j + 7][i.Year + "年"] = balan.L9;
                                dt.Rows[j + 8][i.Year + "年"] = balan.L10;

                                str = "";
                                vbcalc.Type = "2";
                                IList<PSP_VolumeBalance_Calc> vbclist2 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist2)
                                {
                                    if (pspvbc.LX1 == "新建")
                                        str += pspvbc.LX1 + "" + pspvbc.Title + "" + pspvbc.LX2;
                                    else
                                        str += pspvbc.Title + "" + pspvbc.LX1 + "" + pspvbc.LX2;
                                    if (pspvbc.UID != vbclist2[vbclist2.Count - 1].UID)
                                        str += ",";

                                }
                                dt.Rows[j + 9][i.Year + "年"] = str;
                                dt.Rows[j + 10][i.Year + "年"] = balan.L11;
                                str = "";
                                vbcalc.Type = "3";
                                IList<PSP_VolumeBalance_Calc> vbclist3 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist3)
                                {
                                    if (pspvbc.LX1 == "新建")
                                        str += pspvbc.LX1 + "" + pspvbc.Title + "" + pspvbc.LX2;
                                    else
                                        str += pspvbc.Title + "" + pspvbc.LX1 + "" + pspvbc.LX2;
                                    if (pspvbc.UID != vbclist3[vbclist3.Count - 1].UID)
                                        str += ",";
                                }
                                dt.Rows[j + 11][i.Year + "年"] = str;
                                dt.Rows[j + 12][i.Year + "年"] = balan.L12;
                                dt.Rows[j + 13][i.Year + "年"] = balan.L13;
                                dt.Rows[j + 14][i.Year + "年"] = balan.L14;
                            }
                            else if (TypeID == "110")
                            {
                                //foreach (PSP_VolumeBalance_Calc pspvbc in vbclist1)
                                //{
                                //    str = "(" + pspvbc.Title + "," + pspvbc.LX1 + "," + pspvbc.LX2 + ")  " + str;
                                //}
                                dt.Rows[2][i.Year + "年"] = balan.S2;

                                dt.Rows[j + 3][i.Year + "年"] = balan.L4;
                                dt.Rows[j + 4][i.Year + "年"] = balan.S3;
                                dt.Rows[j + 5][i.Year + "年"] = balan.L5;
                                dt.Rows[j + 6][i.Year + "年"] = balan.S4;
                                dt.Rows[j + 7][i.Year + "年"] = balan.L6;
                                dt.Rows[j + 8][i.Year + "年"] = balan.L7;
                                dt.Rows[j + 9][i.Year + "年"] = balan.L8;
                                dt.Rows[j + 10][i.Year + "年"] = balan.L9;
                                dt.Rows[j + 11][i.Year + "年"] = balan.L10;

                                str = "";
                                vbcalc.Type = "2";
                                IList<PSP_VolumeBalance_Calc> vbclist2 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist2)
                                {
                                    if (pspvbc.LX1 == "新建")
                                        str += pspvbc.LX1 + "" + pspvbc.Title + "" + pspvbc.LX2;
                                    else
                                        str += pspvbc.Title + "" + pspvbc.LX1 + "" + pspvbc.LX2;
                                    if (pspvbc.UID != vbclist2[vbclist2.Count - 1].UID)
                                        str += ",";

                                }
                                dt.Rows[j + 12][i.Year + "年"] = str;
                                dt.Rows[j + 13][i.Year + "年"] = balan.L11;

                                str = "";
                                vbcalc.Type = "3";
                                IList<PSP_VolumeBalance_Calc> vbclist3 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist3)
                                {
                                    if (pspvbc.LX1 == "新建")
                                        str += pspvbc.LX1 + "" + pspvbc.Title + "" + pspvbc.LX2;
                                    else
                                        str += pspvbc.Title + "" + pspvbc.LX1 + "" + pspvbc.LX2;
                                    if (pspvbc.UID != vbclist3[vbclist3.Count - 1].UID)
                                        str += ",";
                                }
                                dt.Rows[j + 14][i.Year + "年"] = str;
                                dt.Rows[j + 15][i.Year + "年"] = balan.L12;
                                dt.Rows[j + 16][i.Year + "年"] = balan.L13;
                                dt.Rows[j + 17][i.Year + "年"] = balan.L14;
                            }
                            else if (TypeID == "35")
                            {
                                dt.Rows[j+2][i.Year + "年"] = balan.S2;
                                dt.Rows[j + 3][i.Year + "年"] = balan.L5;
                                dt.Rows[j + 4][i.Year + "年"] = balan.S4;
                                dt.Rows[j + 5][i.Year + "年"] = balan.L6;
                                dt.Rows[j + 6][i.Year + "年"] = balan.L7;
                                dt.Rows[j + 7][i.Year + "年"] = balan.L8;
                                dt.Rows[j + 8][i.Year + "年"] = balan.L9;
                                dt.Rows[j + 9][i.Year + "年"] = balan.L10;
                                //dt.Rows[j + 8][i.Year + "年"] = balan.L10;
                                str = "";
                                vbcalc.Type = "2";
                                IList<PSP_VolumeBalance_Calc> vbclist2 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist2)
                                {
                                    if (pspvbc.LX1 == "新建")
                                        str += pspvbc.LX1 + "" + pspvbc.Title + "" + pspvbc.LX2;
                                    else
                                        str += pspvbc.Title + "" + pspvbc.LX1 + "" + pspvbc.LX2;
                                    if (pspvbc.UID != vbclist2[vbclist2.Count - 1].UID)
                                        str += ",";

                                }
                                dt.Rows[j + 10][i.Year + "年"] = str;
                                dt.Rows[j + 11][i.Year + "年"] = balan.L11;
                                str = "";
                                vbcalc.Type = "3";
                                IList<PSP_VolumeBalance_Calc> vbclist3 = Services.BaseService.GetList<PSP_VolumeBalance_Calc>("SelectPSP_VolumeBalance_CalcByTypeFlag", vbcalc);
                                foreach (PSP_VolumeBalance_Calc pspvbc in vbclist3)
                                {
                                    if (pspvbc.LX1 == "新建")
                                        str += pspvbc.LX1 + "" + pspvbc.Title + "" + pspvbc.LX2;
                                    else
                                        str += pspvbc.Title + "" + pspvbc.LX1 + "" + pspvbc.LX2;
                                    if (pspvbc.UID != vbclist3[vbclist3.Count - 1].UID)
                                        str += ",";
                                }
                                dt.Rows[j + 12][i.Year + "年"] = str;
                                dt.Rows[j + 13][i.Year + "年"] = balan.L12;
                                dt.Rows[j + 14][i.Year + "年"] = balan.L13;

                            }                          
                        }
                    }

                }

                FormPSP_VolumeBalance_Print fr = new FormPSP_VolumeBalance_Print();
                fr.GridDataTable = dt;
                //if (TypeID == "220")
                //{
                    fr.Text = this.Text;
                    fr.gridView1.GroupPanelText = this.Text;
                    fr.PRintRight = PrintRight;
                //}
                fr.ShowDialog();
            }
        }
        #endregion
        private void barA1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool issum = true;
            PSP_VolumeBalance sumobj = new PSP_VolumeBalance();

            Form1_Fs_VolumeBalance frm = new Form1_Fs_VolumeBalance();
                frm.Text = "选择最高负荷";
                frm.TYPEFLAG2 = "2";
                //frm.ShowDialog();
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    IList<PSP_VolumeBalance> list;
                    if (this.ctrlPSP_VolumeBalance1.GridControl.DataSource != null && this.ctrlPSP_VolumeBalance1.GridControl.DataSource != DBNull.Value)
                    {
                        list = this.ctrlPSP_VolumeBalance1.GridControl.DataSource as IList<PSP_VolumeBalance>;
                        foreach (PSP_VolumeBalance pspvol in list)
                            foreach (TreeListColumn tlc in frm.TLN.TreeList.Columns)
                            {
                                if (tlc.FieldName.IndexOf("年") < 0)
                                    continue;

                                if (Convert.ToInt32(tlc.FieldName.Replace("年","")) == pspvol.Year)
                                {

                                    try
                                    {
                                        pspvol.L1 = Math.Round(Convert.ToDouble(frm.TLN[tlc.FieldName]),2);
                                        Services.BaseService.Update<PSP_VolumeBalance>(pspvol);
                                        if (issum)
                                        {
                                            issum = false;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        // MessageBox.Show(ex.ToString());
                                    }
                                }
                            }

                        if (!issum)
                        {
                           foreach(PSP_VolumeBalance vol in list)
                               if (this.ctrlPSP_VolumeBalance1.EnsureBaseYear() > 1900)
                               {
                                   if (vol.Year < this.ctrlPSP_VolumeBalance1.EnsureBaseYear())
                                   {
                                       this.ctrlPSP_VolumeBalance1.reloadsum(vol);

                                   }
                                   else if (vol.Year == this.ctrlPSP_VolumeBalance1.EnsureBaseYear())
                                   {
                                       this.ctrlPSP_VolumeBalance1.reloadsum(vol);

                                   } 
                                   
                               }
                               else
                               {
                                   this.ctrlPSP_VolumeBalance1.reloadsum(vol);
                               }
                        }
                    }
                }
            
        }
        private void barA2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DevExpress.XtraBars.BarItem bot = (DevExpress.XtraBars.BarItem)sender;
            FormPSP_VolumeBalanceVolumecalc frm = new FormPSP_VolumeBalanceVolumecalc();
            
            frm.Type = TypeID;
            //if (bot.Caption == "设定容载比")
            //{
                frm.Text = "设定容载比";
                frm.CtrTitle = "容载比";
                frm.YearValue = EnsureVolumecalc();
                frm.UID = Volumecalc;
            //}
            //else
            //    if (bot.Caption== "设定负荷同时率")
            //    {
            //        frm.Text = "设定负荷同时率";
            //        frm.CtrTitle = "负荷同时率";
            //        frm.YearValue =Ensureloadrate();
            //        frm.UID = loadrate;
            //    }
            frm.Flag = Flag;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.ctrlPSP_VolumeBalance1.Volumecalc0 = EnsureVolumecalc().ToString();
                this.ctrlPSP_VolumeBalance1.colL8.Caption = "需" + TypeID + "kV变电容量(容载比" + EnsureVolumecalc() + ")";
                //if(TypeID=="35")
                //    this.ctrlPSP_VolumeBalance1.colL5.Caption = "需" + TypeID + "千伏变电电力(负荷同时率" + Ensureloadrate() + ")";
                PSP_VolumeBalance vol = new PSP_VolumeBalance();
                vol.Year = this.ctrlPSP_VolumeBalance1.EnsureBaseYear();
                vol.Flag = Flag;
                vol.TypeID = TypeID;
                if (vol.Year > 1900)
                {
                    IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDLessYear", vol);

                    if (list != null)
                    {
                        foreach (PSP_VolumeBalance pvo in list)
                        {
                            this.ctrlPSP_VolumeBalance1.reloadsum(pvo);
                        }
                    }
                    if (list != null) list.Clear();
                    list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDAndYear", vol);

                    if (list != null)
                    {
                        if (list.Count == 1)
                            this.ctrlPSP_VolumeBalance1.reloadsum(list[0]);
                    }
                }
                else
                {
                    PSP_VolumeBalance voltemp = new PSP_VolumeBalance();
                    voltemp.Flag = Flag;
                    voltemp.TypeID = TypeID;
                    IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", voltemp);
                    foreach(PSP_VolumeBalance pvo in list)
                    {
                        this.ctrlPSP_VolumeBalance1.reloadsum(pvo);
                    }
                }
            }
        }
        private void barA10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //DevExpress.XtraBars.BarItem bot = (DevExpress.XtraBars.BarItem)sender;
            FormPSP_VolumeBalanceVolumecalc frm = new FormPSP_VolumeBalanceVolumecalc();

            frm.Type = TypeID;
            ////if (bot.Caption == "设定容载比")
            ////{
            //frm.Text = "设定容载比";
            //frm.CtrTitle = "容载比";
            //frm.YearValue = EnsureVolumecalc();
            //frm.UID = Volumecalc;
            //}
            //else
            //    if (bot.Caption== "设定负荷同时率")
            //    {
            frm.Text = "设定负荷同时率";
            frm.CtrTitle = "负荷同时率";
            frm.YearValue = Ensureloadrate();
            frm.UID = loadrate;
            //    }
            frm.Flag = Flag;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.ctrlPSP_VolumeBalance1.Loadrate = Ensureloadrate().ToString();
               
                    this.ctrlPSP_VolumeBalance1.colL5.Caption = "需" + TypeID + "千伏变电电力(负荷同时率" + Ensureloadrate() + ")";
                PSP_VolumeBalance vol = new PSP_VolumeBalance();
                vol.Year = this.ctrlPSP_VolumeBalance1.EnsureBaseYear();
                vol.Flag = Flag;
                vol.TypeID = TypeID;
                if (vol.Year > 1900)
                {
                    IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDLessYear", vol);

                    if (list != null)
                    {
                        foreach (PSP_VolumeBalance pvo in list)
                        {
                            this.ctrlPSP_VolumeBalance1.reloadsum(pvo);
                        }
                    }
                    if (list != null) list.Clear();
                    list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeIDAndYear", vol);

                    if (list != null)
                    {
                        if (list.Count == 1)
                            this.ctrlPSP_VolumeBalance1.reloadsum(list[0]);
                    }
                }
                else
                {
                    PSP_VolumeBalance voltemp = new PSP_VolumeBalance();
                    voltemp.Flag = Flag;
                    voltemp.TypeID = TypeID;
                    IList<PSP_VolumeBalance> list = Services.BaseService.GetList<PSP_VolumeBalance>("SelectPSP_VolumeBalanceByTypeID", voltemp);
                    foreach (PSP_VolumeBalance pvo in list)
                    {
                        this.ctrlPSP_VolumeBalance1.reloadsum(pvo);
                    }
                }
            }
        }
     
        private void barA3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (TypeID == "220")
            {
                FrmBalance_Calc FrmBalance = new FrmBalance_Calc();
                FrmBalance.FormTitle = this.ctrlPSP_VolumeBalance1.colL2.Caption;
                FrmBalance.CtrTitle = this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText;
                FrmBalance.ADdRight = AddRight;
                FrmBalance.EDitRight = EditRight;
                FrmBalance.DEleteRight = DeleteRight;
                FrmBalance.PRintRight = PrintRight;
                PSP_VolumeBalance vol = new PSP_VolumeBalance();
                if (this.ctrlPSP_VolumeBalance1.GridView.GetRow(this.ctrlPSP_VolumeBalance1.GridView.FocusedRowHandle) != null)
                {
                    vol = this.ctrlPSP_VolumeBalance1.GridView.GetRow(this.ctrlPSP_VolumeBalance1.GridView.FocusedRowHandle) as PSP_VolumeBalance;


                    FrmBalance.FLAG = vol.UID;
                    FrmBalance.TYPE = "1";

                    FrmBalance.DY = TypeID;



                    if (FrmBalance.FLAG != "")
                    {
                        FrmBalance.ShowDialog();
                        this.ctrlPSP_VolumeBalance1.RefreshData();
                        vol.GetType().GetProperty("L2").SetValue(vol, FrmBalance.SUM, null);
                        this.ctrlPSP_VolumeBalance1.reloadsum(vol);
                    }
                  
                }
            }
        }
        private void barA4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmBalance_Calc FrmBalance = new FrmBalance_Calc();
            FrmBalance.FormTitle = "已立项的变电容量";
            FrmBalance.CtrTitle = this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText;
            FrmBalance.ADdRight = AddRight;
            FrmBalance.EDitRight = EditRight;
            FrmBalance.DEleteRight = DeleteRight;
            FrmBalance.PRintRight = PrintRight;
            PSP_VolumeBalance vol = new PSP_VolumeBalance();
            if (this.ctrlPSP_VolumeBalance1.GridView.GetRow(this.ctrlPSP_VolumeBalance1.GridView.FocusedRowHandle) != null)
            {
                vol = this.ctrlPSP_VolumeBalance1.GridView.GetRow(this.ctrlPSP_VolumeBalance1.GridView.FocusedRowHandle) as PSP_VolumeBalance;

                FrmBalance.FLAG = vol.UID;
                FrmBalance.TYPE = "2";
                FrmBalance.DY = TypeID;

                if (FrmBalance.FLAG != "")
                {
                    FrmBalance.ShowDialog();
                    this.ctrlPSP_VolumeBalance1.RefreshData();
                    vol.GetType().GetProperty("L10").SetValue(vol, FrmBalance.SUM, null);
                    this.ctrlPSP_VolumeBalance1.reloadsum(vol);
                }
            }

        }
        private void barA5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            FrmBalance_Calc FrmBalance = new FrmBalance_Calc();
            FrmBalance.FormTitle = "规划新增变电容量";
            FrmBalance.CtrTitle = this.ctrlPSP_VolumeBalance1.GridView.GroupPanelText;
            FrmBalance.ADdRight = AddRight;
            FrmBalance.EDitRight = EditRight;
            FrmBalance.DEleteRight = DeleteRight;
            FrmBalance.PRintRight = PrintRight;
            PSP_VolumeBalance vol = new PSP_VolumeBalance();
            if (this.ctrlPSP_VolumeBalance1.GridView.GetRow(this.ctrlPSP_VolumeBalance1.GridView.FocusedRowHandle) != null)
            {
                vol = this.ctrlPSP_VolumeBalance1.GridView.GetRow(this.ctrlPSP_VolumeBalance1.GridView.FocusedRowHandle) as PSP_VolumeBalance;


                FrmBalance.FLAG = vol.UID;
                FrmBalance.TYPE = "3";
                FrmBalance.DY = TypeID;


                if (FrmBalance.FLAG != "")
                {
                    FrmBalance.ShowDialog();
                    this.ctrlPSP_VolumeBalance1.RefreshData();
                    vol.GetType().GetProperty("L11").SetValue(vol, FrmBalance.SUM, null);
                    this.ctrlPSP_VolumeBalance1.reloadsum(vol);
                }
            }

        }
        #region 确定容载比
        private double EnsureVolumecalc()
        {
            PSP_VolumeBalanceDataSource BaseYeartemp = new PSP_VolumeBalanceDataSource();
            BaseYeartemp.TypeID = Convert.ToInt32(TypeID);
            BaseYeartemp.Flag = Flag;
            BaseYeartemp.UID = Volumecalc;
            PSP_VolumeBalanceDataSource BaseYearrate = (PSP_VolumeBalanceDataSource)Common.Services.BaseService.GetObject("SelectPSP_VolumeBalanceDataSourceByKeyTypeID", BaseYeartemp);
            if (BaseYearrate == null)
            {
                BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.UID = Volumecalc;
                BaseYearrate.Value = 2;
                BaseYearrate.Flag = Flag;
                BaseYearrate.TypeID = Convert.ToInt32(TypeID);
                Services.BaseService.Create<PSP_VolumeBalanceDataSource>(BaseYearrate);
                //
            }
            return Convert.ToDouble(BaseYearrate.Value);

        }
        #endregion

        #region 确定负荷同时率
        private double Ensureloadrate()
        {
            PSP_VolumeBalanceDataSource BaseYeartemp = new PSP_VolumeBalanceDataSource();
            BaseYeartemp.TypeID = Convert.ToInt32(TypeID);
            BaseYeartemp.Flag = Flag;
            BaseYeartemp.UID = loadrate;
            PSP_VolumeBalanceDataSource BaseYearrate = (PSP_VolumeBalanceDataSource)Common.Services.BaseService.GetObject("SelectPSP_VolumeBalanceDataSourceByKeyTypeID", BaseYeartemp);
            if (BaseYearrate == null)
            {
                BaseYearrate = new PSP_VolumeBalanceDataSource();
                BaseYearrate.UID = loadrate;
                BaseYearrate.Value = 0.9;
                BaseYearrate.Flag = Flag;
                BaseYearrate.TypeID = Convert.ToInt32(TypeID);
                Services.BaseService.Create<PSP_VolumeBalanceDataSource>(BaseYearrate);
                //
            }
            return Convert.ToDouble(BaseYearrate.Value);

        }
        #endregion
    }
}