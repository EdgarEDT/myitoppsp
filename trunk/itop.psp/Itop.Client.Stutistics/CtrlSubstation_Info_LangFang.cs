
#region ���������ռ�
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
        public bool editright = true; //�޸ı�־
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


        #region ���췽��
        public CtrlSubstation_Info_LangFang()
        {
            InitializeComponent();
        }
        #endregion

        #region �ֶ�
        protected bool _bAllowUpdate = true;
        #endregion

        #region ��������
        /// <summary>
        /// ��ȡ������"˫�������޸�"��־
        /// </summary>
        public bool AllowUpdate
        {
            get { return _bAllowUpdate; }
            set { _bAllowUpdate = value; }
        }

        /// <summary>
        /// ��ȡGridControl����
        /// </summary>
        public GridControl GridControl
        {
            get { return gridControl; }
        }

        /// <summary>
        /// ��ȡbandedGridView1����
        /// </summary>
        public BandedGridView GridView
        {
            get { return bandedGridView1; }
        }

        /// <summary>
        /// ��ȡGridControl������Դ����������б�
        /// </summary>
        public IList<Substation_Info> ObjectList
        {
            get { return this.gridControl.DataSource as IList<Substation_Info>; }
        }

        /// <summary>
        /// ��ȡ������󣬼�FocusedRow
        /// </summary>
        public Substation_Info FocusedObject
        {
            get { return this.bandedGridView1.GetRow(this.bandedGridView1.FocusedRowHandle) as Substation_Info; }
        }
        #endregion

        #region �¼�����
        private void bandedGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (!editright)
                return;
            // �ж�"˫�������޸�"��־ 
            if (!AllowUpdate)
            {
                return;
            }

            //���������ڵ�Ԫ���У���༭�������
            Point point = this.gridControl.PointToClient(Control.MousePosition);
            if (GridHelper.HitCell(this.bandedGridView1, point))
            {
                UpdateObject();
            }

        }
        #endregion

        #region ��������
        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        public void PrintPreview()
        {
            ComponentPrint.ShowPreview(this.gridControl, this.bandedGridView1.GroupPanelText);
        }

        /// <summary>
        /// ˢ�±���е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
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
        /// ˢ�±���е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
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
        /// ��Ӷ���
        /// </summary>
        public void AddObject()
        {
            //�����������Ƿ��Ѿ�����
            if (ObjectList == null)
            {
                return;
            }
            //�½�����
            Substation_Info obj = new Substation_Info();
            obj.Flag = flags1;
            obj.CreateDate = DateTime.Now;
            //obj.L1 = 100;
            //obj.L2 = 100;
            //obj.L3 = 100;

            //ִ����Ӳ���
            using (FrmSubstation_Info_LangFangDialog dlg = new FrmSubstation_Info_LangFangDialog())
            {
                dlg.Type = types1;
                dlg.Flag = flags2;
                dlg.Type2 = types2;
                dlg.ctrlSubstation_Info = this;

                dlg.IsCreate = true;    //�����½���־
                dlg.Object = obj;
                if (dlg.ShowDialog() != DialogResult.OK)
                {

                    return;
                }
            }

            //���¶�����뵽������
            ObjectList.Add(obj);
            int i = 1;
            foreach (Substation_Info info in ObjectList)
            {
                info.L23 = i.ToString();
                i++;
            }
            //ˢ�±�񣬲��������ж�λ���¶����ϡ�
            gridControl.RefreshDataSource();
            GridHelper.FocuseRow(this.bandedGridView1, obj);
        }

        /// <summary>
        /// �޸Ľ������
        /// </summary>
        public void UpdateObject()
        {
            //��ȡ�������
            Substation_Info obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //���������һ������
            Substation_Info objCopy = new Substation_Info();
            DataConverter.CopyTo<Substation_Info>(obj, objCopy);

            //ִ���޸Ĳ���
            using (FrmSubstation_Info_LangFangDialog dlg = new FrmSubstation_Info_LangFangDialog())
            {
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags2;
                dlg.Type2 = types2;
                dlg.ctrlSubstation_Info = this;

                dlg.Object = objCopy;   //�󶨸���
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
            }

            //�ø������½������
            DataConverter.CopyTo<Substation_Info>(objCopy, obj);
            //ˢ�±��
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// ɾ���������
        /// </summary>
        public void DeleteObject()
        {
            //��ȡ�������
            Substation_Info obj = FocusedObject;
            if (obj == null)
            {
                return;
            }

            //����ȷ��
            if (MsgBox.ShowYesNo(Strings.SubmitDelete) != DialogResult.Yes)
            {
                return;
            }

            //ִ��ɾ������
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
            //��ס��ǰ����������
            int iOldHandle = this.bandedGridView1.FocusedRowHandle;
            //��������ɾ��
            ObjectList.Remove(obj);
            //ˢ�±��
            int i = 1;
            foreach (Substation_Info info in ObjectList)
            {
                info.L23 = i.ToString();
                i++;
            }
            gridControl.RefreshDataSource();
            //�����µĽ���������
            GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
            this.bandedGridView1.EndUpdate();
        }
        #endregion
    }
}
