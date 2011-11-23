
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
using Itop.Domain.Layouts;
#endregion

namespace Itop.Client.Stutistics
{
    public partial class CtrlSubstation_Info_LangFang : UserControl
    {

        private string types1 = "";
        private string types2 = "";
        private string flags1 = "";
        private string xmlflag = "";
        private string xmltype = "";
        private string flags2 = "";
        public bool editright = true; //修改标志
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
        public string Flag2
        {
            get { return flags2; }
            set { flags2 = value; }
        }

        public bool IsSelect
        {
            get { return isselect; }
            set { isselect = value; }
        }

        public string XmlFlag
        {
            get { return xmlflag; }
            set { xmlflag = value; }
        }
        public string XmlType
        {
            get { return xmltype; }
            set { xmltype = value; }
        }
        bool isselect = false;


        #region 构造方法
        public CtrlSubstation_Info_LangFang()
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
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag", flags1);
                int i = 1;
                foreach (Substation_Info info in list)
                {
                    info.L23 = i.ToString();
                    i++;
                    double? l2 = info.L2;
                    double? l9 = info.L9;
                    double? l10 = info.L10;

                    try{
                        if(l2!=0)
                        {
                            l10 = l9 / l2;
                            info.L10 = l10;
                        }

                    }catch{}








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


        /// <summary>
        /// 刷新表格中的数据
        /// </summary>
        /// <returns>ture:成功  false:失败</returns>
        public bool RefreshData1()
        {
            try
            {
                string filepath = "";
                Econ ec = new Econ();
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag", flags1);
                if (xmlflag == "guihua")
                    ec.UID = xmltype;
                else
                {
                    ec.UID = xmltype + "SubstationLayOut";
                }

                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                if (listxml.Count != 0)
                {
                    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                    this.bandedGridView1.RestoreLayoutFromStream(ms);
                }
                int i = 1;
                foreach (Substation_Info info in list)
                {
                    info.L23 = i.ToString();
                    i++;
                    double? l2 = info.L2;
                    double? l9 = info.L9;
                    double? l10 = info.L10;

                    try
                    {
                        if (l2 != 0)
                        {
                            l10 = l9 / l2;
                            info.L10 = l10;
                        }

                    }
                    catch { }
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
        public void RefreshLayout()
        {
            try
            {
                string filepath = "";
                Econ ec = new Econ();
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag", flags1);
                if (xmlflag == "guihua")
                    ec.UID = xmltype;
                else
                {
                    ec.UID = xmltype + "SubstationLayOut";
                }

                IList<Econ> listxml = Services.BaseService.GetList<Econ>("SelectEconByKey", ec);
                if (listxml.Count != 0)
                {
                    MemoryStream ms = new MemoryStream(listxml[0].ExcelData);
                    this.bandedGridView1.RestoreLayoutFromStream(ms);
                }
            }
            catch { }
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
            Substation_Info obj = new Substation_Info();
            obj.Flag = flags1;
            obj.CreateDate = DateTime.Now;
            //obj.L1 = 100;
            //obj.L2 = 100;
            //obj.L3 = 100;

            //执行添加操作
            using (FrmSubstation_Info_LangFangDialog dlg = new FrmSubstation_Info_LangFangDialog())
            {
                dlg.Type = types1;
                dlg.Flag = flags2;
                dlg.Type2 = types2;
                dlg.ctrlSubstation_Info = this;

                dlg.IsCreate = true;    //设置新建标志
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {

                    return;
                }
            }

            //将新对象加入到链表中
            ObjectList.Add(obj);
            int i = 1;
            foreach (Substation_Info info in ObjectList)
            {
                info.L23 = i.ToString();
                i++;
            }
            //刷新表格，并将焦点行定位到新对象上。
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.bandedGridView1, obj);
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

            //创建对象的一个副本
            Substation_Info objCopy = new Substation_Info();
            DataConverter.CopyTo<Substation_Info>(obj, objCopy);

            //执行修改操作
            using (FrmSubstation_Info_LangFangDialog dlg = new FrmSubstation_Info_LangFangDialog())
            {
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags2;
                dlg.Type2 = types2;
                dlg.ctrlSubstation_Info = this;

                dlg.Object = objCopy;   //绑定副本
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //用副本更新焦点对象
            DataConverter.CopyTo<Substation_Info>(objCopy, obj);
            //刷新表格
            gridControl.RefreshDataSource();
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
            int iOldHandle = this.bandedGridView1.FocusedRowHandle;
            //从链表中删除
            ObjectList.Remove(obj);
            //刷新表格
            int i = 1;
            foreach (Substation_Info info in ObjectList)
            {
                info.L23 = i.ToString();
                i++;
            }
            gridControl.RefreshDataSource();
            //设置新的焦点行索引
            GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
            this.bandedGridView1.EndUpdate();
        }
        #endregion
    }
}
