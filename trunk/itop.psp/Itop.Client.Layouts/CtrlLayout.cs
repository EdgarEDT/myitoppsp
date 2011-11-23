
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
using Itop.Domain.Layouts;
using Itop.Domain.RightManager;
using System.Configuration;
using System.Runtime.InteropServices;
#endregion

namespace Itop.Client.Layouts
{
	public partial class CtrlLayout : UserControl
	{
		#region 构造方法
		public CtrlLayout()
		{
			InitializeComponent();
		}
		#endregion

		#region 字段
		protected bool _bAllowUpdate = true;
        private VsmdgroupProg vp = new VsmdgroupProg();
        private bool visu = false;
		#endregion

		#region 公共属性




        string Path = Application.StartupPath + "\\setting.ini";

        [DllImport("kernel32")]

        private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);


        [DllImport("kernel32")]

        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        //写INI文件 
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.Path);

        }

        //读取INI文件指定 
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.Path);
            return temp.ToString();
        } 



        public VsmdgroupProg RightObject
        {
            set { vp = value; }
        }

        public bool Visu
        {
            set { visu = value; }
        }

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
			get { return gridView; }
		}

		/// <summary>
		/// 获取GridControl的数据源，即对象的列表
		/// </summary>
		public IList<Layout> ObjectList
		{
			get { return this.gridControl.DataSource as IList<Layout>; }
		}

		/// <summary>
		/// 获取焦点对象，即FocusedRow
		/// </summary>
		public Layout FocusedObject
		{
			get { return this.gridView.GetRow(this.gridView.FocusedRowHandle) as Layout; }
		}
		#endregion

		#region 事件处理
        //双击修改事件
		private void gridView_DoubleClick(object sender, EventArgs e)
		{
			// 判断"双击允许修改"标志 
            //if (!AllowUpdate)
            //{
            //    return;
            //}

            ////如果鼠标点击在单元格中，则编辑焦点对象。
            //Point point = this.gridControl.PointToClient(Control.MousePosition);
            //if (GridHelper.HitCell(this.gridView, point))
            //{
            //    UpdateObject();
            //}
            if (FocusedObject != null)
            {
                FrmLayoutContents dlg = new FrmLayoutContents();
                dlg.RightObject = vp;
                dlg.LayoutUID = FocusedObject.UID;
                dlg.ShowDialog();
            }

		}
		#endregion

		#region 公共方法
		/// <summary>
		/// 打印预览
		/// </summary>
		public void PrintPreview()
		{
			ComponentPrint.ShowPreview(this.gridControl, this.gridView.GroupPanelText);
		}

		/// <summary>
		/// 刷新表格中的数据
		/// </summary>
		/// <returns>ture:成功  false:失败</returns>
		public bool RefreshData()
		{
			try
			{
                IList<Layout> list = Services.BaseService.GetList<Layout>("SelectLayoutListByCreater",Itop.Client.MIS.ProgUID);
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

		/// <summary>
		/// 添加对象
		/// </summary>
		public void AddObject()
		{
			//检查对象链表是否已经加载
			if (ObjectList == null)
			{
				return;
			}
			//新建对象
			Layout obj = new Layout();
            obj.UID = Guid.NewGuid().ToString() + "|" + vp.ProjectUID;
            obj.Creater = vp.ProjectUID;// Itop.Client.MIS.UserNumber;
            //obj.CreaterName = Itop.Client.MIS.UserName;
            try
            {
                obj.CreateDate = (DateTime)Services.BaseService.GetObject("SelectSysData", null);
            }
            catch { }
			//执行添加操作
			using (FrmLayoutDialog dlg = new FrmLayoutDialog())
			{
				dlg.IsCreate = true;    //设置新建标志
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//将新对象加入到链表中
			ObjectList.Add(obj);
            //////LayoutType lt1 = Services.BaseService.GetOneByKey<LayoutType>(obj.UID);
            //////if (lt1 == null)
            //////{
            //////    lt1 = new LayoutType();
            //////    lt1.UID = obj.UID;
            //////    LayoutType lt2 = Services.BaseService.GetOneByKey<LayoutType>("LayoutModule");
            //////    lt1.ExcelData = lt2.ExcelData;
            //////    Services.BaseService.Create<LayoutType>(lt1);
            //////}




			//刷新表格，并将焦点行定位到新对象上。
			gridControl.RefreshDataSource();
			GridHelper.FocuseRow(this.gridView, obj);
		}

		/// <summary>
		/// 修改焦点对象
		/// </summary>
		public void UpdateObject()
		{
			//获取焦点对象
			Layout obj = FocusedObject;
			if (obj == null)
			{
				return;
			}

			//创建对象的一个副本
			Layout objCopy = new Layout();
			DataConverter.CopyTo<Layout>(obj, objCopy);

			//执行修改操作
			using (FrmLayoutDialog dlg = new FrmLayoutDialog())
			{
				dlg.Object = objCopy;   //绑定副本
				if (dlg.ShowDialog() != DialogResult.OK)
				{
					return;
				}
			}

			//用副本更新焦点对象
			DataConverter.CopyTo<Layout>(objCopy, obj);
			//刷新表格
			gridControl.RefreshDataSource();
		}

		/// <summary>
		/// 删除焦点对象
		/// </summary>
		public void DeleteObject()
		{
			//获取焦点对象
			Layout obj = FocusedObject;
			if (obj == null)
			{
				return;
			}



            IList<LayoutContent> Li=  Services.BaseService.GetList<LayoutContent>("SelectLayoutContentByLayoutID",obj.UID);

            if (Li.Count > 0)
            {
                MsgBox.Show("该规划下面已经存在章节，无法删除");
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
				Services.BaseService.Delete<Layout>(obj);


                Services.BaseService.Update("DeleteLayoutType", obj.UID);
			}
			catch (Exception exc)
			{
				Debug.Fail(exc.Message);
				HandleException.TryCatch(exc);
				return;
			}

			this.gridView.BeginUpdate();
			//记住当前焦点行索引
			int iOldHandle = this.gridView.FocusedRowHandle;
			//从链表中删除
			ObjectList.Remove(obj);
			//刷新表格
			gridControl.RefreshDataSource();
			//设置新的焦点行索引
			GridHelper.FocuseRowAfterDelete(this.gridView, iOldHandle);
			this.gridView.EndUpdate();
		}
		#endregion

      

        private void gridControl_Load(object sender, EventArgs e)
        {
            //RefreshData();

            if (!visu)
            {
                colCreater.Visible = false;
                colRemark.Visible = false;
            
            }


            try 
            { 
                //string fileName = Application.StartupPath + @"\Itop.exe.config";
                //if (!System.IO.File.Exists(fileName)) return;

                

                //Configuration config = null;
                //config = ConfigurationManager.OpenExeConfiguration(fileName);

                //string serverAddress = config.AppSettings.Settings["serverAddress"].Value;
                //string serverPort = config.AppSettings.Settings["serverPort"].Value;
                //string serverProtocol = config.AppSettings.Settings["serverProtocol"].Value;

                Itop.Server.Interface.IConfigService ic = RemotingHelper.GetRemotingService<Itop.Server.Interface.IConfigService>();
                Itop.Domain.DataConfig dc = ic.GetDataConfig();

                IniWriteValue("数据库", "连接", "Provider=SQLOLEDB.1;Password=" + dc.Password + ";Persist Security Info=True;User ID=" + dc.Userid + ";Initial Catalog=" + dc.Database + ";Data Source=" + dc.Datasource + ";");
                ;

            }
            catch { }
        }
	}
}
