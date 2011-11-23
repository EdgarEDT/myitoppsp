
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
using System.Collections;
#endregion

namespace Itop.Client.Table
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

        public void Calc(string addConn)
        {
            int x5 = 1, x1 = 1, x2 = 1, x1z = 1, x35 = 1, x10 = 1, x6 = 1, t5 = 0, t2 = 0, t1 = 0, t1z = 0, t35 = 0, t10 = 0, t6 = 0;
            double h5 = 0, h1 = 0, h2 = 0, h1z = 0, h35 = 0,h10=0,h6=0, z5 = 0, z1 = 0, z2 = 0, z1z = 0, z35 = 0,z10=0,z6=0;
            int index5 = -1, index2 = -1, index1 = -1, index1z = -1, index35 = -1,index10=-1,index6=-1;
            Hashtable table = new Hashtable();
            bool one = true, five = true, two = true, onez = true, three = true,ten=true,six=true;
            string area = "1@3$5q99z99";
            int j = 0;
            int now = 0;
            string con = "AreaID='" + projectid + "'";
            con += addConn;
            con += " order by convert(int,L1) desc,S4,AreaName,CreateDate,convert(int,S5)";
            string[] que = new string[60] { "һ", "��", "��", "��", "��", "��", "��", "��", "��", "ʮ", 
            "ʮһ","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��",
            "��ʮ��","��ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��",
            "��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ","��ʮһ","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ��","��ʮ"};
            //IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByCon", con);
            IList list = Common.Services.BaseService.GetList("SelectSubstation_InfoByWhere", con);
            string conn = "L1=110";
            // IList groupList = Common.Services.BaseService.GetList("SelectAreaNameGroupByConn", conn);
            IList<string> groupList = new List<string>();
            Hashtable table2 = new Hashtable();
            IList<string> groupList2 = new List<string>();
            string area2 = "1@3$5q99z99";
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
                    try
                    {
                       
                        t5 += (int)((Substation_Info)list[i]).L3;
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
                    try
                    {
                       
                        t2 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x2++;
                }
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 == "ר��")
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
                else if (((Substation_Info)list[i]).L1 == 110 && ((Substation_Info)list[i]).S4 != "ר��")
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
                    try
                    {
                       
                        t35 += (int)((Substation_Info)list[i]).L3;
                    }
                    catch { }
                    x35++;
                }
                else if (((Substation_Info)list[i]).L1 == 10)
                {
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
                else if (((Substation_Info)list[i]).L1 == 6)
                {
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
                info.Title = "500ǧ��";
                info.L2 = h5;
                info.L5 = z5.ToString();
                info.L3 = t5;
                info.L1 = 500;
                info.S4 = "no";
                list.Insert(index5, info);//.Add(info);
                now++;
            }
            if (x2 > 1)
            {
                Substation_Info info2 = new Substation_Info();
                info2.S3 = que[j];
                j++;
                info2.Title = "220ǧ��";
                info2.L2 = h2;
                info2.L5 = z2.ToString();
                info2.L3 = t2;
                info2.L1 = 220;
                info2.S4 = "no";
                list.Insert(index2 + now, info2);
                now++;
            }
            if (x1 > 1)
            {
                Substation_Info info1 = new Substation_Info();
                info1.S3 = que[j];
                j++;
                info1.Title = "110ǧ������";
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
                    conn = "L1=110 and AreaID='" + projectid + "' and  AreaName='" + groupList[k] + "'  and S4!='ר��'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
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
                info1z.Title = "110ǧ��ר��";
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
                    conn = "L1=110 and AreaID='" + projectid + "' and  AreaName='" + groupList2[k] + "' and S4='ר��'";
                    IList temList = Common.Services.BaseService.GetList("SelectSumSubstation_InfoByConn", conn);
                    infok.L2 = ((Substation_Info)temList[0]).L2;
                    infok.L5 = ((Substation_Info)temList[0]).L5;
                    infok.L1 = 110;
                    infok.S4 = "no";
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
                info35.Title = "35ǧ��";
                info35.L2 = h35;
                info35.L5 = z35.ToString();
                info35.L3 = t35;
                info35.L1 = 35;
                info35.S4 = "no";
                list.Insert(index35 + now, info35);
                now++;
            }
            if (x10 > 1)
            {
                Substation_Info info10 = new Substation_Info();
                info10.S3 = que[j];
                j++;
                info10.Title = "10ǧ��";
                info10.L2 = h10;
                info10.L5 = z10.ToString();
                info10.L3 =t10;
                info10.L1 = 10;
                info10.S4 = "no";
                list.Insert(index10 + now, info10);
                now++;
            }
            if (x6 > 1)
            {
                Substation_Info info6 = new Substation_Info();
                info6.S3 = que[j];
                j++;
                info6.Title = "6ǧ��";
                info6.L2 = h6;
                info6.L5 = z6.ToString();
                info6.L3 = t6;
                info6.L1 = 6;
                info6.S4 = "no";
                list.Insert(index6 + now, info6);
                now++;
            }
            this.gridControl.DataSource = list;
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
        /// ˢ�±���е�����
        /// </summary>
        /// <returns>ture:�ɹ�  false:ʧ��</returns>
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
                info.Title = "�ϼ�";
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
		/// ��Ӷ���
		/// </summary>
		public void AddObject()
		{
			//�����������Ƿ��Ѿ�����
            //if (ObjectList == null)
            //{
            //    return;
            //}
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
                dlg.SetVisible();
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
            this.bandedGridView1.BeginUpdate();
            CalcTotal();
            //CalcTotal(" order by convert(int,L1) desc,AreaName desc,S4,CreateDate,convert(int,S5) ");
            this.bandedGridView1.EndUpdate();
			//���¶�����뵽������
            //ObjectList.Add(obj);

            ////ˢ�±�񣬲��������ж�λ���¶����ϡ�
            //gridControl.RefreshDataSource();
            //GridHelper.FocuseRow(this.bandedGridView1, obj);
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
            if (obj.S4 == "no")
            { MessageBox.Show("���в����޸�!"); return; }
			//���������һ������
			Substation_Info objCopy = new Substation_Info();
			DataConverter.CopyTo<Substation_Info>(obj, objCopy);

			//ִ���޸Ĳ���
			using (FrmSubstation_InfoDialog dlg = new FrmSubstation_InfoDialog())
			{
                dlg.SetVisible();
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
            this.bandedGridView1.BeginUpdate();
            CalcTotal();
			//ˢ�±��
			//gridControl.RefreshDataSource();
            this.bandedGridView1.EndUpdate();
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
            if (obj.S4 == "no")
            { MessageBox.Show("���в���ɾ��!","ɾ��"); return; }
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
            //int iOldHandle = this.bandedGridView1.FocusedRowHandle;
            ////��������ɾ��
            //ObjectList.Remove(obj);
            ////ˢ�±��
            //gridControl.RefreshDataSource();
            ////�����µĽ���������
            //GridHelper.FocuseRowAfterDelete(this.bandedGridView1, iOldHandle);
            CalcTotal();
			this.bandedGridView1.EndUpdate();
		}
		#endregion

        private void gridControl_Click(object sender, EventArgs e)
        {

        }
	}
}
