
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
#endregion

namespace Itop.TLPsp
{
	public partial class CtrlSubstation_Info : UserControl
	{

        private string types1 = "";
        private string flags1 = "";
        public string xmlflag = "";
        public bool editright = true;
        public List<int> row_num = new List<int>();   //��¼Ҫ���б�ɫ������
        public bool kekaoflag = false;                //����ǿɿ��Է�����ִ�д˲���
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


		#region ���췽��
		public CtrlSubstation_Info()
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
        private string projectid;
        public string ProjectID
        {
            set { projectid = value; }
        }
		/// <summary>
		/// ˢ�±���е�����
		/// </summary>
		/// <returns>ture:�ɹ�  false:ʧ��</returns>
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


        /// <summary>
        /// ˢ�±���е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
        public bool RefreshData1()
        {
            try
            {
                 string filepath="";
               // IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByFlag",flags1);
                string con = "Flag<='" + flags1 + "' and AreaID='" + projectid + "'";
                IList<Substation_Info> list = Services.BaseService.GetList<Substation_Info>("SelectSubstation_InfoByCon", con);
                //for (int i = 0; i < list.Count;i++ )
                //{
                //    Substation_Info sb = (Substation_Info)list[i];
                //    if (sb.L2!=0||sb.L2!=null)
                //    {
                //        sb.L10 =Convert.ToDouble(Convert.ToDouble(sb.L9)/sb.L2* 100);
                //        //sb.GetType().GetProperty("L10").SetValue(sb, LL10, null);
                        
                //    }
                    
                //}
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
			using (FrmSubstation_InfoDialog dlg = new FrmSubstation_InfoDialog())
			{
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.ctrlSubstation_Info = this;
                dlg.ProjectID = projectid;
				dlg.IsCreate = true;    //�����½���־
				dlg.Object = obj;
				if (dlg.ShowDialog() != DialogResult.OK)
				{
                    
					return;
				}
			}

			//���¶�����뵽������
			ObjectList.Add(obj);

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
			using (FrmSubstation_InfoDialog dlg = new FrmSubstation_InfoDialog())
			{
                dlg.IsSelect = isselect;
                dlg.Type = types1;
                dlg.Flag = flags1;
                dlg.ctrlSubstation_Info = this;
                dlg.ProjectID = projectid;
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
			gridControl.RefreshDataSource();
			//�����µĽ���������
			GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
			this.bandedGridView1.EndUpdate();
		}
		#endregion

        private void bandedGridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (kekaoflag)
            {
                Brush brush = null;
                Rectangle r = e.Bounds;
                //int year = 0;
                Color c1 = Color.FromArgb(255, 121, 121);
                Color c2 = Color.FromArgb(255, 121, 121);
                Color c3 = Color.FromArgb(152, 122, 254);
                Color c4 = Color.FromArgb(152, 122, 254);
                foreach (int i in row_num)
                {
                    if (i == e.RowHandle)
                    {
                        object dr = this.bandedGridView1.GetRow(e.RowHandle);
                        if (dr == null)
                            return;
                        Substation_Info bl = (Substation_Info)dr;
                        brush = new System.Drawing.Drawing2D.LinearGradientBrush(e.Bounds, c3, c4, 180);
                        e.Graphics.FillRectangle(brush, r);
                            
                       
                    }
                }
            }    
           
        }
	}
}
