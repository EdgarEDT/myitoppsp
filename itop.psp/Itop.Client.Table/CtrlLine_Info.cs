
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
using Itop.Domain.BaseData;
using Itop.Domain.Layouts;
using System.Collections;
//using Itop.Domain.Layouts;
#endregion

namespace Itop.Client.Table
{
	public partial class CtrlLine_Info : UserControl
	{
        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string flagstype = "";
        public bool editright = true;
        public string FlagsType
        {
            get { return flagstype; }
            set { flagstype = value; }
        }
        public string Type
        {
            get { return types1; }
            set { types1 = value; }
        }

        public string Type2
        {
            get { return types2; }
            set { types2 = value; }
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
		public CtrlLine_Info()
		{
			InitializeComponent();
		}
		#endregion

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
			get { return gridControl; }
		}

		/// <summary>
		/// 获取GridView对象
		/// </summary>
        /// 
        ///DevExpress.XtraGrid.Views.BandedGrid.BandedGridView

        public BandedGridView GridView
		{
			get { return this.bandedGridView2; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<Line_Info> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Line_Info>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public Line_Info FocusedObject
		{
            get { return this.bandedGridView2.GetRow(this.bandedGridView2.FocusedRowHandle) as Line_Info; }
		}
		#endregion

		#region 事件处理
		private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (!editright)
                return;
			// 判断"双击允许修改"标志 
			if (!AllowUpdate)
			{
				return;
			}

			//如果鼠标点击在单元格中，则编辑焦点对象。
			Point point = this.gridControl.PointToClient(Control.MousePosition);
            if (GridHelper.HitCell(this.bandedGridView2, point))
			{
				UpdateObject();
			}

		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
            ComponentPrint.ShowPreview(this.gridControl, this.bandedGridView2.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                IList<Line_Info> list = new List<Line_Info>();
               


                int dd = 0;
                Line_Info li = new Line_Info();
                li.DY = dd;
                li.Flag = flags1;
                string filepath1 = "";
                if (types2 == "66") 
                { list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
                 //filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName("layout1.xml");
              
                }
                 if (types2 == "10")
                { list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy1", li);
                //filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName("layout2.xml");
                }
             
                if (File.Exists(filepath1))
                {
                    this.bandedGridView2.RestoreLayoutFromXml(filepath1);
                }
                string s = " ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                IList list1 = Services.BaseService.GetList("SelectPS_Table_AreaWHByConn", s);
                repositoryItemLookUpEdit1.DisplayMember = "Title";
                repositoryItemLookUpEdit1.NullText = "";
                repositoryItemLookUpEdit1.ValueMember = "ID";
                repositoryItemLookUpEdit1.DataSource = list1;    
               this.gridControl.DataSource = list; 
              


                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
               
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return false;
			}

			return true;
		}

        public bool RefreshData(string type)
        {
            IList<Line_Info> list = new List<Line_Info>();
            string conn = "";
            try
            {
               types1 = type;
                int dd = 0;
                if (types2 == "220")
                { dd = 220; }
                else if (types2 == "66")
                { dd = 66; }
                else if (types2 == "10")
                { dd = 10; }

                Line_Info li = new Line_Info();
                li.DY = dd;
                li.Flag = type;
                string filepath1 = "";
                Econ ec = new Econ();
                if (types2 == "220")
                {
                    conn = "Code=''";
                    list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn_ANTL", conn);
                    CalcTotal(ref list);
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout2";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout2";
                } 
                if (types2 == "66")
                {
                    conn = "AreaID='"+projectid+"'  ";// and Flag='" + type + "'";// ;
                    list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn_ANTL", conn);
                    //CalcTotal(ref list);
                    for (int i = 0; i < list.Count;i++ )
                    {
                        Line_Info lin = list[i];
                        lin.S2 = Convert.ToString (i + 1);
                    }
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");
                    if(FlagsType=="")
                        ec.UID = "RefreshDatalayout1";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout1";
                }
                if (types2 == "10")
                {
                    conn = " DY>0 and 10>=DY  and  Flag='" + type + "'";
                    list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn_ANTL", conn);
                    CalcTotal(ref list);
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType + "RefreshDatalayout2.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout2";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout2";
                }
                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                if (listxml.Count != 0)
                {
                    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                    this.bandedGridView2.RestoreLayoutFromStream(ms);
                }   
                //if (types2 == "66")
                //{ list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li); }
                //else if (types2 == "10")
                //{ list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy1", li); }


                string s = " ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                IList list1 = Services.BaseService.GetList("SelectPS_Table_AreaWHByConn", s);
                repositoryItemLookUpEdit1.DisplayMember = "Title";
                repositoryItemLookUpEdit1.NullText = "";
                repositoryItemLookUpEdit1.ValueMember = "ID";
                repositoryItemLookUpEdit1.DataSource = list1;

                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
                this.gridControl.DataSource = list;
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }
        public bool RefreshData(string type,string con)
        {
            IList<Line_Info> list = new List<Line_Info>();
            string conn = "";
            try
            {
                types1 = type;
                int dd = 0;
                if (types2 == "220")
                { dd = 220; }
                else if (types2 == "66")
                { dd = 66; }
                else if (types2 == "10")
                { dd = 10; }

                Line_Info li = new Line_Info();
                li.DY = dd;
                li.Flag = type;
                string filepath1 = "";
                Econ ec = new Econ();
                if (types2 == "220")
                {
                    conn = "Code='' and Flag='" + type + "'" + con;
                    list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn", conn);
                    CalcTotal(ref list);
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout2";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout2";
                }
                if (types2 == "66")
                {
                    conn = "DY>10 and 10000>=DY and AreaID='" + projectid + "' "+con;// ;
                    list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn", conn);
                    CalcTotal(ref list);
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout1";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout1";
                }
                if (types2 == "10")
                {
                    conn = " DY>0 and 10>=DY " + con;
                    list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByConn", conn);
                    CalcTotal(ref list);
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType + "RefreshDatalayout2.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout2";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout2";
                }
                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                //if (listxml.Count != 0)
                //{
                //    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                //    this.bandedGridView2.RestoreLayoutFromStream(ms);
                //}
                //if (types2 == "66")
                //{ list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li); }
                //else if (types2 == "10")
                //{ list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy1", li); }

                string s = " ProjectID='" + Itop.Client.MIS.ProgUID + "'";
                IList list1 = Services.BaseService.GetList("SelectPS_Table_AreaWHByConn", s);
                repositoryItemLookUpEdit1.DisplayMember = "Title";
                repositoryItemLookUpEdit1.NullText = "";
                repositoryItemLookUpEdit1.ValueMember = "ID";
                repositoryItemLookUpEdit1.DataSource = list1;

                //IList<Line_Info> list = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByFlagDy", li);
                this.gridControl.DataSource = list;
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

        public void CalcTotal(ref IList<Line_Info> list)
        {
            int x5 = 1, x1 = 1, x2 = 1, x1z = 1,x35=1;
            double h5 = 0, h1 = 0, h2 = 0, h1z = 0,h35=0, z5 = 0, z1 = 0, z2 = 0, z1z = 0,z35=0;
            int index5 = -1, index2 = -1, index1 = -1, index1z = -1,index35=-1;
            Hashtable table = new Hashtable();
            bool one = true, five = true, two = true, onez = true,three=true;
            string area = "";
            int j = 0;
            int now = 0;
            string con = "AreaID='" + projectid + "'";
            string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
          //  IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByCon", con);
          //  string conn = "L1=110";
            // IList groupList = Common.Services.BaseService.GetList("SelectAreaNameGroupByConn", conn);
            IList<string> groupList = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if ((list[i]).L1 == 500)
                {
                    if (five)
                    {
                        index5 = i;
                        five = false;
                    }
                    (list[i]).S2 = x5.ToString();
                   // h5 += (list[i]).L2;
                    try
                    {
                        z5 += (list[i]).L5;
                    }
                    catch { }
                    x5++; 
                }
                else if ((list[i]).L1 == 220)
                {
                    if (two)
                    { index2 = i; two = false; }
                    (list[i]).S2 = x2.ToString();
                    //h2 += (list[i]).L2;
                    try
                    {
                        z2 += (list[i]).L5;
                    }
                    catch { }
                    x2++;
                }
                else if ((list[i]).L1 == 110 && (list[i]).S4 == "专用")
                {
                    if (onez)
                    { index1z = i; onez = false; }
                    (list[i]).S2 = x1z.ToString();
                    //h1z += (list[i]).L2;
                    try
                    {
                        z1z += (list[i]).L5;
                    }
                    catch { }
                    x1z++;
                }
                else if ((list[i]).L1 == 110 && (list[i]).S4 != "专用")
                {
                    //if (((Substation_Info)list[i]).AreaName != area)
                    //{
                    //    table.Add(((Substation_Info)list[i]).AreaName, i);
                    //    groupList.Add(((Substation_Info)list[i]).AreaName);
                    //    //  table[((Substation_Info)list[i]).AreaName] = i;
                    //    area = ((Substation_Info)list[i]).AreaName;
                    //}
                    if (one)
                    { index1 = i; one = false; }
                    (list[i]).S2 = x1.ToString();
                   // h1 += (list[i]).L2;
                    try
                    {
                        z1 += (list[i]).L5;
                    }
                    catch { }
                    x1++;
                }
                else if ((list[i]).L1 == 35)
                {
                    if (three)
                    { index35 = i; three = false; }
                    (list[i]).S2 = x35.ToString();
                    //h2 += (list[i]).L2;
                    try
                    {
                        z35 += (list[i]).L5;
                    }
                    catch { }
                    x35++;
                }
            }
            if (x5 > 1)
            {
                Line_Info info = new Line_Info();
                info.S2 = que[j];
                j++;
                info.Title = "500千伏线路";
               // info.L4 = h5;
                info.L5 = z5;
                info.L1 = 500;
                info.S4 = "no";
                list.Insert(index5, info);//.Add(info);
                now++;
            }
            if (x2 > 1)
            {
                Line_Info info2 = new Line_Info();
                info2.S2 = que[j];
                j++;
                info2.Title = "220千伏线路";
              //  info2.L2 = h2;
                info2.L5 = z2;
                info2.L1 = 220;
                info2.S4 = "no";
                list.Insert(index2 + now, info2);
                now++;
            }
            if (x1 > 1)
            {
                Line_Info info1 = new Line_Info();
                info1.S2 = que[j];
                j++;
                info1.Title = "110千伏公用线路";
              // info1.L2 = h1;
                info1.L5 = z1;
                info1.L1 = 110;
                info1.S4 = "no";
                list.Insert(index1 + now, info1);
                now++;
                //for (int k = 0; k < groupList.Count; k++)
                //{
                //    Substation_Info infok = new Substation_Info();
                //    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                //    infok.Title = groupList[k];
                //    conn = "L1=110 and AreaName='" + groupList[k] + "'";
                //    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                //    infok.L2 = ((Substation_Info)temList[0]).L2;
                //    infok.L5 = ((Substation_Info)temList[0]).L5;
                //    infok.L1 = 110;
                //    list.Insert(int.Parse(table[groupList[k]].ToString()) + now, infok);
                //    now++;
                //}
            }
            if (x1z > 1)
            {
                Line_Info info1z = new Line_Info();
                info1z.S2 = que[j];
                j++;
                info1z.Title = "110千伏专用线路";
              //  info1z.L2 = h1z;
                info1z.L5 = z1z;
                info1z.L1 = 110;
                info1z.S4 = "no";
                list.Insert(index1z + now, info1z);
                now++;
            }
            if (x35 > 1)
            {
                Line_Info info35 = new Line_Info();
                info35.S2 = que[j];
                j++;
                info35.Title = "35千伏线路";
                //  info1z.L2 = h1z;
                info35.L5 = z35;
                info35.L1 = 35;
                info35.S4 = "no";
                list.Insert(index35 + now, info35);
                now++;
            }
           // this.gridControl.DataSource = list;
        }


        public void RefreshLayout()
        {
            try
            {
                string filepath1 = "";
                Econ ec = new Econ();
                if (types2 == "66")
                {
                   
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType+"RefreshDatalayout1.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout1";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout1";
                }
                if (types2 == "10")
                {
                   
                    //filepath1 = Path.GetTempPath()+ Path.GetFileName(FlagsType + "RefreshDatalayout2.xml");
                    if (FlagsType == "")
                        ec.UID = "RefreshDatalayout2";
                    else
                        ec.UID = FlagsType + "RefreshDatalayout2";
                }
                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                if (listxml.Count != 0)
                {
                    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                    this.bandedGridView2.RestoreLayoutFromStream(ms);
                }   
            }
            catch { }
        }

        public bool RefreshData(string layer, bool isrun, string power)
        {

            IList<Line_Info> lists = new List<Line_Info>();
            try
            {

                Line_Info ll1 = new Line_Info();
                ll1.AreaID = layer;
                ll1.DY = int.Parse(power);



                //if (isrun)
                //{

                //    lists = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByXZ", ll1);
                //    filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName("RefreshData1layout1.xml");

                //}
                //else
                //{

                //    lists = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByGH", ll1);
                //    filepath1 = Path.GetTempPath() + "\\" + Path.GetFileName("RefreshData1layout2.xml");
                //}

                //if (File.Exists(filepath1))
                //{
                //    this.bandedGridView2.RestoreLayoutFromXml(filepath1);
                //}   

                if (isrun)
                {
                    lists = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByXZ", ll1);
                }
                else
                {
                    lists = Services.BaseService.GetList<Line_Info>("SelectLine_InfoByGH", ll1);
                }
               
                this.gridControl.DataSource = lists;


                //foreach (GridColumn gc in this.bandedGridView2.Columns)
                //{
                //    gc.Visible = false;
                //    gc.OptionsColumn.ShowInCustomizationForm = false;
                //    if (gc.FieldName == "Title" || gc.FieldName == "DY" || gc.FieldName == "K2" || gc.FieldName == "K5")
                //    {
                //        gc.Visible = true;
                //        gc.OptionsColumn.ShowInCustomizationForm = true;
                //    }
                //}
                bandedGridView2.OptionsView.ColumnAutoWidth = true;


                foreach (GridBand gc in this.bandedGridView2.Bands)
                {
                    try
                    {
                        gc.Visible = false;

                        if (gc.Columns[0].FieldName == "Title" || gc.Columns[0].FieldName == "DY" || gc.Columns[0].FieldName == "K2" || gc.Columns[0].FieldName == "K5")
                        {
                            gc.Visible = true;
                            gc.Caption = gc.Columns[0].Caption;
                            gc.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        }

                        
                    }
                    catch { }

                }


            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }

            return true;
        }

		/// <summary>
		/// 添加对象
		/// </summary>
        /// 
        public void AddObject()
        {
            //检查对象链表是否已经加载
            if (ObjectList == null)
            {
                return;
            }
            //新建对象
            Line_Info obj = new Line_Info();
            //obj.L6 = DateTime.Now;
            obj.CreateDate = DateTime.Now;
            //执行添加操作
            using (FrmLine_InfoDialog dlg = new FrmLine_InfoDialog())
            {
                if (gridControl.DataSource != null)
                    dlg.LIST = (IList<Line_Info>)gridControl.DataSource;

                //dlg.Type = types1;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.ctrlLint_Info = this;

                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;

                if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                {
                    dlg.rowAreaName.Properties.Caption = "地线型号";
                    dlg.rowL2.Visible = false;
                    dlg.rowL3.Visible = false;
                }
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }
            //将新对象加入到链表中
            ObjectList.Add(obj);

            //刷新表格，并将焦点行定位到新对象上。
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.bandedGridView2, obj);
        }
		public void AddObject(string type)
		{
			//检查对象链表是否已经加载
			if (ObjectList == null)
			{
				return;
			}
           
			//新建对象
			Line_Info obj = new Line_Info();
            //obj.L6 = DateTime.Now;
            obj.CreateDate = DateTime.Now;
			//执行添加操作
			using (FrmLine_InfoDialog dlg = new FrmLine_InfoDialog())
			{
                if(gridControl.DataSource!=null)
                    dlg.LIST = (IList<Line_Info>)gridControl.DataSource;
                dlg.ProjectID = projectid;
                //dlg.Type = types1;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.ctrlLint_Info = this;

				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;

            if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊") 
            {
                dlg.rowAreaName.Properties.Caption = "地线型号";
                dlg.rowL2.Visible = false;
                dlg.rowL3.Visible = false;
            }
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}
            RefreshData(type);
			//将新对象加入到链表中
            //ObjectList.Add(obj);

            ////刷新表格，并将焦点行定位到新对象上。
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.bandedGridView2, obj);
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象



			Line_Info obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.S4 == "no")
            { MessageBox.Show("不能修改此行！"); return; }
			//创建对象的一个副本
			Line_Info objCopy = new Line_Info();
			DataConverter.CopyTo<Line_Info>(obj, objCopy);

			//执行修改操作
			using (FrmLine_InfoDialog dlg = new FrmLine_InfoDialog())
			{
                if (gridControl.DataSource != null)
                    dlg.LIST = (IList<Line_Info>)gridControl.DataSource;

                dlg.IsSelect = isselect;
                dlg.ctrlLint_Info = this;
                dlg.Type = types1;
                //dlg.Type = flagstype;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.ProjectID = projectid;
                if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                {
                    dlg.rowAreaName.Properties.Caption = "地线型号";
                    dlg.rowL2.Visible = false;
                    dlg.rowL3.Visible = false;
                }

				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<Line_Info>(objCopy, obj);
           
			gridControl.RefreshDataSource();
		}
        public void UpdateObject(string type)
        {
            //获取焦点对象



            Line_Info obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            if (obj.S4 == "no")
            { MessageBox.Show("不能修改此行！"); return; }
            //创建对象的一个副本
            Line_Info objCopy = new Line_Info();
            DataConverter.CopyTo<Line_Info>(obj, objCopy);

            //执行修改操作
            using (FrmLine_InfoDialog dlg = new FrmLine_InfoDialog())
            {
                if (gridControl.DataSource != null)
                    dlg.LIST = (IList<Line_Info>)gridControl.DataSource;

                dlg.IsSelect = isselect;
                dlg.ctrlLint_Info = this;
                dlg.Type = types1;
                //dlg.Type = flagstype;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.ProjectID = projectid;
                if (System.Configuration.ConfigurationSettings.AppSettings["LangFang"].ToString() == "廊坊")
                {
                    dlg.rowAreaName.Properties.Caption = "地线型号";
                    dlg.rowL2.Visible = false;
                    dlg.rowL3.Visible = false;
                }

                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象
            DataConverter.CopyTo<Line_Info>(objCopy, obj);
            RefreshData(type);
            gridControl.RefreshDataSource();
        }

		/// <summary>
		/// 删除焦点对象
		/// </summary>
        /// 
        public void DeleteObject()
		{
			//获取焦点对象
			Line_Info obj = FocusedObject;
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
				Services.BaseService.Delete<Line_Info>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}
            this.bandedGridView2.BeginUpdate();
            //记住当前焦点行索引
            int iOldHandle = this.bandedGridView2.FocusedRowHandle;
            //从链表中删除
            ObjectList.Remove(obj);
            //刷新表格
            gridControl.RefreshDataSource();
            //设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.bandedGridView2, iOldHandle);
            this.bandedGridView2.EndUpdate();
		}
		public void DeleteObject(string type)
		{
			//获取焦点对象
			Line_Info obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.S4 == "no")
            { MessageBox.Show("不能删除此行！"); return; }
			//请求确认
			if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
			{
				return;
			}
            
			//执行删除操作
			try
			{
				Services.BaseService.Delete<Line_Info>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}
            RefreshData(type);
            //this.bandedGridView2.BeginUpdate();
            ////记住当前焦点行索引
            //int iOldHandle = this.bandedGridView2.FocusedRowHandle;
            ////从链表中删除
            //ObjectList.Remove(obj);
            ////刷新表格
            //gridControl.RefreshDataSource();
            ////设置新的焦点行索引
            //GridHelper.FocuseRowAfterDelete(this.bandedGridView2, iOldHandle);
            //this.bandedGridView2.EndUpdate();
		}
		#endregion

        private void gridControl_Load(object sender, EventArgs e)
        {

        }


	}
}
