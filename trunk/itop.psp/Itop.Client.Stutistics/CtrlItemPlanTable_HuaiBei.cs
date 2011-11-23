
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
using System.IO;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
#endregion

namespace Itop.Client.Stutistics
{
	public partial class CtrlItemPlanTable_HuaiBei : UserControl
    {
        private string types1 = "";
        private string flags1 = "";
        private string types2 = "";
        public bool editright = true;
        DataTable dt = new DataTable();

        ArrayList al = new ArrayList();
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
        public CtrlItemPlanTable_HuaiBei()
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
		public GridView GridView
		{
			get { return bandedGridView1; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
        public IList<PSP_PlanTable_HuaiBei> ObjectList
		{
            get { return this.gridControl.DataSource as IList<PSP_PlanTable_HuaiBei>; }
		}
        

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
        public PSP_PlanTable_HuaiBei FocusedObject
		{
            get { return this.bandedGridView1.GetRow(this.bandedGridView1.FocusedRowHandle) as PSP_PlanTable_HuaiBei; }
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
			if (GridHelper.HitCell(this.bandedGridView1, point))
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
			ComponentPrint.ShowPreview(this.gridControl, this.bandedGridView1.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
        public bool RefreshData1()
        {   IList list1 = new ArrayList();
        IList list2 = new ArrayList();
        IList list3 = new ArrayList();
                   
            try
            {
                IList<PSP_PlanTable_HuaiBei> listTypes = Services.BaseService.GetList<PSP_PlanTable_HuaiBei>("SelectPSP_PlanTable_HuaiBeiByFlag2", flags1);
                listTypes.RemoveAt(listTypes.Count - 1);
              
                //list1 = Common.Services.BaseService.GetList("SelectPSP_PlanTable_HuaiBeiByFlag2", flags1);
                //list1.RemoveAt(listTypes.Count - 1);
              
                listTypes[6].XuHao = "一";
                for (int j = 0; j < 6; j = j + 2)
                {
                    DateTime date = new DateTime();
                    listTypes[j + 1].XuHao = "1";
                    try
                    {
                        //if (listTypes[j].JHTCDateTime_GuiHua == null || listTypes[j].JHTCDateTime_GuiHua == "")
                        //    listTypes[j].JHTCDateTime_GuiHua = "0";
                      listTypes[j + 1].JHTCDateTime_GuiHua =  listTypes[j + 1].JHTCDateTime_GuiHua;
                       // date = Convert.ToDateTime(listTypes[j + 1].JHTCDateTime_GuiHua).AddMonths(-int.Parse(listTypes[j].JHTCDateTime_GuiHua));
                        //if (listTypes[j + 1].JHTCDateTime_GuiHua == null )
                        listTypes[j + 1].JHTCDateTime = Convert.ToDateTime(listTypes[j + 1].JHTCDateTime_GuiHua).ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].JHTCDateTime).AddMonths(-int.Parse(listTypes[j].JHTCDateTime));
                        listTypes[j + 1].JHKSDateTime = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].JHKSDateTime).AddMonths(-int.Parse(listTypes[j].JHKSDateTime));
                        listTypes[j + 1].XMHZ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].XMHZ).AddMonths(-int.Parse(listTypes[j].XMHZ));
                        listTypes[j + 1].SBHZSQ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].SBHZSQ).AddMonths(-int.Parse(listTypes[j].SBHZSQ));
                        listTypes[j + 1].TDYS = date.ToString("yyyy年MM月");
                    }
                    catch { }

                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].TDYS).AddMonths(-int.Parse(listTypes[j].TDYS));
                        listTypes[j + 1].HPPF = date.ToString("yyyy年MM月");
                    }
                    catch { }

                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].HPPF).AddMonths(-int.Parse(listTypes[j].HPPF));
                        listTypes[j + 1].XZYJS = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    /////////////////////////////////////////////
                    //try
                    //{
                    //    date = Convert.ToDateTime(listTypes[j + 1].CSSC);
                    //    listTypes[j + 1].CSSC = date.ToString("yyyy年MM月dd日");
                    //}
                    //catch { }
                    //try
                    //{
                    //    date = Convert.ToDateTime(listTypes[j + 1].JSGFSSB);
                    //    listTypes[j + 1].JSGFSSB = date.ToString("yyyy年MM月dd日");
                    //}
                    //catch { }
                    //try
                    //{
                    //    date = Convert.ToDateTime(listTypes[j + 1].ZBSB);
                    //    listTypes[j + 1].ZBSB = date.ToString("yyyy年MM月dd日");
                    //}
                    //catch { }
                    //try
                    //{
                    //    date = Convert.ToDateTime(listTypes[j + 1].ZBSHDateTime);
                    //    listTypes[j + 1].ZBSHDateTime = date.ToString("yyyy年MM月dd日");
                    //}
                    //catch { }

                    //中间空出4个，无计算过程
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].XZYJS).AddMonths(-int.Parse(listTypes[j].XZYJS));
                        listTypes[j + 1].PSYJ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].PSYJ).AddMonths(-int.Parse(listTypes[j].PSYJ));
                        listTypes[j + 1].KYPS = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].KYPS).AddMonths(-int.Parse(listTypes[j].KYPS));
                        listTypes[j + 1].KYWC = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].KYWC).AddMonths(-int.Parse(listTypes[j].KYWC));
                        listTypes[j + 1].ItemPF = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes[j + 1].ItemPF).AddMonths(-int.Parse(listTypes[j].ItemPF));
                        listTypes[j + 1].ItemSB = date.ToString("yyyy年MM月");
                    }
                    catch { }


                }
                
                //220千伏部分
                //list2 = Common.Services.BaseService.GetList("SelectPSP_PlanTable_HuaiBeiByFlag2AND220", flags1);

                IList<PSP_PlanTable_HuaiBei> listTypes1 = Services.BaseService.GetList<PSP_PlanTable_HuaiBei>("SelectPSP_PlanTable_HuaiBeiByFlag2AND220", flags1);
                for (int k = 0; k < listTypes1.Count; k++)
                {
                    DateTime date = new DateTime();
                    try
                    {
                        //if (listTypes[0].JHTCDateTime_GuiHua == null || listTypes[0].JHTCDateTime_GuiHua == "")
                        //    listTypes[0].JHTCDateTime_GuiHua = "0";
                       listTypes1[k].JHTCDateTime_GuiHua =listTypes1[k].JHTCDateTime_GuiHua;
                        //date = Convert.ToDateTime(listTypes1[k].JHTCDateTime_GuiHua).AddMonths(-int.Parse(listTypes[0].JHTCDateTime_GuiHua));
                       // if (listTypes1[k].JHTCDateTime_GuiHua == null )
                        listTypes1[k].JHTCDateTime = Convert.ToDateTime(listTypes1[k].JHTCDateTime_GuiHua).ToString("yyyy年MM月");
                        //if (DateTime.Now.Month >= DateTime.Parse(listTypes1[k].JHTCDateTime).Month)
                        //{

                        //}
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].JHTCDateTime).AddMonths(-int.Parse(listTypes[0].JHTCDateTime));
                        listTypes1[k].JHKSDateTime = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].JHKSDateTime).AddMonths(-int.Parse(listTypes[0].JHKSDateTime));
                        listTypes1[k].XMHZ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].XMHZ).AddMonths(-int.Parse(listTypes[0].XMHZ));
                        listTypes1[k].SBHZSQ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].SBHZSQ).AddMonths(-int.Parse(listTypes[0].SBHZSQ));
                        listTypes1[k].TDYS = date.ToString("yyyy年MM月");
                    }
                    catch { }

                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].TDYS).AddMonths(-int.Parse(listTypes[0].TDYS));
                        listTypes1[k].HPPF = date.ToString("yyyy年MM月");
                    }
                    catch { }

                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].HPPF).AddMonths(-int.Parse(listTypes[0].HPPF));
                        listTypes1[k].XZYJS = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    /////////////////////////////////////////////
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes1[k].CSSC);
                    ////    listTypes1[k].CSSC = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes1[k].JSGFSSB);
                    ////    listTypes1[k].JSGFSSB = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes1[k].ZBSB);
                    ////    listTypes1[k].ZBSB = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes1[k].ZBSHDateTime);
                    ////    listTypes1[k].ZBSHDateTime = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }

                    //中间空出4个，无计算过程
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].XZYJS).AddMonths(-int.Parse(listTypes[0].XZYJS));
                        listTypes1[k].PSYJ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].PSYJ).AddMonths(-int.Parse(listTypes[0].PSYJ));
                        listTypes1[k].KYPS = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].KYPS).AddMonths(-int.Parse(listTypes[0].KYPS));
                        listTypes1[k].KYWC = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].KYWC).AddMonths(-int.Parse(listTypes[0].KYWC));
                        listTypes1[k].ItemPF = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes1[k].ItemPF).AddMonths(-int.Parse(listTypes[0].ItemPF));
                        listTypes1[k].ItemSB = date.ToString("yyyy年MM月");
                    }
                    catch { }
 
                }
                //计算220千伏部分某几列的合计值
                double sum = 0;
                double sum1 = 0;
                double sum2= 0;
                for (int kk = 0; kk < listTypes1.Count; kk++)
                {
                   
                    if (listTypes1[kk].Line220 == null || listTypes1[kk].Line220 == "")
                        listTypes1[kk].Line220="0";
                    sum = sum + double.Parse(listTypes1[kk].Line220);
                    listTypes[6].Line220 = sum.ToString();

                    if (listTypes1[kk].BD220 == null || listTypes1[kk].BD220 == "")
                        listTypes1[kk].BD220="0";
                    sum1 = sum1 + double.Parse(listTypes1[kk].BD220);
                    listTypes[6].BD220 = sum1.ToString();
             
                    if (listTypes1[kk].TZGM == null || listTypes1[kk].TZGM == "")
                        listTypes1[kk].TZGM="0";
                    sum2 = sum2 + double.Parse(listTypes1[kk].TZGM);
                    listTypes[6].TZGM = sum2.ToString();
                }
                int xuhao=0;
                    foreach (PSP_PlanTable_HuaiBei psp in listTypes1)
                    {    xuhao++;
                        psp.XuHao=xuhao.ToString();
                        listTypes.Add(psp);
                    }
                    foreach (PSP_PlanTable_HuaiBei psp in list2)
                    {
                        list1.Add(psp);
                    }
                //110千伏部分

                   //list3 = Services.BaseService.GetList("SelectPSP_PlanTable_HuaiBeiByFlag2AND110", flags1);
 
                IList<PSP_PlanTable_HuaiBei> listTypes2 = Services.BaseService.GetList<PSP_PlanTable_HuaiBei>("SelectPSP_PlanTable_HuaiBeiByFlag2AND110", flags1);
                PSP_PlanTable_HuaiBei obb = (PSP_PlanTable_HuaiBei)Common.Services.BaseService.GetObject("SelectPSP_PlanTable_HuaiBeiByFlag2ANDKeyFlag", flags1);
                for (int k2 = 0; k2 < listTypes2.Count; k2++)
                {
                    DateTime date = new DateTime();
                    try
                    {
                        //if (listTypes[0].JHTCDateTime_GuiHua == null || listTypes[0].JHTCDateTime_GuiHua == "")
                        //    listTypes[0].JHTCDateTime_GuiHua = "0";
                       listTypes2[k2].JHTCDateTime_GuiHua =  listTypes2[k2].JHTCDateTime_GuiHua;
                       // date = Convert.ToDateTime(listTypes2[k2].JHTCDateTime_GuiHua).AddMonths(-int.Parse(listTypes[2].JHTCDateTime_GuiHua));
                       // if (listTypes2[k2].JHTCDateTime_GuiHua == null )
                        listTypes2[k2].JHTCDateTime = Convert.ToDateTime(listTypes2[k2].JHTCDateTime_GuiHua).ToString("yyyy年MM月");
              
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].JHTCDateTime).AddMonths(-int.Parse(listTypes[2].JHTCDateTime));
                        listTypes2[k2].JHKSDateTime = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].JHKSDateTime).AddMonths(-int.Parse(listTypes[2].JHKSDateTime));
                        listTypes2[k2].XMHZ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].XMHZ).AddMonths(-int.Parse(listTypes[2].XMHZ));
                        listTypes2[k2].SBHZSQ = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].SBHZSQ).AddMonths(-int.Parse(listTypes[2].SBHZSQ));
                        listTypes2[k2].TDYS = date.ToString("yyyy年MM月");
                    }
                    catch { }

                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].TDYS).AddMonths(-int.Parse(listTypes[2].TDYS));
                        listTypes2[k2].HPPF = date.ToString("yyyy年MM月");
                    }
                    catch { }

                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].HPPF).AddMonths(-int.Parse(listTypes[2].HPPF));
                        listTypes2[k2].XZYJS = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    /////////////////////////////////////////////
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes2[k2].CSSC);
                    ////    listTypes2[k2].CSSC = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes2[k2].JSGFSSB);
                    ////    listTypes2[k2].JSGFSSB = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes2[k2].ZBSB);
                    ////    listTypes2[k2].ZBSB = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }
                    ////try
                    ////{
                    ////    date = Convert.ToDateTime(listTypes2[k2].ZBSHDateTime);
                    ////    listTypes2[k2].ZBSHDateTime = date.ToString("yyyy年MM月dd日");
                    ////}
                    ////catch { }

                    //中间空出4个，无计算过程
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].XZYJS).AddMonths(-int.Parse(listTypes[2].XZYJS));
                        listTypes2[k2].PSYJ = date.ToString("yyyy年MM月");
                       
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].PSYJ).AddMonths(-int.Parse(listTypes[2].PSYJ));
                        listTypes2[k2].KYPS = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].KYPS).AddMonths(-int.Parse(listTypes[2].KYPS));
                        listTypes2[k2].KYWC = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].KYWC).AddMonths(-int.Parse(listTypes[2].KYWC));
                        listTypes2[k2].ItemPF = date.ToString("yyyy年MM月");
                    }
                    catch { }
                    try
                    {
                        date = Convert.ToDateTime(listTypes2[k2].ItemPF).AddMonths(-int.Parse(listTypes[2].ItemPF));
                        listTypes2[k2].ItemSB = date.ToString("yyyy年MM月");
                    }
                    catch { }

                }
                //计算110千伏部分某几列的合计值
                sum = 0;
                sum1 = 0;
                sum2 = 0;
                for (int m = 0; m < listTypes2.Count; m++)
                {
                    if (listTypes2[m].Line110 == null || listTypes2[m].Line110 == "")
                        listTypes2[m].Line110="0";
                    sum = sum + double.Parse(listTypes2[m].Line110);
                    obb.Line110 = sum.ToString();

                    if (listTypes2[m].BD110 == null || listTypes2[m].BD110 == "")
                        listTypes2[m].BD110="0";
                    sum1 = sum1 + double.Parse(listTypes2[m].BD110);
                    obb.BD110 = sum1.ToString();

                    if (listTypes2[m].TZGM == null || listTypes2[m].TZGM == "")
                        listTypes2[m].TZGM="0";
                    sum2 = sum2 + double.Parse(listTypes2[m].TZGM);
                    obb.TZGM = sum2.ToString();
                }
                obb.XuHao = "二";
                listTypes.Add(obb);

                //list1.Add(obb);

                xuhao = 0;
                foreach (PSP_PlanTable_HuaiBei psp in listTypes2)
                {
                    xuhao++;
                    psp.XuHao = xuhao.ToString();
                    listTypes.Add(psp);
                }
                foreach (PSP_PlanTable_HuaiBei psp in listTypes)
                {
                    list1.Add(psp);
                }
                ////设置计划投产时间（规划）的格式

                //listTypes[1].JHTCDateTime_GuiHua = listTypes[1].JHTCDateTime_GuiHua.ToString("yyyy年MM月");
                //listTypes[n].JHTCDateTime_GuiHua = listTypes[n].JHTCDateTime_GuiHua.ToString("yyyy年MM月");
                //listTypes[n].JHTCDateTime_GuiHua = listTypes[n].JHTCDateTime_GuiHua.ToString("yyyy年MM月");
                //for (int n = 7; n < listTypes.Count; n++)
                //{
                //  listTypes[n].JHTCDateTime_GuiHua=listTypes[n].JHTCDateTime_GuiHua.ToString("yyyy年MM月");
                //}

                    this.gridControl.DataSource = listTypes;
                    //mn=DT.Rows[0][0].ToString();
                

                    dt = Itop.Common.DataConverter.ToDataTable(list1, typeof(PSP_PlanTable_HuaiBei));
                    al.Clear();
                    for (int h = 0; h < dt.Columns.Count;h++ )
                        al.Add(dt.Columns[h].ColumnName);
            
                    //for (int I = 0; I < bandedGridView1.VisibleColumns.Count; I++)
                        bandedGridView1.GetVisibleColumn(1).AppearanceCell.ForeColor = Color.Red;
                    //bandedGridView1.GetRowCellValue(1, aaa);
                    //PSP_PlanTable_HuaiBei dd = bandedGridView1.GetRow() as PSP_PlanTable_HuaiBei;
                    
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return false;
            }
            return true;
        }
        public bool RefreshData()
     {
         try
         {
         //        this.gridControl.DataSource = listTypes;
         }
         catch (Exception exc)
         {
             Debug.Fail(exc.Message);
             HandleException.TryCatch(exc);
             return false;
         }
         return true;
		}
        public static DataSet ListToDataSet(IList ResList)
        {
            DataSet RDS = new DataSet();
            DataTable TempDT = new DataTable();

            //此处遍历IList的结构并建立同样的DataTable
            System.Reflection.PropertyInfo[] p = ResList[0].GetType().GetProperties();
            foreach (System.Reflection.PropertyInfo pi in p)
            {
                //TempDT.Columns.Add(pi.SetValue().Name, System.Type.GetType(pi.PropertyType.ToString()));
                
            }

            for (int i = 0; i < ResList.Count; i++)
            {
                IList TempList = new ArrayList();
                //将IList中的一条记录写入ArrayList
                foreach (System.Reflection.PropertyInfo pi in p)
                {
                    object oo = pi.GetValue(ResList[i], null);
                    TempList.Add(oo);
                }

                object[] itm = new object[p.Length];
                //遍历ArrayList向object[]里放数据
                for (int j = 0; j < TempList.Count; j++)
                {
                    itm.SetValue(TempList[j], j);
                }
                //将object[]的内容放入DataTable
                TempDT.LoadDataRow(itm, true);
            }
            //将DateTable放入DataSet
            RDS.Tables.Add(TempDT);
            //返回DataSet
            return RDS;
        }

        //private IList<PSP_PlanTable_HuaiBei> tablelist(DataSet ds)
        //{
        //    PSP_PlanTable_HuaiBei t = default(PSP_PlanTable_HuaiBei);
        //    ds.Tables[0].TableName = typeof(T).Name;
        //    string str = ds.GetXml();
        //    XmlDocument xd = new XmlDocument();
        //    xd.LoadXml(str);
        //    XmlNodeList xls = xd.SelectNodes("/" + typeof(PSP_PlanTable_HuaiBei).Name.ToString() + "s/" + typeof(PSP_PlanTable_HuaiBei).Name.ToString());
        //    IList<PSP_PlanTable_HuaiBei> ts = new List<PSP_PlanTable_HuaiBei>();
        //    foreach (XmlNode xn in xls)
        //    {
        //        string str1 = xn.OuterXml;
        //        System.Xml.Serialization.XmlSerializer xms = new XmlSerializer(typeof(PSP_PlanTable_HuaiBei));
        //        System.IO.MemoryStream m = new System.IO.MemoryStream();
        //        System.IO.StreamWriter sw = new System.IO.StreamWriter(m);
        //        sw.Write(str1);
        //        sw.Flush();
        //        m.Position = 0;
        //        t = (PSP_PlanTable_HuaiBei)xms.Deserialize(m);
        //        ts.Add(t);
        //    }
        //    if (null != ts)
        //        return ts;
        //    else
        //        return null;
        //} 
		/// <summary>
		/// 添加对象
		/// </summary>
        public void AddObject(string flag)
        {
           // 检查对象链表是否已经加载
            if (ObjectList == null)
            {
                return;
            }
            //新建对象
            PSP_PlanTable_HuaiBei obj = new PSP_PlanTable_HuaiBei();
           
            obj.Flag2 = flags1;
            obj.CreateDate = DateTime.Now;

            //执行添加操作
            using (FrmPSP_PlanTable_HuaiBeiDialog dlg = new FrmPSP_PlanTable_HuaiBeiDialog())
            {
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
               //// dlg.ctrlPSP_PowerSubstationInfo = this;
                dlg.Text = "添加项目计划表";
                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;

                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //将新对象加入到链表中
            ObjectList.Add(obj);

            //刷新表格，并将焦点行定位到新对象上。
            gridControl.RefreshDataSource();
            RefreshData1();
            GridHelper.FocuseRow(this.bandedGridView1, obj);
        }

        /// <summary>
        /// 修改焦点对象
        /// </summary>
        public void UpdateObject()
        {
            //获取焦点对象
            PSP_PlanTable_HuaiBei obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            
            //创建对象的一个副本
            PSP_PlanTable_HuaiBei objCopy = new PSP_PlanTable_HuaiBei();
            DataConverter.CopyTo<PSP_PlanTable_HuaiBei>(obj, objCopy);

            //执行修改操作
            using (FrmPSP_PlanTable_HuaiBeiDialog dlg = new FrmPSP_PlanTable_HuaiBeiDialog())
            {
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.Type2 = types2;
                dlg.Text = "修改项目计划表";
               //// dlg.ctrlPSP_PowerSubstationInfo = this;
                //int[] a = this.GridView.SetFocusedRowCellValue(.GetSelectedRows(); //传递实体类过去 获取选中的行
                //LAA.AssetGuid = this.GridView.GetRowCellValue(a[0], ).ToString();//获取选中行的内容

                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象
            DataConverter.CopyTo<PSP_PlanTable_HuaiBei>(objCopy, obj);
            //刷新表格
            RefreshData1();
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 删除焦点对象
        /// </summary>
        public void DeleteObject()
        {
            //获取焦点对象
            PSP_PlanTable_HuaiBei obj = FocusedObject;
            if (obj == null)
            {
                return;
            }
            if (obj.KeyFlag == "time1" || obj.KeyFlag == "time2" || obj.KeyFlag == "time3" || obj.KeyFlag == "220" || obj.KeyFlag == "110" || obj.KeyFlag == "kuojian" || obj.KeyFlag == "220千伏" || obj.KeyFlag == "110千伏")
            {
                MsgBox.Show("此行为固定行不允许删除！");
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
              //  Services.BaseService.Delete<PSP_PowerSubstationInfo>(obj);
                Services.BaseService.Delete<PSP_PlanTable_HuaiBei>(obj);
            }
            catch (Exception exc)
            {
                Debug.Fail(exc.Message);
                HandleException.TryCatch(exc);
                return;
            }

            this.bandedGridView1.BeginUpdate();
            //记住当前焦点行索引
            int iOldHandle = this.bandedGridView1.FocusedRowHandle;
            //从链表中删除
            ObjectList.Remove(obj);
            //刷新表格
            gridControl.RefreshDataSource();
            //设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
            RefreshData1();
            this.bandedGridView1.EndUpdate();
        }
		#endregion

        private void gridControl_Validating(object sender, CancelEventArgs e)
        {
            PSP_PlanTable_HuaiBei obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

        }
        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            try
            {
                if (e.RowHandle > 6 || e.RowHandle == 1 || e.RowHandle == 3 || e.RowHandle == 5)
                {
                    if (e.Column.FieldName.ToString() == "XuHao" || e.Column.FieldName.ToString() == "Title" || e.Column.FieldName.ToString() == "Contents" || e.Column.FieldName.ToString() == "BD220" || e.Column.FieldName.ToString() == "BD110" || e.Column.FieldName.ToString() == "Line110" || e.Column.FieldName.ToString() == "Line220" || e.Column.FieldName.ToString() == "Contents" || e.Column.FieldName.ToString() == "DeptName" || e.Column.FieldName.ToString() == "DY" || e.Column.FieldName.ToString() == "TZGM" || e.Column.FieldName.ToString() == "ReMark")
                    { }
                    else
                    {
                        //if (al.Contains(e.Column.FieldName))
                            if (e.Column.FieldName.ToString() == "PSYJ")
                            {
                                if (DateTime.Now.Year >= DateTime.Parse(dt.Rows[e.RowHandle ][e.Column.FieldName].ToString()).Year && DateTime.Now.Year < DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 5].ToString()].ToString()).Year)
                                {
                                    if (DateTime.Now.Month >= DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                                if (DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Year && DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 5].ToString()].ToString()).Year)
                                {
                                    if (DateTime.Now.Month > DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month && DateTime.Now.Month < DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 5].ToString()].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                    if (DateTime.Now.Month == DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                                if (DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle ][e.Column.FieldName].ToString()).Year && DateTime.Now.Year > DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 5].ToString()].ToString()).Year)
                                {
                                    if (DateTime.Now.Month >= DateTime.Parse(dt.Rows[e.RowHandle ][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                            }
                            else if (e.Column.FieldName.ToString() == "ZBSHDateTime" || e.Column.FieldName.ToString() == "JHTCDateTime_GuiHua")
                            {
                                if (DateTime.Now.Year > DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Year)
                                {
                                    e.Appearance.ForeColor = Color.Green;
                                }
                                if (DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Year)
                                {
                                    if (DateTime.Now.Month >= DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                            }
                            else 
                            {
                                if (DateTime.Now.Year >= DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Year && DateTime.Now.Year < DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 1].ToString()].ToString()).Year)
                                {
                                    if (DateTime.Now.Month >= DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                                if (DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Year && DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 1].ToString()].ToString()).Year)
                                {
                                    if (DateTime.Now.Month > DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month && DateTime.Now.Month < DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 1].ToString()].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                    if (DateTime.Now.Month == DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                                if (DateTime.Now.Year == DateTime.Parse(dt.Rows[e.RowHandle][e.Column.FieldName].ToString()).Year && DateTime.Now.Year > DateTime.Parse(dt.Rows[e.RowHandle][al[al.IndexOf(e.Column.FieldName) + 1].ToString()].ToString()).Year)
                                {
                                    //if (DateTime.Now.Month >= DateTime.Parse(dt.Rows[e.RowHandle + 1][e.Column.FieldName].ToString()).Month)
                                    {
                                        e.Appearance.ForeColor = Color.Green;
                                    }
                                }
                            }

                        }
                  
                }
            }
            catch { }
           
           
        }

        private void gridControl_Click(object sender, EventArgs e)
        {

        }



      
	}
}
