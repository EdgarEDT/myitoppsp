using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.BaseData;
using Itop.Client.Common;
using Itop.Common;
using System.Diagnostics;

namespace Itop.Client.BaseData
{
    public partial class FrmPs_Volume : Itop.Client.Base.FormModuleBase
    {
        IList<Ps_Volume> list = new List<Ps_Volume>();
        DataTable dt = new DataTable();
            


        public FrmPs_Volume()
        {
            InitializeComponent();
            
            
        }

        private void FrmPs_Volume_Load(object sender, EventArgs e)
        {
            InitData();

            


        }

        protected override void Add()
        {
            //新建对象
            Ps_Volume obj = new Ps_Volume();
            obj.ID = Guid.NewGuid().ToString();
            //执行添加操作
            using (FrmPs_VolumeDialog dlg = new FrmPs_VolumeDialog())
            {
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            InitData();
        }

        protected override void Edit()
        {
            DevExpress.XtraGrid.Columns.GridColumn gc = this.gridView1.FocusedColumn;
            if (gc.FieldName.IndexOf("Y") < 0)
                return;

            Ps_Volume obj=null;
            foreach (Ps_Volume pv in list)
            {
                if (pv.Years == Convert.ToInt32(gc.FieldName.Replace("Y", "")))
                { obj = pv; break; }

            }
            if (obj == null)
            {
                return;
            }

            //执行修改操作
            using (FrmPs_VolumeDialog dlg = new FrmPs_VolumeDialog())
            {
                dlg.Object = obj;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            InitData();
        }

        protected override void Del()
        {
            DevExpress.XtraGrid.Columns.GridColumn gc = this.gridView1.FocusedColumn;
            if (gc.FieldName.IndexOf("Y") < 0)
                return;

            Ps_Volume obj = null;
            foreach (Ps_Volume pv in list)
            {
                if (pv.Years == Convert.ToInt32(gc.FieldName.Replace("Y", "")))
                { obj = pv; break; }

            }
            if (obj == null)
            {
                return;
            }

            //请求确认
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //执行删除操作
            try
            {
                Services.BaseService.Delete<Ps_Volume>(obj);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }
            InitData();
        }

        protected override void Print()
        {
            ComponentPrint.ShowPreview(this.gridControl1, this.gridView1.GroupPanelText);
        }


        private void InitData()
        {
            list = Services.BaseService.GetStrongList<Ps_Volume>();
            dt.Columns.Clear();
            dt.Rows.Clear();
            this.gridView1.Columns.Clear();
            dt.Columns.Add("S1");
            dt.Columns.Add("S2");

            DevExpress.XtraGrid.Columns.GridColumn gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            DevExpress.XtraGrid.Columns.GridColumn gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            gridColumn1,
            gridColumn2});

            // 
            // gridColumn1
            // 
            gridColumn1.Caption = "序号";
            gridColumn1.FieldName = "S1";
            gridColumn1.Name = "gridColumn1";
            gridColumn1.Visible = true;
            gridColumn1.VisibleIndex = 0;
            gridColumn1.Width = 30;
            gridColumn1.AppearanceCell.Options.UseTextOptions = true;
            gridColumn1.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

            // 
            // gridColumn2
            // 
            gridColumn2.Caption = "项目";
            gridColumn2.FieldName = "S2";
            gridColumn2.Name = "gridColumn2";
            gridColumn2.Visible = true;
            gridColumn2.VisibleIndex = 1;
            gridColumn2.Width = 200;
            gridColumn2.AppearanceHeader.Options.UseTextOptions = true;
            gridColumn2.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;



            foreach (Ps_Volume pv in list)
            {
                DevExpress.XtraGrid.Columns.GridColumn gc = new DevExpress.XtraGrid.Columns.GridColumn();
                gc.Caption = pv.Years + "年";
                gc.FieldName = "Y" + pv.Years;
                gc.Name = "Y" + pv.Years;
                gc.VisibleIndex = pv.Years;
                gc.Width = 100;
                gc.DisplayFormat.FormatString = "n2";
                gc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gc.AppearanceHeader.Options.UseTextOptions = true;
                gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                this.gridView1.Columns.Add(gc);
                dt.Columns.Add("Y" + pv.Years, typeof(double));
            }

            DataRow row1 = dt.NewRow();
            row1["S1"] = "1";
            row1["S2"] = "最大供电负荷需求";

            foreach (Ps_Volume pv in list)
            {
                row1["Y" + pv.Years] = pv.MaxPw;
            }
            dt.Rows.Add(row1);

            DataRow row2 = dt.NewRow();
            row2["S1"] = "2";
            row2["S2"] = "年末装机容量";
            foreach (Ps_Volume pv in list)
            {
                row2["Y" + pv.Years] = pv.WaterVolume + pv.FireVolume;
            }
            dt.Rows.Add(row2);

            DataRow row3 = dt.NewRow();
            row3["S1"] = "";
            row3["S2"] = "　其中：水电装机容量";
            foreach (Ps_Volume pv in list)
            {
                row3["Y" + pv.Years] = pv.WaterVolume;
            }
            dt.Rows.Add(row3);

            DataRow row4 = dt.NewRow();
            row4["S1"] = "";
            row4["S2"] = "　　　　火电装机容量";
            foreach (Ps_Volume pv in list)
            {
                row4["Y" + pv.Years] = pv.FireVolume;
            }
            dt.Rows.Add(row4);

            DataRow row5 = dt.NewRow();
            row5["S1"] = "3";
            row5["S2"] = "装机备用容量";
            //foreach (Ps_Volume pv in list)
            //{
            //    row5["Y" + pv.Years] = 0;
            //}
            dt.Rows.Add(row5);

            DataRow row6 = dt.NewRow();
            row6["S1"] = "";
            row6["S2"] = "　其中：机组容量";
            foreach (Ps_Volume pv in list)
            {
                if (pv.IsWaterFire == "火力")
                    row6["Y" + pv.Years] = pv.FireVolume * pv.IsWaterFirePst;
                else
                    row6["Y" + pv.Years] = pv.WaterVolume * pv.IsWaterFirePst;
            }
            dt.Rows.Add(row6);

            DataRow row7 = dt.NewRow();
            row7["S1"] = "";
            row7["S2"] = "　　　　最大单机容量";
            foreach (Ps_Volume pv in list)
            {
                row7["Y" + pv.Years] = pv.MaxVolume;
            }
            dt.Rows.Add(row7);

            foreach (Ps_Volume pv in list)
            {
                row5["Y" + pv.Years] = Math.Max((double)row6["Y" + pv.Years], (double)row7["Y" + pv.Years]);
            }



            DataRow row8 = dt.NewRow();
            row8["S1"] = "4";
            row8["S2"] = "受阻容量";
            foreach (Ps_Volume pv in list)
            {
                row8["Y" + pv.Years] = pv.balkWaterVolume + pv.balkFireVolume;
            }
            dt.Rows.Add(row8);

            DataRow row9 = dt.NewRow();
            row9["S1"] = "";
            row9["S2"] = "　其中：水电受阻容量";
            foreach (Ps_Volume pv in list)
            {
                row9["Y" + pv.Years] = pv.balkWaterVolume;
            }
            dt.Rows.Add(row9);

            DataRow row10 = dt.NewRow();
            row10["S1"] = "";
            row10["S2"] = "　　　　火电受阻容量";
            foreach (Ps_Volume pv in list)
            {
                row10["Y" + pv.Years] = pv.balkFireVolume;
            }
            dt.Rows.Add(row10);

            DataRow row11 = dt.NewRow();
            row11["S1"] = "5";
            row11["S2"] = "可参加平衡装机容量";
            foreach (Ps_Volume pv in list)
            {
                row11["Y" + pv.Years] = (double)row2["Y" + pv.Years] - (double)row5["Y" + pv.Years] - (double)row8["Y" + pv.Years];
            }
            dt.Rows.Add(row11);

            DataRow row12 = dt.NewRow();
            row12["S1"] = "6";
            row12["S2"] = "可供电力";
            foreach (Ps_Volume pv in list)
            {
                row12["Y" + pv.Years] = (double)row11["Y" + pv.Years] * pv.IsGetPwPst;
            }
            dt.Rows.Add(row12);

            DataRow row13 = dt.NewRow();
            row13["S1"] = "7";
            row13["S2"] = "电网外受电力";
            foreach (Ps_Volume pv in list)
            {
                row13["Y" + pv.Years] = pv.GetPw;
            }
            dt.Rows.Add(row13);

            DataRow row14 = dt.NewRow();
            row14["S1"] = "8";
            row14["S2"] = "电力盈（+）亏（-）平衡";
            foreach (Ps_Volume pv in list)
            {
                row14["Y" + pv.Years] = (double)row12["Y" + pv.Years] + (double)row13["Y" + pv.Years] - (double)row1["Y" + pv.Years];
            }
            dt.Rows.Add(row14);

            DataRow row15 = dt.NewRow();
            row15["S1"] = "9";
            row15["S2"] = "外受电比例";
            foreach (Ps_Volume pv in list)
            {
                if ((double)row1["Y" + pv.Years] != 0 || (double)row1["Y" + pv.Years] != null)
                    row15["Y" + pv.Years] = (double)row14["Y" + pv.Years] / (double)row1["Y" + pv.Years];
                else
                    row15["Y" + pv.Years] = 0;
            }
            dt.Rows.Add(row15);

            this.gridControl1.DataSource = dt;
        
        }
    }
}