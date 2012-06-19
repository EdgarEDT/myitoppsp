
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
#endregion

namespace Itop.Client.Table
{
	public partial class CtrlSubstation_Info_AHTL : UserControl
	{

        private string types1 = "";
        private string flags1 = "";
        public string xmlflag = "";
        public bool editright = true;
        private string Ghflag = "1";
        private IList fu_list = null;
        private List<Substation_Info> fu_list_no = new List<Substation_Info>(); 
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
		public CtrlSubstation_Info_AHTL()
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
		/// 获取bandedGridView1对象
		/// </summary>
		public BandedGridView GridView
		{
			get { return bandedGridView1; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<Substation_Info> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Substation_Info>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public Substation_Info FocusedObject
		{
			get { return this.bandedGridView1.GetRow(this.bandedGridView1.FocusedRowHandle) as Substation_Info; }
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
			ComponentPrint.ShowPreview(this.gridControl, this.bandedGridView1.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
				IList<Substation_Info> list = Services.BaseService.GetStrongList<Substation_Info>();
                        
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
        public void Is_Fu(bool tempbool)
        {
            if (tempbool)
            {
                this.gridControl.DataSource = fu_list;
            }
            else
            {
                this.gridControl.DataSource = fu_list_no;
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
            string con = "AreaID='" + projectid + "'"+ " and Flag='"+Ghflag+"'";
           // con += addConn;
            con += " order by convert(int,L1) desc,S4,AreaName,CreateDate,convert(int,S5)";
            string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
            titlestr = que;
            //IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByCon", con);
            IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", con);
            if (!string.IsNullOrEmpty(addConn))
            {
                List<Substation_Info> listremove = new List<Substation_Info>();
                for (int i = 0; i < list.Count;i++ )
                {
                    if (((Substation_Info)list[i]).S2.Length!=4)
                    {
                        listremove.Add((Substation_Info)list[i]);
                    }
                    else
                    {
                        if (Convert.ToDouble(((Substation_Info)list[i]).S2)>Convert.ToDouble(addConn))
                        {
                            listremove.Add((Substation_Info)list[i]);
                        }
                    }
                }
                for (int i = 0; i < listremove.Count;i++ )
                {
                    list.Remove(listremove[i]);
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
                if (((Substation_Info)list[i]).L1 == 500  )
                {
                    if (((Substation_Info)list[i]).AreaName != area_500)
                    {
                        if (!table_500.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_500.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_500.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_500 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (five)
                    { index5 = i; five = false; }
                    ((Substation_Info)list[i]).S3 = x5.ToString();
                    h5 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z5 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t5 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x5++;
                }
                else if (((Substation_Info)list[i]).L1 == 220 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_220z)
                    {
                        if (!table_220z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_220z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_220z.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area_220z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (twoz)
                    { index2z = i; twoz = false; }
                    ((Substation_Info)list[i]).S3 = x2z.ToString();
                    h2z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z2z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t2z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x2z++;
                }
                else if (((Substation_Info)list[i]).L1 == 220 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_220)
                    {
                        if (!table_220.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_220.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_220.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area_220 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (two)
                    { index2 = i; two = false; }
                    ((Substation_Info)list[i]).S3 = x2.ToString();
                    h2 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z2 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t2 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x2++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area2)
                    {
                        if (!table2.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table2.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList2.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area2 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (onez)
                    { index1z = i; onez = false; }
                    ((Substation_Info)list[i]).S3 = x1z.ToString();
                    h1z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z1z += double.Parse(((Substation_Info)list[i]).L5);
                       
                    }
                    catch { }
                    try
                    {
                      
                        t1z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x1z++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area)
                    {
                        if (!table.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList.Add(((Substation_Info)list[i]).AreaName);
                            //  table[((Substation_Info)list[i]).AreaName] = i;
                        }
                        area = ((Substation_Info)list[i]).AreaName;
                    }
                   
                    if (one)
                    { index1 = i; one = false; }
                    ((Substation_Info)list[i]).S3 = x1.ToString();
                    h1 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z1 += double.Parse(((Substation_Info)list[i]).L5);
                       
                    }
                    catch { }
                    try
                    {
                      
                        t1 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x1++;
                }
                else if (((Substation_Info)list[i]).L1 == 35 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_35z)
                    {
                        if (!table_35z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_35z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_35z.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_35z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (threez)
                    { index35z = i; threez = false; }
                    ((Substation_Info)list[i]).S3 = x35z.ToString();
                    h35z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z35z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t35z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x35z++;
                }
                else if (((Substation_Info)list[i]).L1 == 35 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_35)
                    {
                        if (!table_35.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_35.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_35.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_35 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (three)
                    { index35 = i; three = false; }
                    ((Substation_Info)list[i]).S3 = x35.ToString();
                    h35 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z35 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t35 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x35++;
                }
                else if (((Substation_Info)list[i]).L1 == 10 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_10z)
                    {
                        if (!table_10z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_10z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_10z.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_10z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (tenz)
                    { index10z = i; tenz = false; }
                    ((Substation_Info)list[i]).S3 = x10z.ToString();
                    h10z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z10z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t10z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x10z++;
                }
                else if (((Substation_Info)list[i]).L1 == 10 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_10)
                    {
                        if (!table_10.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_10.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_10.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_10 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (ten)
                    { index10 = i; ten = false; }
                    ((Substation_Info)list[i]).S3 = x10.ToString();
                    h10 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z10 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t10 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x10++;
                }
                else if (((Substation_Info)list[i]).L1 == 6 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_6z)
                    {
                        if (!table_6z.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_6z.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_6z.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_6z = ((Substation_Info)list[i]).AreaName;
                    }
                    if (sixz)
                    { index6z = i; sixz = false; }
                    ((Substation_Info)list[i]).S3 = x6z.ToString();
                    h6z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z6z += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t6z += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x6z++;
                }
                else if (((Substation_Info)list[i]).L1 == 6 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area_6)
                    {
                        if (!table_6.Contains(((Substation_Info)list[i]).AreaName))
                        {

                            table_6.Add(((Substation_Info)list[i]).AreaName, i);
                            groupList_6.Add(((Substation_Info)list[i]).AreaName);
                        }
                        area_6 = ((Substation_Info)list[i]).AreaName;
                    }
                    if (six)
                    { index6 = i; six = false; }
                    ((Substation_Info)list[i]).S3 = x6.ToString();
                    h6 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z6 += double.Parse(((Substation_Info)list[i]).L5);

                    }
                    catch { }
                    try
                    {

                        t6 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x6++;
                }
            }
            if (x5 > 1)
            {
                Substation_Info info = new Substation_Info();
                info.S3 = que[j];
                j++;
                info.Title = "500千伏";
                info.L2 = h5;
                info.L5 = z5.ToString();
                info.L3 = t5;
                info.L1 = 500;
                info.S4 = "no";
                list.Insert(index5, info);//.Add(info);
                now++;
                for (int k = 0; k < groupList_500.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_500[k];
                    conn = "L1=500 and AreaID='" + projectid + "' and  AreaName='" + groupList_500[k] + "'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 500;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_500[groupList_500[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x2 > 1)
            {
                Substation_Info info2 = new Substation_Info();
                info2.S3 = que[j];
                j++;
                info2.Title = "220千伏公变";
                info2.L2 = h2;
                info2.L5 = z2.ToString();
                info2.L3 = t2;
                info2.L1 = 220;
                info2.S4 = "no";
                list.Insert(index2 + now, info2);
                now++;
                for (int k = 0; k < groupList_220.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_220[k];
                    conn = "L1=220 and AreaID='" + projectid + "' and  AreaName='" + groupList_220[k] + "'  and S4!='专用'" ;
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 220;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_220[groupList_220[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x2z > 1)
            {
                Substation_Info info2z = new Substation_Info();
                info2z.S3 = que[j];
                j++;
                info2z.Title = "220千伏专变";
                info2z.L2 = h2z;
                info2z.L5 = z2z.ToString();
                info2z.L3 = t2z;
                info2z.L1 = 220;
                info2z.S4 = "no";
                list.Insert(index2z + now, info2z);
                now++;

                for (int k = 0; k < groupList_220z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_220z[k];
                    conn = "L1=220 and AreaID='" + projectid + "' and  AreaName='" + groupList_220z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 220;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_220z[groupList_220z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x1 > 1)
            {
                Substation_Info info1 = new Substation_Info();
                info1.S3 = que[j];
                j++;
                info1.Title = "110千伏公变";
                info1.L2 = h1;
                info1.L5 = z1.ToString();
                info1.L3= t1;
                info1.L1 = 110;
                info1.S4 = "no";
                list.Insert(index1 + now, info1);
                now++;
                for (int k = 0; k < groupList.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList[k];
                    conn = "L1=110 and AreaID='" + projectid + "' and  AreaName='" + groupList[k] + "'  and S4!='专用'" ;
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table[groupList[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x1z > 1)
            {
                Substation_Info info1z = new Substation_Info();
                info1z.S3 = que[j];
                j++;
                info1z.Title = "110千伏专变";
                info1z.L2 = h1z;
                info1z.L5 = z1z.ToString();
                info1z.L3 = t1z;
                info1z.L1 = 110;
                info1z.S4 = "no";
                list.Insert(index1z + now, info1z);
                now++;

                for (int k = 0; k < groupList2.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList2[k];
                    conn = "L1=110 and AreaID='" + projectid + "' and  AreaName='" + groupList2[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch 
                        {
                        	
                        }
                    }
                    list.Insert(int.Parse(table2[groupList2[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x35 > 1)
            {
                Substation_Info info35 = new Substation_Info();
                info35.S3 = que[j];
                j++;
                info35.Title = "35千伏公变";
                info35.L2 = h35;
                info35.L5 = z35.ToString();
                info35.L3 = t35;
                info35.L1 = 35;
                info35.S4 = "no";
                list.Insert(index35 + now, info35);
                now++;
                for (int k = 0; k < groupList_35.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_35[k];
                    conn = "L1=35 and AreaID='" + projectid + "' and  AreaName='" + groupList_35[k] + "'  and S4!='专用' ";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 35;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_35[groupList_35[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x35z > 1)
            {
                Substation_Info info35z = new Substation_Info();
                info35z.S3 = que[j];
                j++;
                info35z.Title = "35千伏专变";
                info35z.L2 = h35z;
                info35z.L5 = z35z.ToString();
                info35z.L3 = t35z;
                info35z.L1 = 35;
                info35z.S4 = "no";
                list.Insert(index35z + now, info35z);
                now++;

                for (int k = 0; k < groupList_35z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_35z[k];
                    conn = "L1=35 and AreaID='" + projectid + "' and  AreaName='" + groupList_35z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 35;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_35z[groupList_35z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x10 > 1)
            {
                Substation_Info info10 = new Substation_Info();
                info10.S3 = que[j];
                j++;
                info10.Title = "10千伏公变";
                info10.L2 = h10;
                info10.L5 = z10.ToString();
                info10.L3 = t10;
                info10.L1 = 10;
                info10.S4 = "no";
                list.Insert(index10 + now, info10);
                now++;
                for (int k = 0; k < groupList_10.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_10[k];
                    conn = "L1=10 and AreaID='" + projectid + "' and  AreaName='" + groupList_10[k] + "'  and S4!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 10;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_10[groupList_10[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x10z > 1)
            {
                Substation_Info info10z = new Substation_Info();
                info10z.S3 = que[j];
                j++;
                info10z.Title = "10千伏专变";
                info10z.L2 = h10z;
                info10z.L5 = z10z.ToString();
                info10z.L3 = t10z;
                info10z.L1 = 10;
                info10z.S4 = "no";
                list.Insert(index10z + now, info10z);
                now++;

                for (int k = 0; k < groupList_10z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_10z[k];
                    conn = "L1=10 and AreaID='" + projectid + "' and  AreaName='" + groupList_10z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 10;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_10z[groupList_10z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x6 > 1)
            {
                Substation_Info info6 = new Substation_Info();
                info6.S3 = que[j];
                j++;
                info6.Title = "6千伏公变";
                info6.L2 = h6;
                info6.L5 = z6.ToString();
                info6.L3 = t6;
                info6.L1 = 6;
                info6.S4 = "no";
                list.Insert(index6 + now, info6);
                now++;
                for (int k = 0; k < groupList_6.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList[k];
                    infok.AreaName = groupList_6[k];
                    conn = "L1=6 and AreaID='" + projectid + "' and  AreaName='" + groupList_6[k] + "'  and S4!='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 6;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_6[groupList_6[k]].ToString()) + now, infok);
                    now++;
                }
            }
            if (x6z > 1)
            {
                Substation_Info info6z = new Substation_Info();
                info6z.S3 = que[j];
                j++;
                info6z.Title = "6千伏专变";
                info6z.L2 = h6z;
                info6z.L5 = z6z.ToString();
                info6z.L3 = t6z;
                info6z.L1 = 6;
                info6z.S4 = "no";
                list.Insert(index6z + now, info6z);
                now++;

                for (int k = 0; k < groupList_6z.Count; k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k + 65).ToString().ToLower();
                    //infok.Title = groupList2[k];
                    infok.AreaName = groupList_6z[k];
                    conn = "L1=6 and AreaID='" + projectid + "' and  AreaName='" + groupList_6z[k] + "' and S4='专用'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 6;
                    infok.S4 = "no";
                    conn += " and Flag='" + Ghflag + "' order by Title ";
                    temList = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", conn);
                    infok.L3 = 0;
                    foreach (Substation_Info temp in temList)
                    {
                        try
                        {
                            infok.L3 += (int)temp.L3;
                        }
                        catch
                        {

                        }
                    }
                    list.Insert(int.Parse(table_6z[groupList_6z[k]].ToString()) + now, infok);
                    now++;
                }
            }
            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (((Substation_Info)list[i]).L9 != null && (double)((Substation_Info)list[i]).L2 != null)
                    {
                        double templ9 = (double)((Substation_Info)list[i]).L9;
                        double templ2 = (double)((Substation_Info)list[i]).L2;
                        ((Substation_Info)list[i]).L10 = (templ2 == 0 ? 0 : templ9 / templ2) * 100;
                        Substation_Info tempsub = Common.Services.BaseService.GetOneByKey<Substation_Info>(((Substation_Info)list[i]).UID);
                        tempsub.L10 = ((Substation_Info)list[i]).L10;
                        Common.Services.BaseService.Update<Substation_Info>(tempsub);
                    }
                }
            }
            catch (Exception ew)
            {

                MessageBox.Show("计算负载率出错" + ew.Message);
            }
            

            this.gridControl.DataSource = list;
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
                if (!string.IsNullOrEmpty(((Substation_Info)templist[i]).S3))
                {
                    if (char.IsLower(((Substation_Info)templist[i]).S3, 0))
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
                 string con = "AreaID='" + projectid + "' order by convert(int,L1) desc,S4,AreaName desc,CreateDate,convert(int,S5)";
                 IList<Substation_Info> list = Common.Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByWhere", con);
                //IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon",con);
                if(xmlflag=="guihua")
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationGuiHua.xml");
                else
                {
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationLayOut11.xml");
                }
              
                if (File.Exists(filepath))
                {
                    this.bandedGridView1.RestoreLayoutFromXml(filepath);
                }
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

        public bool RefreshDataOut(string addConn)
        {
            try
            {
                string filepath = "";
                string con = "AreaID='" + projectid + "'";
                con += addConn;
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", con);
                if (xmlflag == "guihua")
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationGuiHua.xml");
                else
                {
                    filepath = Path.GetTempPath() + "\\" + Path.GetFileName("SubstationLayOut11.xml");
                }

                if (File.Exists(filepath))
                {
                    this.bandedGridView1.RestoreLayoutFromXml(filepath);
                }
                Substation_Info info = new Substation_Info();
                info.Title = "合计";
                info.L9 = 0.0;
                info.L3 = 0;
                for (int i = 0; i < list.Count; i++)
                {
                   
                    info.L5 = Convert.ToString(double.Parse(info.L5 == "" ? "0" : info.L5) + double.Parse((list[i] as Substation_Info).L5 == "" ? "0" : (list[i] as Substation_Info).L5));
                    info.L6 = Convert.ToString(double.Parse(info.L6 == "" ? "0" : info.L6) + double.Parse((list[i] as Substation_Info).L6 == "" ? "0" : (list[i] as Substation_Info).L6));
                    info.L3 += ((list[i] as Substation_Info).L3 == null ? 0 : (list[i] as Substation_Info).L3);
                    info.L2 += (list[i] as Substation_Info).L2;
                    info.L9 += (list[i] as Substation_Info).L9;
                    info.L14 = Convert.ToString(double.Parse(info.L14 == "" ? "0" : info.L14) + double.Parse((list[i] as Substation_Info).L14 == "" ? "0" : (list[i] as Substation_Info).L14));
                    info.L13 = Convert.ToString(double.Parse(info.L13 == "" ? "0" : info.L13) + double.Parse((list[i] as Substation_Info).L13 == "" ? "0" : (list[i] as Substation_Info).L13));
                    info.S9 = Convert.ToString(double.Parse(info.S9 == "" ? "0" : info.S9) + double.Parse((list[i] as Substation_Info).S9 == "" ? "0" : (list[i] as Substation_Info).S9));
                    info.S10 = Convert.ToString(double.Parse(info.S10 == "" ? "0" : info.S10) + double.Parse((list[i] as Substation_Info).S10 == "" ? "0" : (list[i] as Substation_Info).S10));
                }
                info.L10 = (info.L2 == 0 ? 0 : info.L9 / info.L2)*100;
                info.S6 = Convert.ToString(double.Parse(info.L13) == 0.0 ? 0 : double.Parse(info.L14 == "" ? "0" : info.L14)*100 / double.Parse(info.L13));
                list.Add(info);
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




        public bool RefreshData(string layer, bool isrun, string power)
        {

            IList<Substation_Info> lists = new List<Substation_Info>();
            try
            {

                Substation_Info ll1 = new Substation_Info();
                ll1.AreaID = layer;
                ll1.L1 = int.Parse(power);

                if (isrun)
                {
                    lists = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByXZ", ll1);
                }
                else
                {

                    lists = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByGH", ll1);
                }

                this.gridControl.DataSource = lists;


                bandedGridView1.OptionsView.ColumnAutoWidth = true;

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
            //if (ObjectList == null)
            //{
            //    return;
            //}
			//新建对象
			Substation_Info obj = new Substation_Info();
            obj.Flag = flags1;
            obj.CreateDate = DateTime.Now;
            //obj.L1 = 100;
            //obj.L2 = 100;
            //obj.L3 = 100;

			//执行添加操作
			using (FrmSubstation_InfoDialog_AHTL dlg = new FrmSubstation_InfoDialog_AHTL())
			{
                dlg.SetVisible();
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.ctrlSubstation_Info = this;
                dlg.ProjectID = projectid;
				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
                    
					return;
				}
			}
            this.bandedGridView1.BeginUpdate();
            CalcTotal();
            //CalcTotal(" order by convert(int,L1) desc,AreaName desc,S4,CreateDate,convert(int,S5) ");
            this.bandedGridView1.EndUpdate();
			//将新对象加入到链表中
            //ObjectList.Add(obj);

            ////刷新表格，并将焦点行定位到新对象上。
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.bandedGridView1, obj);
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
			Substation_Info obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.S4 == "no")
            { MessageBox.Show("此行不能修改!"); return; }
			//创建对象的一个副本
			Substation_Info objCopy = new Substation_Info();
			DataConverter.CopyTo<Substation_Info>(obj, objCopy);

			//执行修改操作
			using (FrmSubstation_InfoDialog_AHTL dlg = new FrmSubstation_InfoDialog_AHTL())
			{
                dlg.SetVisible();
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.ctrlSubstation_Info = this;
                dlg.ProjectID = projectid;
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<Substation_Info>(objCopy, obj);
            this.bandedGridView1.BeginUpdate();
            CalcTotal();
			//刷新表格
			//gridControl.RefreshDataSource();
            this.bandedGridView1.EndUpdate();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			Substation_Info obj = FocusedObject;
			if (obj == null)
			{
				return;
			}
            if (obj.S4 == "no")
            { MessageBox.Show("此行不能删除!","删除"); return; }
			//请求确认
			if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
			{
				return;
			}

			//执行删除操作
			try
			{
				Services.BaseService.Delete<Substation_Info>(obj);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

			this.bandedGridView1.BeginUpdate();
			//记住当前焦点行索引
            //int iOldHandle = this.bandedGridView1.FocusedRowHandle;
            ////从链表中删除
            //ObjectList.Remove(obj);
            ////刷新表格
            //gridControl.RefreshDataSource();
            ////设置新的焦点行索引
            //GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
            CalcTotal();
			this.bandedGridView1.EndUpdate();
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
             object dr = this.bandedGridView1.GetRow(e.RowHandle);
            Brush brush = null;
            Rectangle r = e.Bounds;
            Color c1 = Color.FromArgb(175, 238, 238);
            Color c2 = Color.FromArgb(175, 238, 238);
            Color c3 = Color.FromArgb(245, 222, 180);
            Color c4 = Color.FromArgb(245, 222, 180);
           
            if (dr == null)
                return;
            int tempsum=0;
            if (bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[0]) != null)
            {
                if (Findstr(bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[0]).ToString()))
                {
                    brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c1, c2, 180);
                    e.Graphics.FillRectangle(brush, r);
                }
                else if (!int.TryParse(bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[0]).ToString(),out  tempsum))
                {
                    if (e.CellValue!=null&&bandedGridView1.GetRowCellValue(e.RowHandle, bandedGridView1.Columns[0]).ToString() == e.CellValue.ToString())
                    {
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                        e.Graphics.FillRectangle(brush, r);
                    }
                   
                }
            }
          
           
        }
	}
}
