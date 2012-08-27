
#region 引用命名空间
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using Itop.Common;
using Itop.Client.Common;
using Itop.Domain.Stutistic;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using System.IO;
using System.Collections;
using Itop.Domain.Graphics;
#endregion

namespace Itop.Client.Table
{
	public partial class CtrlPowerSubstation_Info_SH: UserControl
	{

        private string types1 = "";
        private string flags1 = "";
        public string xmlflag = "";
        public bool editright = true;
        private string Ghflag = "1";
        private IList fu_list = null;
        private List<PSP_PowerSubstation_Info> fu_list_no = new List<PSP_PowerSubstation_Info>(); 
        string[] titlestr = new string[60];
     
        public string Type
        {
            get { return types1; }
            set { types1 = value; }
        }

        public string Flag
        {
            get { return flags1; }
            set { flags1 = value; }
        }


        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        bool isselect = false;


		#region 构造方法
		public CtrlPowerSubstation_Info_SH()
		{
			InitializeComponent();
		}
		#endregion
        /// <summary>
        /// 设置设备显示列
        /// </summary>
        public  void InitColumns()
        {
            GridColumn column = gridView1.Columns.Add();
            column.Caption = "序号";
            column.FieldName = "S20";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电厂名称";
            column.FieldName = "Title";
            column.Width = 100;
            column.VisibleIndex = 1;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电压等级";
            column.FieldName = "S1";
            column.Width = 100;
            column.VisibleIndex = 2;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "总容量";
            column.FieldName = "S2";
            column.Width = 100;
            column.VisibleIndex = 3;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "容量构成";
            column.FieldName = "S18";
            column.Width = 100;
            column.VisibleIndex = 4;
            column.OptionsColumn.AllowEdit = false;

            column = gridView1.Columns.Add();
            column.Caption = "投产年份";
            column.FieldName = "S3";
            column.Width = 100;
            column.VisibleIndex = 5;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "退役年份";
            column.FieldName = "S30";
            column.Width = 100;
            column.VisibleIndex = 6;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "分区名称";
            column.FieldName = "S9";
            column.Width = 100;
            column.VisibleIndex = 7;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "电厂类型";
            column.FieldName = "S10";
            column.Width = 100;
            column.VisibleIndex = 8;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电量";
            column.FieldName = "S11";
            column.Width = 100;
            column.VisibleIndex = 9;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "发电利用小时数";
            column.FieldName = "S12";
            column.Width = 100;
            column.VisibleIndex = 10;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "厂用电量";
            column.FieldName = "S13";
            column.Width = 100;
            column.VisibleIndex = 11;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "是否统调";
            column.FieldName = "S14";
            column.Width = 100;
            column.VisibleIndex = 12;
            column.OptionsColumn.AllowEdit = false;
            column = gridView1.Columns.Add();
            column.Caption = "公用自备";
            column.FieldName = "S8";
            column.Width = 100;
            column.VisibleIndex = 13;
            column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "停产年份";
            //column.FieldName = "L12";
            //column.Width = 100;
            //column.VisibleIndex = 5;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "状态";
            //column.FieldName = "flag_";
            //column.Width = 100;
            //column.VisibleIndex = 6;
            //column.OptionsColumn.AllowEdit = false;
            //column = gridView1.Columns.Add();
            //column.Caption = "分区类型";
            //column.FieldName = "S5";
            //column.Width = 100;
            //column.VisibleIndex = 7;
            //column.OptionsColumn.AllowEdit = false;

           



        }
		#region 字段
		protected bool _bAllowUpdate = true;
		#endregion

		#region 公共属性
		/// <summary>
		/// 获取或设置"双击允许修改"标志
		/// </summary>
		public bool AllowUpdate
		{
			get { return _bAllowUpdate; }
			set { _bAllowUpdate = value; }
		}

		/// <summary>
		/// 获取GridControl对象
		/// </summary>
		public GridControl GridControl
		{
			get { return gridControl1; }
		}

		/// <summary>
		/// 获取bandedGridView1对象
		/// </summary>
		public GridView GridView
		{
			get { return gridView1; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<PSP_PowerSubstation_Info> ObjectList
		{
            get { return this.gridControl1.DataSource as IList<PSP_PowerSubstation_Info>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        public PSP_PowerSubstation_Info FocusedObject
		{
            get { return this.gridView1.GetRow(this.gridView1.FocusedRowHandle) as PSP_PowerSubstation_Info; }
		}
		#endregion

		#region 事件处理
		private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            //if (!editright)
            //    return;
            //// 判断"双击允许修改"标志 
            //if (!AllowUpdate)
            //{
            //    return;
            //}

            ////如果鼠标点击在单元格中，则编辑焦点对象。
            //Point point = this.gridControl.PointToClient(Control.MousePosition);
            //if (GridHelper.HitCell(this.bandedGridView1, point))
            //{
            //    UpdateObject();
            //}

		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl1, this.gridView1.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                IList<PSP_PowerSubstation_Info> list = Services.BaseService.GetStrongList<PSP_PowerSubstation_Info>();
                        
				this.gridControl1.DataSource = list;
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false;
			}

			return true;
		}
        public void Is_Fu(bool tempbool)
        {
            if (tempbool)
            {
                this.gridControl1.DataSource = fu_list;
            }
            else
            {
                this.gridControl1.DataSource = fu_list_no;
            }
        }
        public void Calc(string addConn)
        {
            int x5 = 1, x1 = 1, x1z = 1, x2 = 1, x2z = 1, x35 = 1, x35z = 1, x10 = 1, x10z = 1, x6 = 1, x6z = 1, t5 = 0, t2 = 0, t2z = 0, t1 = 0, t1z = 0, t35 = 0, t35z = 0, t10 = 0, t10z = 0, t6 = 0, t6z = 0;
            double h5 = 0, h1 = 0, h1z = 0, h2 = 0, h2z = 0, h35 = 0, h35z = 0, h10 = 0, h10z = 0, h6 = 0, h6z = 0, z5 = 0, z1 = 0, z1z = 0, z2 = 0, z2z = 0, z35 = 0, z35z = 0, z10 = 0, z10z = 0, z6 = 0, z6z = 0; ;
            int index5 = -1, index2 = -1, index2z = -1, index1 = -1, index1z = -1, index35 = -1, index35z = -1, index10 = -1, index10z = -1, index6 = -1, index6z = -1;
            Hashtable table = new Hashtable();
            Hashtable table_500 = new Hashtable();
            Hashtable table_220 = new Hashtable();
            Hashtable table_220z = new Hashtable();
            Hashtable table_35 = new Hashtable();
            Hashtable table_35z = new Hashtable();
            Hashtable table_10 = new Hashtable();
            Hashtable table_10z = new Hashtable();
            Hashtable table_6 = new Hashtable();
            Hashtable table_6z = new Hashtable();
            IList<string> groupList_500 = new List<string>();
            IList<string> groupList_220 = new List<string>();
            IList<string> groupList_220z = new List<string>();
            IList<string> groupList_35 = new List<string>();
            IList<string> groupList_35z = new List<string>();
            IList<string> groupList_10 = new List<string>();
            IList<string> groupList_10z = new List<string>();
            IList<string> groupList_6 = new List<string>();
            IList<string> groupList_6z = new List<string>();
          
            bool five = true, one = true, onez = true, two = true, twoz = true, three = true, threez = true, ten = true, tenz = true, six = true, sixz = true;
            string area = "1@3$5q99z99";
            string area_500 = "1@3$5q99z99";
            string area_220 = "1@3$5q99z99";
            string area_220z = "1@3$5q99z99";
            string area_35 = "1@3$5q99z99";
            string area_35z = "1@3$5q99z99";
            string area_10 = "1@3$5q99z99";
            string area_10z = "1@3$5q99z99";
            string area_6 = "1@3$5q99z99";
            string area_6z = "1@3$5q99z99";
            int j = 0;
            int now = 0;
            string con = "AreaID='" + projectid + "'";// +" and Flag='" + Ghflag + "'";
           // con += addConn;
            con += " order by convert(int,S1) desc,S4,AreaName,CreateDate,convert(int,S5)";
            string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
            titlestr = que;
            //IList list = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoByCon", con);
            IList list = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", con);
            if (!string.IsNullOrEmpty(addConn))
            {
                List<PSP_PowerSubstation_Info> listremove = new List<PSP_PowerSubstation_Info>();
                for (int i = 0; i < list.Count;i++ )
                {
                    if (((PSP_PowerSubstation_Info)list[i]).S3.Length != 4)
                    {
                        listremove.Add((PSP_PowerSubstation_Info)list[i]);
                    }
                    else
                    {
                        if (Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S3)>Convert.ToDouble(addConn))
                        {
                            listremove.Add((PSP_PowerSubstation_Info)list[i]);
                        }
                       
                    }
                }
                for (int i = 0; i < listremove.Count;i++ )
                {
                    list.Remove(listremove[i]);
                }
                for (int i = 0; i < list.Count;i++ )
                {
                    double rl=0; int bts=0;
                    string where = "where projectid='" + Itop.Client.MIS.ProgUID + "'and type='02'and SvgUID='" + ((PSP_PowerSubstation_Info)list[i]).UID + "'";
                    IList<PSPDEV> list1 = Services.BaseService.GetList<PSPDEV>("SelectPSPDEVByCondition", where);
                    foreach (PSPDEV pd in list1)
                    {
                        if (!string.IsNullOrEmpty(pd.OperationYear) && Convert.ToInt32(pd.OperationYear) <= Convert.ToDouble(addConn))
                        {
                            if (!string.IsNullOrEmpty(pd.OperationYear) && !string.IsNullOrEmpty(pd.Date2) && pd.Date2.Length == 4 && !string.IsNullOrEmpty(((PSP_PowerSubstation_Info)list[i]).S29) && !string.IsNullOrEmpty(((PSP_PowerSubstation_Info)list[i]).S30))
                            {
                                if (Convert.ToInt32(pd.OperationYear) >= Convert.ToInt32(((PSP_PowerSubstation_Info)list[i]).S29) && Convert.ToInt32(pd.Date2) <= Convert.ToInt32(((PSP_PowerSubstation_Info)list[i]).S30))
                                {
                                    rl += (double)pd.Burthen;
                                    bts++;
                                }
                            }
                            else
                            {
                                rl += (double)pd.Burthen;
                                bts++;
                            }
                        }
                      
                    }
                    ((PSP_PowerSubstation_Info)list[i]).S2 = rl.ToString();
                  
                }
            }
            string conn = "L1=110";
            // IList groupList = Common.Services.BaseService.GetList("SelectAreaNameGroupByConn", conn);
            IList<string> groupList = new List<string>();
            Hashtable table2 = new Hashtable();
            IList<string> groupList2 = new List<string>();
            string area2 = "1@3$5q99z99";
            for (int i = 0; i < list.Count; i++)
            {
                if (((PSP_PowerSubstation_Info)list[i]).S1 == "500"  )
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_500)
                    {
                        if (!table_500.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_500.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_500.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_500 = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (five)
                    { index5 = i; five = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S3 = x5.ToString();
                    h5 += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z5 += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t5 += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                   // catch { }
                    x5++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "220" && ((PSP_PowerSubstation_Info)list[i]).S8 == "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_220z)
                    {
                        if (!table_220z.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_220z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_220z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                            //  table[((PSP_PowerSubstation_Info)list[i]).AreaName] = i;
                        }
                        area_220z = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (twoz)
                    { index2z = i; twoz = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x2z.ToString();
                    h2z +=Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2) ;
                    //try
                    //{
                    //    z2z += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t2z += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x2z++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "220"&& ((PSP_PowerSubstation_Info)list[i]).S8 != "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_220)
                    {
                        if (!table_220.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_220.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_220.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                            //  table[((PSP_PowerSubstation_Info)list[i]).AreaName] = i;
                        }
                        area_220 = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (two)
                    { index2 = i; two = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x2.ToString();
                    h2 += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z2 += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t2 += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x2++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "110" && ((PSP_PowerSubstation_Info)list[i]).S8 == "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area2)
                    {
                        if (!table2.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table2.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList2.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                            //  table[((PSP_PowerSubstation_Info)list[i]).AreaName] = i;
                        }
                        area2 = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (onez)
                    { index1z = i; onez = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x1z.ToString();
                    h1z +=Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2) ;
                    //try
                    //{
                    //    z1z += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);
                       
                    //}
                    //catch { }
                    //try
                    //{
                      
                    //    t1z += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x1z++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "110" && ((PSP_PowerSubstation_Info)list[i]).S8 != "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area)
                    {
                        if (!table.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                            //  table[((PSP_PowerSubstation_Info)list[i]).AreaName] = i;
                        }
                        area = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                   
                    if (one)
                    { index1 = i; one = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x1.ToString();
                    h1 += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z1 += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);
                       
                    //}
                    //catch { }
                    //try
                    //{
                      
                    //    t1 += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x1++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "35" && ((PSP_PowerSubstation_Info)list[i]).S8 == "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_35z)
                    {
                        if (!table_35z.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_35z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_35z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_35z = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (threez)
                    { index35z = i; threez = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x35z.ToString();
                    h35z += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z35z += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t35z += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x35z++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "35" && ((PSP_PowerSubstation_Info)list[i]).S8 != "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_35)
                    {
                        if (!table_35.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_35.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_35.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_35 = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (three)
                    { index35 = i; three = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x35.ToString();
                    h35 +=Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2) ;
                    //try
                    //{
                    //    z35 += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t35 += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x35++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "10" && ((PSP_PowerSubstation_Info)list[i]).S8 == "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_10z)
                    {
                        if (!table_10z.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_10z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_10z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_10z = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (tenz)
                    { index10z = i; tenz = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20= x10z.ToString();
                    h10z += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z10z += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t10z += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x10z++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "10" && ((PSP_PowerSubstation_Info)list[i]).S8 != "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_10)
                    {
                        if (!table_10.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_10.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_10.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_10 = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (ten)
                    { index10 = i; ten = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x10.ToString();
                    h10 += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z10 += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t10 += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x10++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 == "6" && ((PSP_PowerSubstation_Info)list[i]).S8 == "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_6z)
                    {
                        if (!table_6z.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_6z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_6z.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_6z = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (sixz)
                    { index6z = i; sixz = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x6z.ToString();
                    h6z += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z6z += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t6z += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x6z++;
                }
                else if (((PSP_PowerSubstation_Info)list[i]).S1 =="6"  && ((PSP_PowerSubstation_Info)list[i]).S8 != "专用")
                {
                    if (((PSP_PowerSubstation_Info)list[i]).AreaName != area_6)
                    {
                        if (!table_6.Contains(((PSP_PowerSubstation_Info)list[i]).AreaName))
                        {

                            table_6.Add(((PSP_PowerSubstation_Info)list[i]).AreaName, i);
                            groupList_6.Add(((PSP_PowerSubstation_Info)list[i]).AreaName);
                        }
                        area_6 = ((PSP_PowerSubstation_Info)list[i]).AreaName;
                    }
                    if (six)
                    { index6 = i; six = false; }
                    ((PSP_PowerSubstation_Info)list[i]).S20 = x6.ToString();
                    h6 += Convert.ToDouble(((PSP_PowerSubstation_Info)list[i]).S2);
                    //try
                    //{
                    //    z6 += double.Parse(((PSP_PowerSubstation_Info)list[i]).L5);

                    //}
                    //catch { }
                    //try
                    //{

                    //    t6 += (int)((PSP_PowerSubstation_Info)list[i]).L3;
                    //}
                    //catch { }
                    x6++;
                }
            }
            if (x5 > 1)
            {
                PSP_PowerSubstation_Info info = new PSP_PowerSubstation_Info();
                info.S20 = que[j];
                j++;
                info.Title = "500千伏";
                info.S2 = h5.ToString();
                //info.L5 = z5.ToString();
                //info.L3 = t5;
                info.S11 = "500";
                info.S8 = "no";
                list.Insert(index5, info);//.Add(info);
                now++;
                for (int k = 0; k < groupList_500.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_500[k];
                    conn = "S1='500' and AreaID='" + projectid + "' and  AreaName='" + groupList_500[k] + "'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                    //infok.L5 = ((PSP_PowerSubstation_Info)temList[0]).L5;
                    infok.S1 = "500";
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                    
                   
                    list.Insert(int.Parse(table_500[groupList_500[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x2 > 1)
            {
                PSP_PowerSubstation_Info info2 = new PSP_PowerSubstation_Info();
                info2.S20 = que[j];
                j++;
                info2.Title = "220千伏公变";
                info2.S2 = h2.ToString();
                
                info2.S1 = "220";
                info2.S8 = "no";
                list.Insert(index2 + now, info2);
                now++;
                for (int k = 0; k < groupList_220.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_220[k];
                    conn = "S1='220' and AreaID='" + projectid + "' and  AreaName='" + groupList_220[k] + "'and S8!='专用'" ;
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                   
                    infok.S1 = "220";
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                   
                    list.Insert(int.Parse(table_220[groupList_220[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x2z > 1)
            {
                PSP_PowerSubstation_Info info2z = new PSP_PowerSubstation_Info();
                info2z.S20 = que[j];
                j++;
                info2z.Title = "220千伏专变";
                info2z.S2 = h2z.ToString();
                
                info2z.S1 = "220";
                info2z.S8 = "no";
                list.Insert(index2z + now, info2z);
                now++;

                for (int k = 0; k < groupList_220z.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_220z[k];
                    conn = "S1='220' and AreaID='" + projectid + "' and  AreaName='" + groupList_220z[k] + "' and S8='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                  
                    infok.S1 = "220";
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                    
                   
                    list.Insert(int.Parse(table_220z[groupList_220z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x1 > 1)
            {
                PSP_PowerSubstation_Info info1 = new PSP_PowerSubstation_Info();
                info1.S20 = que[j];
                j++;
                info1.Title = "110千伏公变";
                info1.S2 = h1.ToString();
               
                info1.S1 = "110";
                info1.S8 = "no";
                list.Insert(index1 + now, info1);
                now++;
                for (int k = 0; k < groupList.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList[k];
                    conn = "S1='110' and AreaID='" + projectid + "' and  AreaName='" + groupList[k] + "'  and S8!='专用'" ;
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                    
                    infok.S1 = "110";
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                   
                    list.Insert(int.Parse(table[groupList[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x1z > 1)
            {
                PSP_PowerSubstation_Info info1z = new PSP_PowerSubstation_Info();
                info1z.S20= que[j];
                j++;
                info1z.Title = "110千伏专变";
                info1z.S2 = h1z.ToString();
             
               
                info1z.S1 = "110";
                info1z.S8 = "no";
                list.Insert(index1z + now, info1z);
                now++;

                for (int k = 0; k < groupList2.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20= Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList2[k];
                    conn = "S1='110 'and AreaID='" + projectid + "' and  AreaName='" + groupList2[k] + "' and S8='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                    
                    infok.S1 = "110";
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                    
                    list.Insert(int.Parse(table2[groupList2[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x35 > 1)
            {
                PSP_PowerSubstation_Info info35 = new PSP_PowerSubstation_Info();
                info35.S20 = que[j];
                j++;
                info35.Title = "35千伏公变";
                info35.S2 = h35.ToString();
               
                info35.S1 = "35";
                info35.S4 = "no";
                list.Insert(index35 + now, info35);
                now++;
                for (int k = 0; k < groupList_35.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_35[k];
                    conn = "S1='35' and AreaID='" + projectid + "' and  AreaName='" + groupList_35[k] + "'  and S8!='专用' ";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                   
                    infok.S1 = "35";
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                   
                    list.Insert(int.Parse(table_35[groupList_35[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x35z > 1)
            {
                PSP_PowerSubstation_Info info35z = new PSP_PowerSubstation_Info();
                info35z.S20 = que[j];
                j++;
                info35z.Title = "35千伏专变";
                info35z.S2 = h35z.ToString();
             
                info35z.S2 = "35";
                info35z.S4 = "no";
                list.Insert(index35z + now, info35z);
                now++;

                for (int k = 0; k < groupList_35z.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_35z[k];
                    conn = "S1='35' and AreaID='" + projectid + "' and  AreaName='" + groupList_35z[k] + "' and S8='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                  
                    infok.S1 = "35";
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                  
                    list.Insert(int.Parse(table_35z[groupList_35z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x10 > 1)
            {
                PSP_PowerSubstation_Info info10 = new PSP_PowerSubstation_Info();
                info10.S20 = que[j];
                j++;
                info10.Title = "10千伏公变";
                info10.S2 = h10.ToString();
             
                info10.S1 = "10";
                info10.S8 = "no";
                list.Insert(index10 + now, info10);
                now++;
                for (int k = 0; k < groupList_10.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_10[k];
                    conn = "S1='10' and AreaID='" + projectid + "' and  AreaName='" + groupList_10[k] + "'  and S8!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                   
                    infok.S1 ="10" ;
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                   
                    list.Insert(int.Parse(table_10[groupList_10[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x10z > 1)
            {
                PSP_PowerSubstation_Info info10z = new PSP_PowerSubstation_Info();
                info10z.S20 = que[j];
                j++;
                info10z.Title = "10千伏专变";
                info10z.S2 = h10z.ToString();
                
                info10z.S1 = "10";
                info10z.S8 = "no";
                list.Insert(index10z + now, info10z);
                now++;

                for (int k = 0; k < groupList_10z.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_10z[k];
                    conn = "S1='10' and AreaID='" + projectid + "' and  AreaName='" + groupList_10z[k] + "' and S8='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                  
                    infok.S1 = "10";
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                  
                   
                    list.Insert(int.Parse(table_10z[groupList_10z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x6 > 1)
            {
                PSP_PowerSubstation_Info info6 = new PSP_PowerSubstation_Info();
                info6.S20 = que[j];
                j++;
                info6.Title = "6千伏公变";
                info6.S2 = h6.ToString();
               
                info6.S1 ="6" ;
                info6.S8 = "no";
                list.Insert(index6 + now, info6);
                now++;
                for (int k = 0; k < groupList_6.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_6[k];
                    conn = "S1='6' and AreaID='" + projectid + "' and  AreaName='" + groupList_6[k] + "'  and S8!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                 
                    infok.S1 = "6";
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                   
                    list.Insert(int.Parse(table_6[groupList_6[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x6z > 1)
            {
                PSP_PowerSubstation_Info info6z = new PSP_PowerSubstation_Info();
                info6z.S20= que[j];
                j++;
                info6z.Title = "6千伏专变";
                info6z.S2 = h6z.ToString();
               
                info6z.S1 = "6";
                info6z.S8= "no";
                list.Insert(index6z + now, info6z);
                now++;

                for (int k = 0; k < groupList_6z.Count; k++)
                {
                    PSP_PowerSubstation_Info infok = new PSP_PowerSubstation_Info();
                    infok.S20 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_6z[k];
                    conn = "S1='6' and AreaID='" + projectid + "' and  AreaName='" + groupList_6z[k] + "' and S8='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumPSP_PowerSubstation_InfoByConn", conn);
                    infok.S2 = ((PSP_PowerSubstation_Info)temList[0]).S2;
                 
                    infok.S1 ="6" ;
                    infok.S8 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectPSP_PowerSubstation_InfoListByWhere", conn);
                    
                   
                    list.Insert(int.Parse(table_6z[groupList_6z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            try
            {
                //for (int i = 0; i < list.Count; i++)
                //{
                //    if (((PSP_PowerSubstation_Info)list[i]).L9 != null && (double)((PSP_PowerSubstation_Info)list[i]).L2 != null)
                //    {
                //        double templ9 = (double)((PSP_PowerSubstation_Info)list[i]).L9;
                //        double templ2 = (double)((PSP_PowerSubstation_Info)list[i]).L2;
                //        ((PSP_PowerSubstation_Info)list[i]).L10 = (templ2 == 0 ? 0 : templ9 / templ2) * 100;
                //        PSP_PowerSubstation_Info tempsub = Common.Services.BaseService.GetOneByKey<PSP_PowerSubstation_Info>(((PSP_PowerSubstation_Info)list[i]).UID);
                //        tempsub.L10 = ((PSP_PowerSubstation_Info)list[i]).L10;
                //        Common.Services.BaseService.Update<PSP_PowerSubstation_Info>(tempsub);
                //    }
                //}
            }
            catch (Exception ew)
            {

                MessageBox.Show("计算负载率出错" + ew.Message);
            }
            

            this.gridControl1.DataSource = list;
            fu_list = list;

            list_copy(list, fu_list_no);
            att_list(fu_list_no);

        }
        private void list_copy(IList list1, IList list2)
        {
            for (int i = 0; i < list1.Count; i++)
            {
                list2.Add(list1[i]);
            }
        }

        private void att_list(IList templist)
        {
            for (int i = 0; i < templist.Count; i++)
            {
                if (!string.IsNullOrEmpty(((PSP_PowerSubstation_Info)templist[i]).S20))
                {
                    if (char.IsLower(((PSP_PowerSubstation_Info)templist[i]).S20, 0))
                    {
                        templist.RemoveAt(i);
                        i--;
                    }
                }
               
            }
        }
        public void CalcTotal()
        {
            Calc("");
        }

        public void CalcTotal(string conn)
        {
            Calc(conn);
        }


        /// <summary>
        /// 刷新表格中的数据
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
        public bool RefreshData1()
        {
            try
            {
                 string filepath="";
                 string con = "AreaID='" + projectid + "' order by convert(int,S1) desc,S4,AreaName desc,CreateDate";
                 IList<PSP_PowerSubstation_Info> list = Common.Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectPSP_PowerSubstation_InfoListByWhere", con);
                //IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon",con);
                if(xmlflag=="guihua")
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("PSP_PowerSubstation_InfoGuiHua.xml");
                else
                {
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("PSP_PowerSubstation_InfoLayOut11.xml");
                }
              
                if (File.Exists(filepath))
                {
                    this.gridView1.RestoreLayoutFromXml(filepath);
                }
                this.gridControl1.DataSource = list;
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        public bool RefreshDataOut(string addConn)
        {
            try
            {
                string filepath = "";
                string con = "AreaID='" + projectid + "'";
                con += addConn;
                IList<PSP_PowerSubstation_Info> list = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectSubstation_InfoByCon", con);
                if (xmlflag == "guihua")
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationGuiHua.xml");
                else
                {
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationLayOut11.xml");
                }

                if (File.Exists(filepath))
                {
                    this.gridView1.RestoreLayoutFromXml(filepath);
                }
                PSP_PowerSubstation_Info info = new PSP_PowerSubstation_Info();
                info.Title = "合计";
                //info.L9 = 0.0;
                //info.L3 = 0;
                //for (int i = 0; i < list.Count; i++)
                //{
                   
                //    info.L5 = Convert.ToString(double.Parse(info.L5 == "" ? "0" : info.L5) + double.Parse((list[i] as Substation_Info).L5 == "" ? "0" : (list[i] as Substation_Info).L5));
                //    info.L6 = Convert.ToString(double.Parse(info.L6 == "" ? "0" : info.L6) + double.Parse((list[i] as Substation_Info).L6 == "" ? "0" : (list[i] as Substation_Info).L6));
                //    info.L3 += ((list[i] as Substation_Info).L3 == null ? 0 : (list[i] as Substation_Info).L3);
                //    info.L2 += (list[i] as Substation_Info).L2;
                //    info.L9 += (list[i] as Substation_Info).L9;
                //    info.L14 = Convert.ToString(double.Parse(info.L14 == "" ? "0" : info.L14) + double.Parse((list[i] as Substation_Info).L14 == "" ? "0" : (list[i] as Substation_Info).L14));
                //    info.L13 = Convert.ToString(double.Parse(info.L13 == "" ? "0" : info.L13) + double.Parse((list[i] as Substation_Info).L13 == "" ? "0" : (list[i] as Substation_Info).L13));
                //    info.S9 = Convert.ToString(double.Parse(info.S9 == "" ? "0" : info.S9) + double.Parse((list[i] as Substation_Info).S9 == "" ? "0" : (list[i] as Substation_Info).S9));
                //    info.S10 = Convert.ToString(double.Parse(info.S10 == "" ? "0" : info.S10) + double.Parse((list[i] as Substation_Info).S10 == "" ? "0" : (list[i] as Substation_Info).S10));
                //}
                //info.L10 = (info.L2 == 0 ? 0 : info.L9 / info.L2)*100;
                //info.S6 = Convert.ToString(double.Parse(info.L13) == 0.0 ? 0 : double.Parse(info.L14 == "" ? "0" : info.L14)*100 / double.Parse(info.L13));
                list.Add(info);
                this.gridControl1.DataSource = list;
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }




        public bool RefreshData(string layer, bool isrun, string power)
        {

            IList<PSP_PowerSubstation_Info> lists = new List<PSP_PowerSubstation_Info>();
            try
            {

                Substation_Info ll1 = new Substation_Info();
            //    ll1.AreaID = layer;
            //    ll1.L1 = int.Parse(power);

            //    if (isrun)
            //    {
            //        lists = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectSubstation_InfoByXZ", ll1);
            //    }
            //    else
            //    {

            //        lists = Services.BaseService.GetList<PSP_PowerSubstation_Info>("SelectSubstation_InfoByGH", ll1);
            //    }

            //    this.gridControl1.DataSource = lists;


            //    gridView1.OptionsView.ColumnAutoWidth = true;

                //foreach (GridColumn gc in this.bandedGridView1.Columns)
                //{
                //    gc.Visible = false;
                //    gc.OptionsColumn.ShowInCustomizationForm = false;
                //    if (gc.FieldName == "Title" || gc.FieldName == "L9" || gc.FieldName == "L2" || gc.FieldName == "L1" || gc.FieldName == "L10")
                //    {
                //        gc.Visible = true;
                //        gc.OptionsColumn.ShowInCustomizationForm = true;
                //    }


                //    //if (gc.FieldName.Substring(0, 1) == "S")
                //    //{
                //    //    gc.Visible = false;
                //    //    gc.OptionsColumn.ShowInCustomizationForm = false;
                //    //}
                //}
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

        private string projectid;
        public string ProjectID
        {
            set { projectid = value; }
        }

		/// <summary>
		/// 添加对象
		/// </summary>
		public void AddObject()
		{
			//检查对象链表是否已经加载
            ////if (ObjectList == null)
            ////{
            ////    return;
            ////}
            ////新建对象
            //Substation_Info obj = new Substation_Info();
            //obj.Flag = flags1;
            //obj.CreateDate = DateTime.Now;
            ////obj.L1 = 100;
            ////obj.L2 = 100;
            ////obj.L3 = 100;

            ////执行添加操作
            //using (FrmSubstation_InfoDialog_AHTL dlg = new FrmSubstation_InfoDialog_AHTL())
            //{
            //    dlg.SetVisible();
            //    dlg.Type = types1;
            //    dlg.Flag = flags1;
            //    dlg.ctrlSubstation_Info = this;
            //    dlg.ProjectID = projectid;
            //    dlg.IsCreate = true;    //设置新建标志
            //    dlg.Object = obj;
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
                    
            //        return;
            //    }
            //}
            //this.bandedGridView1.BeginUpdate();
            //CalcTotal();
            ////CalcTotal(" order by convert(int,L1) desc,AreaName desc,S4,CreateDate,convert(int,S5) ");
            //this.bandedGridView1.EndUpdate();
            ////将新对象加入到链表中
            ////ObjectList.Add(obj);

            ////刷新表格，并将焦点行定位到新对象上。
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.bandedGridView1, obj);
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
            ////获取焦点对象
            //Substation_Info obj = FocusedObject;
            //if (obj == null)
            //{
            //    return;
            //}
            //if (obj.S4 == "no")
            //{ MessageBox.Show("此行不能修改!"); return; }
            ////创建对象的一个副本
            //Substation_Info objCopy = new Substation_Info();
            //DataConverter.CopyTo<Substation_Info>(obj, objCopy);

            ////执行修改操作
            //using (FrmSubstation_InfoDialog_AHTL dlg = new FrmSubstation_InfoDialog_AHTL())
            //{
            //    dlg.SetVisible();
            //    dlg.IsSelect = isselect;
            //    dlg.Type = types1;
            //    dlg.Flag = flags1;
            //    dlg.ctrlSubstation_Info = this;
            //    dlg.ProjectID = projectid;
            //    dlg.Object = objCopy;   //绑定副本
            //    if (dlg.ShowDialog() != DialogResult.OK)
            //    {
            //        return;
            //    }
            //}

            ////用副本更新焦点对象
            //DataConverter.CopyTo<Substation_Info>(objCopy, obj);
            //this.bandedGridView1.BeginUpdate();
            //CalcTotal();
            ////刷新表格
            ////gridControl.RefreshDataSource();
            //this.bandedGridView1.EndUpdate();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
            ////获取焦点对象
            //Substation_Info obj = FocusedObject;
            //if (obj == null)
            //{
            //    return;
            //}
            //if (obj.S4 == "no")
            //{ MessageBox.Show("此行不能删除!","删除"); return; }
            ////请求确认
            //if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            //{
            //    return;
            //}

            ////执行删除操作
            //try
            //{
            //    Services.BaseService.Delete<Substation_Info>(obj);
            //}
            //catch (Exception exc)
            //{
            //    Debug.Fail(exc.Message);
            //    HandleException.TryCatch(exc);
            //    return;
            //}

            //this.bandedGridView1.BeginUpdate();
            ////记住当前焦点行索引
            ////int iOldHandle = this.bandedGridView1.FocusedRowHandle;
            //////从链表中删除
            ////ObjectList.Remove(obj);
            //////刷新表格
            ////gridControl.RefreshDataSource();
            //////设置新的焦点行索引
            ////GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
            //CalcTotal();
            //this.bandedGridView1.EndUpdate();
		}
		#endregion

        private void gridControl_Click(object sender, EventArgs e)
        {

        }
        private bool Findstr(string str)
        {
            bool value = false;
            for (int i = 0; i < titlestr.Length; i++)
            {
                if (str==titlestr[i])
                {
                    value = true;
                    break;
                }
            }
            return value;
        }
       
        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
             object dr = this.gridView1.GetRow(e.RowHandle);
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c1 = Color.FromArgb(175, 238, 238);
            Color c2 = Color.FromArgb(175, 238, 238);
            Color c3 = Color.FromArgb(245, 222, 180);
            Color c4 = Color.FromArgb(245, 222, 180);
           
            if (dr == null)
                return;
            int tempsum=0;
            if (gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[0]) != null)
            {
                if (Findstr(gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[0]).ToString()))
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    e.Graphics.FillRectangle(brush, r);
                }
                else if (!int.TryParse(gridView1.GetRowCellValue(e.RowHandle, gridView1.Columns[0]).ToString(),out  tempsum))
                {
                    if (e.CellValue!=null&&gridView1.GetRowCellValue(e.RowHandle,gridView1.Columns[0]).ToString() == e.CellValue.ToString())
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                        e.Graphics.FillRectangle(brush, r);
                    }
                   
                }
            }
          
           
        }
	}
}
