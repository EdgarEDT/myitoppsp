
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

namespace Itop.Client.Stutistics
{
	public partial class CtrlSubstation_Info : UserControl
	{

        private string types1 = "";
        private string flags1 = "";
        public string xmlflag = "";
        public bool editright = true;
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
		public CtrlSubstation_Info()
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

        public void CalcTotal()
        {
            int x5 = 1, x1 = 1, x2 = 1,x1z=1,x35=1;
            double h5 = 0, h1 = 0, h2 = 0,h1z=0,h35=0, z5 = 0, z1 = 0, z2 = 0,z1z=0,z35=0;
            int index5 = -1, index2 = -1, index1 = -1,index1z=-1,index35=-1;
            Hashtable table = new Hashtable();
            bool one = true,five=true,two=true,onez=true,three=true;
            string area = "";
            int j = 0;
            int now = 0;
            string con = "Flag='"+flags1+"' and AreaID='"+projectid+"'";
            string[] que = new string[60] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十", 
            "十一","十二","十三","十四","十五","十六","十七","十八","十九","二十","二十一","二十二","二十三","二十四","二十五","二十六","二十七",
            "二十八","二十九","三十","三十一","三十二","三十三","三十四","三十五","三十六","三十七","三十八","三十九","四十","四十一","四十二","四十三","四十四",
            "四十五","四十六","四十七","四十八","四十九","五十","五十一","五十二","五十三","五十四","五十五","五十六","五十七","五十八","五十九","六十"};
            IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByCon", con);
            string conn = "L1=110";
           // IList groupList = Common.Services.BaseService.GetList("SelectAreaNameGroupByConn", conn);
            IList<string> groupList = new List<string>();
            for (int i = 0; i < list.Count; i++)
            {
                if (((Substation_Info)list[i]).L1 == 500)
                {
                    if (five)
                    { 
                        index5 = i;
                        five = false;
                    }
                    ((Substation_Info)list[i]).S3 = x5.ToString();
                    h5 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z5 += double.Parse(((Substation_Info)list[i]).L5);
                    }
                    catch { }
                    x5++;
                }
                else if (((Substation_Info)list[i]).L1 == 220)
                {
                    if (two)
                    { index2 = i; two = false; }
                    ((Substation_Info)list[i]).S3 = x2.ToString();
                    h2 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z2 += double.Parse(((Substation_Info)list[i]).L5);
                    }
                    catch { }
                    x2++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 == "专用")
                {
                    if (onez)
                    { index1z = i; onez = false; }
                    ((Substation_Info)list[i]).S3 = x1z.ToString();
                    h1z += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z1z += double.Parse(((Substation_Info)list[i]).L5);
                    }
                    catch { }
                    x1z++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 != "专用")
                {
                    if (((Substation_Info)list[i]).AreaName != area)
                    {
                        table.Add(((Substation_Info)list[i]).AreaName, i);
                        groupList.Add(((Substation_Info)list[i]).AreaName);
                        //  table[((Substation_Info)list[i]).AreaName] = i;
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
                    x1++;
                }
                else if (((Substation_Info)list[i]).L1 == 35)
                {
                    if (three)
                    { index35 = i; three = false; }
                    ((Substation_Info)list[i]).S3 = x35.ToString();
                    h35 += ((Substation_Info)list[i]).L2;
                    try
                    {
                        z35 += double.Parse(((Substation_Info)list[i]).L5);
                    }
                    catch { }
                    x35++;
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
                info.L1 = 500;
                info.S4 = "no";
                list.Insert(index5,info);//.Add(info);
                now++;
            }
            if (x2 > 1)
            {
                Substation_Info info2 = new Substation_Info();
                info2.S3 = que[j];
                j++;
                info2.Title = "220千伏";
                info2.L2 = h2;
                info2.L5 = z2.ToString();
                info2.L1 = 220;
                info2.S4 = "no";
                list.Insert(index2+now, info2);
                now++;
            }
            if (x1 > 1)
            {
                Substation_Info info1 = new Substation_Info();
                info1.S3 = que[j];
                j++;
                info1.Title = "110千伏公变";
                info1.L2 = h1;
                info1.L5 = z1.ToString();
                info1.L1 = 110;
                info1.S4 = "no";
                list.Insert(index1+now, info1);
                now++;
                for(int k=0;k<groupList.Count;k++)
                {
                    Substation_Info infok = new Substation_Info();
                    infok.S3 = Convert.ToChar(k+65).ToString().ToLower();
                    infok.Title=groupList[k];
                    conn="L1=110 and AreaName='"+groupList[k]+"'";
                    IList temList=Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn",conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
                    list.Insert(int.Parse(table[groupList[k]].ToString())+now, infok);
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
                info1z.L1 = 110;
                info1z.S4 = "no";
                list.Insert(index1z + now, info1z);
                now++;
            }
            if (x35 > 1)
            {
                Substation_Info info35 = new Substation_Info();
                info35.S3 = que[j];
                j++;
                info35.Title = "35千伏";
                info35.L2 = h35;
                info35.L5 = z35.ToString();
                info35.L1 = 35;
                info35.S4 = "no";
                list.Insert(index35 + now, info35);
                now++;
            }
            this.gridControl.DataSource = list;
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
                 string con = "Flag='" + flags1 + "' and AreaID='" + projectid + "'";
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon",con);
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

                foreach (GridColumn gc in this.bandedGridView1.Columns)
                {
                    gc.Visible = false;
                    gc.OptionsColumn.ShowInCustomizationForm = false;
                    if (gc.FieldName == "Title" || gc.FieldName == "L9" || gc.FieldName == "L2" || gc.FieldName == "L1" || gc.FieldName == "L10")
                    {
                        gc.Visible = true;
                        gc.OptionsColumn.ShowInCustomizationForm = true;
                    }


                    //if (gc.FieldName.Substring(0, 1) == "S")
                    //{
                    //    gc.Visible = false;
                    //    gc.OptionsColumn.ShowInCustomizationForm = false;
                    //}
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
			using (FrmSubstation_InfoDialog dlg = new FrmSubstation_InfoDialog())
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
			using (FrmSubstation_InfoDialog dlg = new FrmSubstation_InfoDialog())
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
	}
}
