using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
using Itop.Domain.Forecast;
using Itop.Common;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Drawing.Drawing2D;
namespace Itop.Client.History
{
    public partial class FormHistoryType : FormBase
    {

        public int Forecast=0;
        public string ForecastID = string.Empty;
        private TreeListNode CurrentNode = null;
        private string TypeFlag = string.Empty;
        private string OldId = string.Empty;
        private string _flag="";
        private string _title;
        private string _flagvalue;
        string   ProjectID = MIS.ProgUID;
        public string AreaId =string.Empty;
        public string AreaName = string.Empty;
        //�����ж��ӽ���ֵ�Ƿ�ı����
        //���ڵ�һ�δ��б�ȡֵ�ж�
        private bool CheckEnd = false;
        private IList<Ps_HistoryType> P_HTypelist = null;
        //�������ģ�鴫����IDֵ�����ڶԱ�����Ƿ�����
        public List<string> ValueList = new List<string>();
        //��λ�б�
        public List<string> UnitsList = new List<string>();

        //public List<string> TypeList = new List<string>();
        //��ѡ��Ҫ��ӵ��������
        public List<string> AddList = new List<string>();
        //��ѡ��Ҫ��ӵ����ID
        public List<string> AddListID = new List<string>();
        //Ҫ���ٵ��������
        public List<string> ReduceList = new List<string>();
        //��ѡ��Ҫ��ӵ����ID
        public List<string> ReduceListID = new List<string>();
        //�ı䵥λ���������
        public List<string> changeUnitlist = new List<string>();
        //�ı䵥λ�����ID
        public List<string> changeUnitlistID = new List<string>();
        DataTable dataTable = new DataTable();
        /// <summary>
        /// flag="" ��ʾ����Աʹ��
        /// flag="1" ��ʾ����ʵ��ʹ��
        /// flag="2" ��ʾ��������ʵ��ʹ��
        /// flag="3" ��ʾ�����õ����ʹ��
        /// </summary>
        public string Flag
        {
            get { return _flag; }
            set { _flag = value; }
        }
        //public string Title
        //{
        //    get
        //    {
        //        if (Flag=="1")
        //        {
        //                _title = "������չʵ��Ĭ��������";
        //        }
        //        else if (Flag=="2")
        //        {
        //            _title = "�����ع���ʵ��Ĭ��������";
        //        }
        //        else if (Flag=="3")
        //        {
        //            _title = "���� "+AreaName+" �õ����Ĭ��������";
        //        }
        //        else if (Flag == "4")
        //        {
        //            _title = "�������ݼ�Ĭ��������";
        //        }
        //        else if (Flag == "5")
        //        {
        //            _title = "�������ݼ�Ĭ��������";
        //        }
        //        else if (Flag == "6")
        //        {
        //            _title = "�������ݼ�Ĭ��������";
        //        }
        //        else
        //        {
        //            _title = "������";
        //        }
        //        return _title;
        //    }
        //    set { _title = value; }
        //}
        public string Title
        {
            get
            {
                if (Flag == "1")
                {
                    _title = "������չʵ��Ĭ��������";
                }
                else if (Flag == "2")
                {
                    _title = "�������������Ĭ��������";
                }
                else if (Flag == "3")
                {
                    _title =  AreaName + " ���û�����Ĭ��������";
                }
                else if (Flag == "4")
                {
                    _title = "�����˿�����Ĭ��������";
                }
                else if (Flag == "5")
                {
                    _title = "�������ݼ�Ĭ��������";
                }
                else if (Flag == "6")
                {
                    _title = "�������ݼ�Ĭ��������";
                }
                else
                {
                    _title = "������";
                }
                return _title;
            }
            set { _title = value; }
        }
        /// <summary>
        /// 1Ϊ����ʵ��
        /// 2Ϊ��������ʵ��
        /// 3Ϊ�����õ����
        /// </summary>
        public string FlagValue
        {
            get { return _flagvalue; }
            set { _flagvalue = value; }
        }
       // private string[] str ={ "������չʵ��", "��������ʵ��","�����õ����" ,"��������","��������","��������"};
        private string[] str = { "�������������", "���û�����", "�����˿�����", "��������", "��������" };
        public FormHistoryType()
        {
          
            InitializeComponent();
            treeList1.Columns["nowunits"].VisibleIndex = -1;
          
        }
        public FormHistoryType(string flag)
        {
            InitializeComponent();
            Flag = flag;
            FlagValue = flag;
            treeList1.Columns["Units"].VisibleIndex = -1;
           
        }
        
        //���ģ����������б�
        private void BarEditItem1_Add(string[] str)
        {
            if (Flag!="")
            {
                return;
            }
            for (int i = 0; i < str.Length; i++)
            {
                ((DevExpress.XtraEditors.Repository.RepositoryItemComboBox)combtype.Edit).Items.Add(str[i].ToString());
            }
            combtype.EditValue = str[0].ToString();
        }
        private void FormHistoryType_Load(object sender, EventArgs e)
        {
            //�������õ������������ID������ĿID�������ж������ʱ���ID�����ظ�
            if (Flag == "3")
            {
                ProjectID = AreaId;
            }
            this.Text = Title;
            BarEditItem1_Add(str);
            ButtonShoworHide();
            ButtonShow();
            LoadData();
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        private void ButtonShow()
        {
            if (!AddRight)
            {
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!EditRight)
            {
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            if (!DeleteRight)
            {
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
        }
        //����ʹ�����ȷ����ʾ��Щ��ť
        private void ButtonShoworHide()
        {
            if (Flag=="")
            {
                treeList1.StateImageList = null;
                barButtonItem12.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else
            {
                barButtonItem5.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem6.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem7.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem8.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem9.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barButtonItem10.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                barStaticItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            
                combtype.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }

        }
        //��������
        private void LoadData()
        {
            
            string connstr = "  Flag='" + FlagValue + "'  Order by Sort";
            P_HTypelist = Common.Services.BaseService.GetList<Ps_HistoryType>("SelectPs_HistoryTypeBYconn", connstr);
            dataTable = Itop.Common.DataConverter.ToDataTable((IList)P_HTypelist, typeof(Ps_HistoryType));
           //���ǹ���Ա�������й���(������ʵ������������õ��������ʱ)
         
            if (Flag!="")
            {
                CheckState check = CheckState.Unchecked;
                //��ӵ�λ��Ϊ�жϵ�λ�仯�����������λ��
                dataTable.Columns.Add("nowunits", typeof(string));
                dataTable.Columns.Add("oldunits", typeof(string));
                //���ѡ�У�Ϊ�жϱ仯������������ѡ��
                dataTable.Columns.Add("check", typeof(CheckState));
                dataTable.Columns.Add("oldcheck", typeof(CheckState));

                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    //����ѡ�б�����ֵͬ
                    dataTable.Rows[i]["check"] = ISchecked(ValueList, dataTable.Rows[i]["ID"].ToString(), dataTable.Rows[i]["TypeName"].ToString());
                    dataTable.Rows[i]["oldcheck"] = ISchecked(ValueList, dataTable.Rows[i]["ID"].ToString(), dataTable.Rows[i]["TypeName"].ToString());
                    if ((CheckState)dataTable.Rows[i]["check"] != CheckState.Unchecked)
                    {
                        //�����ѡ�еģ�����ӵ�λ��������λ����ֵͬ
                        dataTable.Rows[i]["nowunits"] = FindUnits(dataTable.Rows[i]["ID"].ToString());
                        dataTable.Rows[i]["oldunits"] = FindUnits(dataTable.Rows[i]["ID"].ToString());
                    }
                }
            }
           
            treeList1.DataSource = dataTable;
            if (OldId!=string.Empty)
            {
                FindCurrentNodeById(treeList1.Nodes, OldId);
            }
           
        }
        //����ID �ҵ���λ,����Ӧ�� �����+"("+��λ+")"�����ص�λ
        private string FindUnits(string ID)
        {
            Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID + "|" + ProjectID);
            string tempstr = String.Empty;
            if (ph!=null)
            {
                int starts = ph.Title.IndexOf("(");
                if (starts>0)
                {
                    return ph.Title.Substring(starts + 1, ph.Title.Length - starts - 2);
                }
                else
                {
                    return tempstr;
                }
            }
            else
            {
                return tempstr;
            }
        }
        
        /// <summary>
        /// ����ID�����������Ƿ�ѡ��״̬
        /// </summary>
        /// <param name="str">����ID�б�</param>
        /// <param name="ID">Ҫ�жϵ�ID</param>
        /// <param name="TypeName">Ҫ�жϵ��������</param>
        /// <returns></returns>
        private CheckState ISchecked(List<string> str, string ID,string TypeName)
        {
            //Ҷ�ӽ��
            if ((int)Common.Services.BaseService.GetObject("SelectPs_HistoryTypeCountBycon", " ParentID='" + ID + "' and Flag='" + FlagValue + "'") == 0)
            {
                bool checkflag = false;
                for (int j = 0; j < str.Count; j++)
                {
                    if (ID + "|" + ProjectID == str[j].ToString())
                    {
                        checkflag = true;
                        break;
                    }
                }
                if (checkflag)
                {
                    return CheckState.Checked;
                }
                else
                {
                    return CheckState.Unchecked;
                }
            }
            else
            {
                //���ǵ�һ����
                if ( Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID).ParentID!="")
                {
                    if (!HaveIN(str, ID))
                    {
                        return CheckState.Unchecked;
                    }
                    else
                    {
                        if (CheckChild(str, ID))
                        {
                            return CheckState.Checked;
                        }
                        else
                        {
                            return CheckState.Indeterminate;
                        }
                    }
                }
                //��һ����
                else
                {
                    if (CheckChild(str, ID))
                    {
                        return CheckState.Checked;
                    }
                    else
                    {
                        if (CheckChildFalse(str, ID))
                        {
                            return CheckState.Unchecked;
                        }
                        else
                        {
                            return CheckState.Indeterminate;
                        }
                       
                    }
                }


              
            }
           
        }
        //�жϺ����Ƿ񶼱�ѡ�У���ѡ�з���true,���򷵻�false
        private bool CheckChild(List<string> str, string ID)
        {
            IList<Ps_HistoryType> phtlist = Common.Services.BaseService.GetList<Ps_HistoryType>("SelectPs_HistoryTypeBYconn", "  Flag='" + FlagValue + "'  and ParentID='" + ID + "'");
            bool ChecK=true;
            for (int i = 0; i < phtlist.Count; i++)
            {
                ChecK = ChecK && HaveIN(str, phtlist[i].ID);
            }
            if (ChecK)
            {
                for (int i = 0; i < phtlist.Count; i++)
                {
                    ChecK = ChecK && CheckChild(str,phtlist[i].ID);
                }
            }
            return ChecK;
           
        }
        //�жϺ����Ƿ�δ��ѡ�У���δѡ�з���true,���򷵻�false
        private bool CheckChildFalse(List<string> str, string ID)
        {
            IList<Ps_HistoryType> phtlist = Common.Services.BaseService.GetList<Ps_HistoryType>("SelectPs_HistoryTypeBYconn", "  Flag='" + FlagValue + "'  and ParentID='" + ID + "'");
            bool ChecK = true;
            for (int i = 0; i < phtlist.Count; i++)
            {
                ChecK = ChecK && !HaveIN(str, phtlist[i].ID);
            }
            if (ChecK)
            {
                for (int i = 0; i < phtlist.Count; i++)
                {
                    ChecK = ChecK && CheckChildFalse(str, phtlist[i].ID);
                }
            }
            return ChecK;

        }
        //�ж��б����Ƿ����ID������Ϊtrue,����Ϊfalse
        private bool HaveIN(List<string> str, string ID)
        {
            bool have = false;
            for (int i = 0; i < str.Count; i++)
            {
                if (str[i].ToString() == ID.ToString() + "|" + ProjectID)
	            {
                    have = true;
                    break;
	            }
            }
            return have;

        }
        //����ID�ҵ�����㲢��Ϊ��ǰ���
        private void FindCurrentNodeById(TreeListNodes nodes, string ID)
        {
            foreach (TreeListNode node in nodes)
            {
                if (node["ID"].ToString() == ID)
                {
                    treeList1.FocusedNode = node;
                    break;
                }
                else
                {
                    if (node.Nodes.Count != 0)
                    {
                        FindCurrentNodeById(node.Nodes, ID);
                    }
                }
            }
        }
        ////ģ���б����ı�ʱ�޸Ķ�Ӧ��ֵ
        //private void combtype_EditValueChanged(object sender, EventArgs e)
        //{
        //    if (combtype.EditValue == str[0].ToString())
        //    {
        //        FlagValue = "1";

        //    }
        //    else if (combtype.EditValue == str[1].ToString())
        //    {
        //        FlagValue = "2";
        //    }
        //    else if (combtype.EditValue == str[2].ToString())
        //    {
        //        FlagValue = "3";
        //    }
        //    else if (combtype.EditValue == str[3].ToString())
        //    {
        //        FlagValue = "4";
        //    }
        //    else if (combtype.EditValue == str[4].ToString())
        //    {
        //        FlagValue = "5";
        //    }
        //    else if (combtype.EditValue == str[5].ToString())
        //    {
        //        FlagValue = "6";
        //    }
        //    else
        //    {
        //        FlagValue = "";
        //    }
        //    LoadData();
        //}
        //ģ���б����ı�ʱ�޸Ķ�Ӧ��ֵ
        private void combtype_EditValueChanged(object sender, EventArgs e)
        {
            
            if (combtype.EditValue == str[0].ToString())
            {
                FlagValue = "2";
            }
            else if (combtype.EditValue == str[1].ToString())
            {
                FlagValue = "3";
            }
            else if (combtype.EditValue == str[2].ToString())
            {
                FlagValue = "4";
            }
            else if (combtype.EditValue == str[3].ToString())
            {
                FlagValue = "5";
            }
            else if (combtype.EditValue == str[4].ToString())
            {
                FlagValue = "6";
            }
            else
            {
                FlagValue = "";
            }
            LoadData();
        }
        //������ʱ�¼�
        private void treeList1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                TreeListHitInfo hInfo = treeList1.CalcHitInfo(new Point(e.X, e.Y));
                if (hInfo.HitInfoType == HitInfoType.StateImage)
                {
                    SetCheckedNode(hInfo.Node);
                    CheckEnd = true;
                }
            }
        }
        //��ѡ����Ŀʱ�ı����״̬ͬʱ�丸�����ӽ���״̬
        private void SetCheckedNode(TreeListNode node)
        {
            CheckState check = (CheckState)node.GetValue("check");
            if (check == CheckState.Indeterminate || check == CheckState.Unchecked) check = CheckState.Checked;
            else check = CheckState.Unchecked;
            treeList1.BeginUpdate();
            node["check"] = check;
            //StatusBarDisplayText(treeList1.FocusedNode);
            SetCheckedChildNodes(node, check);
            SetCheckedParentNodes(node, check);
            treeList1.Refresh();
            treeList1.EndUpdate();
        }
        //�ı��ӽ��״̬
        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                ChangeUnits(node.Nodes[i]);
                node.Nodes[i]["check"] = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        //�ı丸���״̬
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    ChangeUnits(node.ParentNode.Nodes[i]);
                    if (!CheckState.Checked.Equals((CheckState)node.ParentNode.Nodes[i]["check"]))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode["check"] = b ?  CheckState.Indeterminate:check ;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        }
        //����check�е�ֵ���ı�ͼ���ֵ
        private void treeList1_GetStateImage(object sender, GetStateImageEventArgs e)
        {
            if (Flag=="")
            {
                return;
            }
            CheckState check = (CheckState)e.Node.GetValue("check");
            if (check == CheckState.Unchecked)
                e.NodeImageIndex = 0;
            else if (check == CheckState.Checked)
                e.NodeImageIndex = 1;
            else e.NodeImageIndex = 2;
           
        }
        //����һλ
        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.NextNode != null)
            {
                string ID1 = treeList1.FocusedNode.NextNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.NextNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 + 1;
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }

                    Ps_HistoryType pj = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID1);
                    pj.Sort = sortj;
                    Ps_HistoryType pj2 = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID2);
                    pj2.Sort = sortj2;

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph != null)
                    {
                        ph.Sort = pj.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = pj2.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }


                    Common.Services.BaseService.Update<Ps_HistoryType>(pj);
                    Common.Services.BaseService.Update<Ps_HistoryType>(pj2);
                    treeList1.FocusedNode.NextNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();
                }

            }
        }
        //����һλ
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                return;
            }

            TreeListNode node = treeList1.FocusedNode;
            int i = 0, sortj = 0, sortj2 = 0;

            if (treeList1.FocusedNode.PrevNode != null)
            {
                string ID1= treeList1.FocusedNode.PrevNode["ID"].ToString();
                string ID2 = treeList1.FocusedNode["ID"].ToString();
                if (ID1 != ID2)
                {
                    sortj = Convert.ToInt32(treeList1.FocusedNode.PrevNode["Sort"]);
                    sortj2 = Convert.ToInt32(treeList1.FocusedNode["Sort"]);
                    if (sortj2 == sortj)
                    {
                        sortj2 = sortj2 - 1;
                       
                    }
                    else
                    {
                        i = sortj;
                        sortj = sortj2;
                        sortj2 = i;
                    }
                    Ps_HistoryType pj = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID1);
                    pj.Sort = sortj;
                    Ps_HistoryType pj2 = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(ID2);
                    pj2.Sort = sortj2;

                    Ps_History ph = Common.Services.BaseService.GetOneByKey<Ps_History>(ID1);
                    Ps_History ph2 = Common.Services.BaseService.GetOneByKey<Ps_History>(ID2);
                    if (ph!=null)
                    {
                        ph.Sort = pj.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph);
                    }
                    if (ph2 != null)
                    {
                        ph2.Sort = pj2.Sort;
                        Common.Services.BaseService.Update<Ps_History>(ph2);
                    }

                    Common.Services.BaseService.Update<Ps_HistoryType>(pj);
                    Common.Services.BaseService.Update<Ps_HistoryType>(pj2);
                    treeList1.FocusedNode.PrevNode["Sort"] = sortj;
                    treeList1.FocusedNode["Sort"] = sortj2;
                    treeList1.BeginSort();
                    treeList1.EndSort();

                }

            }
            
        }
        //���һ�����
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            FormHistoryTypeEdit frm = new FormHistoryTypeEdit();
            frm.Text = "���ӷ���";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_HistoryType pf = new Ps_HistoryType();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Flag = FlagValue;
                pf.ParentID = "";
                pf.Sort = (int)Common.Services.BaseService.GetObject("SelectPs_HistoryTypeMaxsort", "") + 1;
                pf.TypeName = frm.TypeTitle;
                pf.Units = frm.Units;
                pf.Remark = frm.Remark;
                // SelectPs_HistoryTypeMaxsort
               
                try
                {
                    Common.Services.BaseService.Create<Ps_HistoryType>(pf);
                    OldId = pf.ID;
                    LoadData();
                }
                catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
            }
        }
        //�������
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                MessageBox.Show("����ѡ���ϼ����");
                return;
            }
            string ParentID = treeList1.FocusedNode["ID"].ToString();
            FormHistoryTypeEdit frm = new FormHistoryTypeEdit();
            frm.Text = "���ӷ���";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_HistoryType pf = new Ps_HistoryType();
                pf.ID = Guid.NewGuid().ToString() + "|" + ProjectUID;
                pf.Flag = FlagValue;
                pf.ParentID = ParentID;
                pf.Sort = (int)Common.Services.BaseService.GetObject("SelectPs_HistoryTypeMaxsort", "") + 1;
                pf.TypeName = frm.TypeTitle;
                pf.Units = frm.Units;
                pf.Remark = frm.Remark;
                // SelectPs_HistoryTypeMaxsort

                try
                {
                    Common.Services.BaseService.Create<Ps_HistoryType>(pf);
                    OldId = pf.ID;
                    LoadData();
                }
                catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
            }
        }
        //�޸����
        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null)
            {
                MessageBox.Show("����ѡ���㣡");
                return;
            }
            string ParentID = treeList1.FocusedNode["ID"].ToString();
            FormHistoryTypeEdit frm = new FormHistoryTypeEdit();
            frm.Text = "�޸ķ���";
            frm.TypeTitle=treeList1.FocusedNode["TypeName"].ToString();
            frm.Remark = treeList1.FocusedNode["Remark"].ToString();
            frm.Units = treeList1.FocusedNode["Units"].ToString();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Ps_HistoryType pf = new Ps_HistoryType();
                pf.ID = treeList1.FocusedNode["ID"].ToString();
                pf.Flag = FlagValue;
                pf.Sort = int.Parse(treeList1.FocusedNode["Sort"].ToString());
                pf.ParentID = treeList1.FocusedNode["ParentID"].ToString();
                pf.TypeName = frm.TypeTitle;
                pf.Remark = frm.Remark;
                pf.Units = frm.Units;
                // SelectPs_HistoryTypeMaxsort

                try
                {
                    Common.Services.BaseService.Update<Ps_HistoryType>(pf);
                    //�޸Ĺ���ʵ���е����Ƽ����������
                    IList<Ps_History> phlist = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", " ID like '" + pf.ID+"%'");
                    for (int i = 0; i < phlist.Count; i++)
                    {

                        if (phlist[i].Title.Contains("("))
                        {
                            phlist[i].Title = pf.TypeName + phlist[i].Title.Substring(phlist[i].Title.IndexOf("("), phlist[i].Title.Length - phlist[i].Title.IndexOf("("));
                        }
                        else
                        {
                            phlist[i].Title = pf.TypeName;
                        }

                        Common.Services.BaseService.Update<Ps_History>(phlist[i]);
                        
                    }
                   
                    treeList1.FocusedNode["TypeName"]=frm.TypeTitle;
                    treeList1.FocusedNode["Units"] = frm.Units;
                    treeList1.FocusedNode["Remark"]=frm.Remark;
                }
                catch (Exception ex) { MsgBox.Show("�޸ķ������" + ex.Message); }
            }
        }
        //ɾ�����
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode != null)
            {
                if (MessageBox.Show("ɾ�������ܶԵ�����չʵ������������ʵ��������Ȳ����ش�Ӱ�죡��\n\n                           ��ȷ��", "����", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return;
                }
                if (treeList1.FocusedNode.Nodes.Count != 0)
                {

                    if (MessageBox.Show(treeList1.FocusedNode["TypeName"].ToString() + "��������ӽ�㣬�Ƿ�һ��ɾ��?", "ѯ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DeleteNodes(treeList1.FocusedNode);
                    }
                }
                else
                {
                    if (MessageBox.Show("�Ƿ�ȷ��ɾ�����" + treeList1.FocusedNode["TypeName"].ToString() + " ?", "ѯ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        DeleteNodes(treeList1.FocusedNode);
                    }
                }
            }
        }
        //ɾ�����
        private void DeleteNodes(TreeListNode node)
        {
            if (node.HasChildren)
            {
                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    DeleteNodes(node.Nodes[i]);
                }
                DeleteNodes(node);
            }
            else
            {
                Ps_HistoryType pht = new Ps_HistoryType();
                pht.ID = node["ID"].ToString();
                Common.Services.BaseService.Delete<Ps_HistoryType>(pht);
                //ɾ������ʵ���еķ������Ƽ����������еĸ���
                  IList<Ps_History> phlist = Common.Services.BaseService.GetList<Ps_History>("SelectPs_HistoryBYconnstr", " ID like '" +pht.ID+"%'");
                  for (int i = 0; i < phlist.Count; i++)
                  {
                      Common.Services.BaseService.Delete<Ps_History>(phlist[i]);
                  }
                treeList1.DeleteNode(node);
            }

           
        }
        //��Ԫֵ�ı�
        private void treeList1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (Flag == "")
            {
                return;
            }
            if (e.Column.Caption=="check"&&e.Value.ToString()!=e.Node.GetValue("oldcheck").ToString())
            {
                treeList1.Refresh();
            }
        }
        //����ʱѡ�����ı��λ�����ı���ò�ͬ��ɫ��ʶ����
        private void treeList1_CustomDrawNodeCell(object sender, CustomDrawNodeCellEventArgs e)
        {
            if (Flag == "")
            {
                return;
            }
            Brush backBrush, foreBrush;
            if (e.Node.GetValue("oldcheck").ToString() != e.Node.GetValue("check").ToString())
            {
                //Color.Bisque
                backBrush = new LinearGradientBrush(e.Bounds, Color.LightYellow, Color.LightYellow, LinearGradientMode.Horizontal);
                foreBrush = new SolidBrush(Color.Black);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
            else if ((CheckState)e.Node.GetValue("check") != CheckState.Unchecked && e.Node.GetValue("nowunits").ToString() != e.Node.GetValue("oldunits").ToString())
            {
                backBrush = new LinearGradientBrush(e.Bounds, Color.LightSteelBlue, Color.LightSteelBlue, LinearGradientMode.Horizontal);
                foreBrush = new SolidBrush(Color.Black);
                e.Graphics.FillRectangle(backBrush, e.Bounds);
                e.Graphics.DrawString(e.CellText, e.Appearance.Font, foreBrush, e.Bounds, e.Appearance.GetStringFormat());
                e.Handled = true;
            }
            else
            {
                backBrush = new SolidBrush(Color.DarkBlue);
                foreBrush = new SolidBrush(Color.PeachPuff);

            }
        }
        //����ѡ�е�ģ�����
        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //���µ緢����ʵ���е����
            if (Flag=="1")
            {

              RefreshData_DLFZ();
               
            }
            //���·�������ʵ���е����
            if (Flag=="2")
            {
                RefreshData_FQGD();
            }
            //���·����õ�������
            if (Flag == "3")
            {
                RefreshData_FQYD();
            }
            if (Flag == "4")
            {
                RefreshData_JJ();
            }
            if (Flag == "5")
            {
                RefreshData_DL();
            }
            if (Flag == "6")
            {
                RefreshData_FH();
            }
        }
        //���µ緢����ʵ���е����
        private void RefreshData_DLFZ()
        {
            //�ı�treelist1�Ľ����㣬Ȼ���ٱ����������treelist1�Ż����ֵ
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //�������б�
            AddListID.Clear();
            AddList.Clear();
            //��ռ����б�
            ReduceListID.Clear();
            ReduceList.Clear();
            //��ոı䵥λ�б�
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("��δ���κ��޸ģ�����Ҫ����ģ�飡");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "��ȷ�����Ƿ�Ҫ���������ı䣿";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        //Ps_HistoryType pht = new Ps_HistoryType();
                        //pht.ID = AddListID[i];
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = Forecast;
                        pf.ForecastID = ForecastID;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormHistory.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormHistory.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
                        
                        FormHistory.Historyhome.RefreshChart();
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormHistory.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormHistory.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormHistory.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("�޸ĵ�λ����" + ex.Message); }

                    }
                }
               
                FormHistory.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("�������!");
            }
        }
        //���µ緢����ʵ���е����
        private void RefreshData_JJ()
        {
            //�ı�treelist1�Ľ����㣬Ȼ���ٱ����������treelist1�Ż����ֵ
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //�������б�
            AddListID.Clear();
            AddList.Clear();
            //��ռ����б�
            ReduceListID.Clear();
            ReduceList.Clear();
            //��ոı䵥λ�б�
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("��δ���κ��޸ģ�����Ҫ����ģ�飡");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "��ȷ�����Ƿ�Ҫ���������ı䣿";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        //Ps_HistoryType pht = new Ps_HistoryType();
                        //pht.ID = AddListID[i];
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = Forecast;
                        pf.ForecastID = ForecastID;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormHistoryJJ.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormHistoryJJ.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }

                        FormHistoryJJ.Historyhome.RefreshChart();
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormHistoryJJ.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormHistoryJJ.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormHistoryJJ.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("�޸ĵ�λ����" + ex.Message); }

                    }
                }
                
                FormHistoryJJ.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("�������!");
            }
        }
        //���µ緢����ʵ���е����
        private void RefreshData_DL()
        {
            //�ı�treelist1�Ľ����㣬Ȼ���ٱ����������treelist1�Ż����ֵ
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //�������б�
            AddListID.Clear();
            AddList.Clear();
            //��ռ����б�
            ReduceListID.Clear();
            ReduceList.Clear();
            //��ոı䵥λ�б�
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("��δ���κ��޸ģ�����Ҫ����ģ�飡");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "��ȷ�����Ƿ�Ҫ���������ı䣿";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        //Ps_HistoryType pht = new Ps_HistoryType();
                        //pht.ID = AddListID[i];
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = Forecast;
                        pf.ForecastID = ForecastID;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormHistoryDL.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormHistoryDL.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }

                        FormHistoryDL.Historyhome.RefreshChart();
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormHistoryDL.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormHistoryDL.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormHistoryDL.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("�޸ĵ�λ����" + ex.Message); }

                    }
                }

                FormHistoryDL.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("�������!");
            }
        }
        //���µ緢����ʵ���е����
        private void RefreshData_FH()
        {
            //�ı�treelist1�Ľ����㣬Ȼ���ٱ����������treelist1�Ż����ֵ
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //�������б�
            AddListID.Clear();
            AddList.Clear();
            //��ռ����б�
            ReduceListID.Clear();
            ReduceList.Clear();
            //��ոı䵥λ�б�
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("��δ���κ��޸ģ�����Ҫ����ģ�飡");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "��ȷ�����Ƿ�Ҫ���������ı䣿";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        //Ps_HistoryType pht = new Ps_HistoryType();
                        //pht.ID = AddListID[i];
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = Forecast;
                        pf.ForecastID = ForecastID;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormHistoryFH.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormHistoryFH.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }

                        FormHistoryFH.Historyhome.RefreshChart();
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormHistoryFH.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormHistoryFH.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormHistoryFH.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("�޸ĵ�λ����" + ex.Message); }

                    }
                }

                FormHistoryFH.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("�������!");
            }
        }

        //���·�������ʵ���е����
        private void RefreshData_FQGD()
        {
            //�ı�treelist1�Ľ����㣬Ȼ���ٱ����������treelist1�Ż����ֵ
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //�������б�
            AddListID.Clear();
            AddList.Clear();
            //��ռ����б�
            ReduceListID.Clear();
            ReduceList.Clear();
            //��ոı䵥λ�б�
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("��δ���κ��޸ģ�����Ҫ����ģ�飡");
                return;
            }

            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "��ȷ�����Ƿ�Ҫ���������ı䣿";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {
                        
                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = FormFqHistory.Historyhome.type;
                        pf.ForecastID = FormFqHistory.Historyhome.type1;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        string tempstr = string.Empty;
                        pf.Title = AddList[i].ToString();
                        pf.ParentID = pht.ParentID + "|" + ProjectID;
                        pf.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf.Col10 = "1";

                        pf.Sort = pht.Sort;

                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            FormFqHistory.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormFqHistory.Historyhome.dataTable.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }
                       
                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormFqHistory.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormFqHistory.Historyhome.DeleteNode(node);
                        }

                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormFqHistory.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }

                        }
                        catch (Exception ex) { MsgBox.Show("�޸ĵ�λ����" + ex.Message); }

                    }
                }
                FormFqHistory.Historyhome.treeList1.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("�������!");
            }
        }
        //���·����õ����
        private void RefreshData_FQYD()
        {
            //�ı�treelist1�Ľ����㣬Ȼ���ٱ����������treelist1�Ż����ֵ
            if (treeList1.FocusedNode != null)
            {
                TreeListNode node = treeList1.FocusedNode;
                treeList1.FocusedNode = null;
                treeList1.FocusedNode = node;
            }

            //�������б�
            AddListID.Clear();
            AddList.Clear();
            //��ռ����б�
            ReduceListID.Clear();
            ReduceList.Clear();
            //��ոı䵥λ�б�
            changeUnitlist.Clear();
            changeUnitlistID.Clear();
            AddorReduListBynode(treeList1.Nodes, AddList, ReduceList);
            if (AddList.Count == 0 && ReduceList.Count == 0 && changeUnitlist.Count == 0)
            {
                MsgBox.Show("��δ���κ��޸ģ�����Ҫ����ģ�飡");
                return;
            }
            FormHistoryTypeEditDeal frm = new FormHistoryTypeEditDeal();
            frm.TypeTitle = "��ȷ�����Ƿ�Ҫ���������ı䣿";
            frm.addlist = AddList;
            frm.reducelist = ReduceList;
            frm.changeUnitslist = changeUnitlist;
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (AddListID.Count != 0)
                {
                    for (int i = 0; i < AddListID.Count; i++)
                    {

                        Ps_HistoryType pht = Common.Services.BaseService.GetOneByKey<Ps_HistoryType>(AddListID[i]); ;
                        Ps_History pf = new Ps_History();
                        pf.ID = pht.ID + "|" + ProjectID;
                        pf.Forecast = FormFqHistoryDF.Historyhome.type;
                        pf.ForecastID = FormFqHistoryDF.Historyhome.type1;
                        TreeListNode node = treeList1.FindNodeByKeyID(pht.ID);
                        pf.Title = AddList[i].ToString();
                        if (node.Level==1)
                        {
                            pf.ParentID =  ProjectID;
                        }
                        else
                        {
                            pf.ParentID = pht.ParentID + "|" + ProjectID;
                        }
                        pf.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf.Col10 = "1";
                        pf.Sort = pht.Sort;

                        Ps_History pf2 = new Ps_History();
                        pf2.ID = pht.ID + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr;
                        pf2.Forecast = FormFqHistoryDF.Historyhome.type32;
                        pf2.ForecastID = FormFqHistoryDF.Historyhome.type31;
                        pf2.Title = AddList[i].ToString();
                       
                        if (pf2.Title.Contains("�õ���"))
                        {
                            pf2.Title = pf2.Title.Replace("�õ���", "����");
                        }
                        if (pf2.Title.Contains("����"))
                        {
                            pf2.Title=pf2.Title.Replace("����", "����");
                        }
                        if (pf.Title.Contains("����"))
                        {
                            pf.Title = pf.Title.Replace("����", "����");
                        }
                        

                        if (node.Level == 1)
                        {
                            pf2.ParentID = ProjectID + FormFqHistoryDF.Historyhome.splitstr;
                        }
                        else
                        {
                            pf2.ParentID = pht.ParentID + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr; ;
                        }
                        pf2.Col4 = MIS.ProgUID;
                        //��ʶ��Ĭ���������
                        pf2.Col10 = "1";
                        pf2.Sort = pht.Sort;
                        if (pf2.Title.Contains("ͬʱ��"))
                        {
                            pf2.Sort = 0;
                            pf.Sort = 1;
                            for (int j = 1990; j <= 2060; j++)
                            {
                                string y = "y" + j.ToString();
                                pf2.GetType().GetProperty(y).SetValue(pf2, 1.0, null);
                                pf.GetType().GetProperty(y).SetValue(pf, 1, null);
                            }
                        }
                        try
                        {
                            Common.Services.BaseService.Create<Ps_History>(pf);
                            Common.Services.BaseService.Create<Ps_History>(pf2);
                            FormFqHistoryDF.Historyhome.dataTable.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf, FormFqHistoryDF.Historyhome.dataTable.NewRow()));
                            FormFqHistoryDF.Historyhome.dataTable2.Rows.Add(Itop.Common.DataConverter.ObjectToRow(pf2, FormFqHistoryDF.Historyhome.dataTable2.NewRow()));

                        }
                        catch (Exception ex) { MsgBox.Show("���ӷ������" + ex.Message); }

                    }

                }
                if (ReduceListID.Count != 0)
                {
                    for (int i = 0; i < ReduceListID.Count; i++)
                    {
                        TreeListNode node = FormFqHistoryDF.Historyhome.treeList1.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID);
                        if (node != null)
                        {
                            FormFqHistoryDF.Historyhome.DeleteNode(node);
                        }
                        TreeListNode node2 = FormFqHistoryDF.Historyhome.treeList2.FindNodeByKeyID(ReduceListID[i].ToString() + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr);
                        if (node2 != null)
                        {
                            FormFqHistoryDF.Historyhome.DeleteNode(node2);
                        }
                    }
                }
                if (changeUnitlistID.Count != 0)
                {
                    for (int i = 0; i < changeUnitlistID.Count; i++)
                    {
                        try
                        {
                            Ps_History pht = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID);
                            pht.Title = changeUnitlist[i].ToString();
                            Common.Services.BaseService.Update<Ps_History>(pht);
                            TreeListNode node = FormFqHistoryDF.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID);
                            if (node != null)
                            {
                                node["Title"] = changeUnitlist[i].ToString();
                            }
                            Ps_History pht2 = Common.Services.BaseService.GetOneByKey<Ps_History>(changeUnitlistID[i] + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr);
                            pht2.Title = changeUnitlist[i].ToString();
                            if (pht2.Title.Contains("�õ���"))
                            {
                                pht2.Title = pht2.Title.Replace("�õ���", "����");
                            }
                            if (pht2.Title.Contains("����"))
                            {
                                pht2.Title = pht2.Title.Replace("����", "����");
                            }
                            Common.Services.BaseService.Update<Ps_History>(pht2);
                            TreeListNode node2 = FormFqHistoryDF.Historyhome.treeList1.FindNodeByKeyID(changeUnitlistID[i].ToString() + "|" + ProjectID + FormFqHistoryDF.Historyhome.splitstr);
                            if (node2 != null)
                            {
                                node2["Title"] = pht2.Title;
                            }
                        }
                        catch (Exception ex) { MsgBox.Show("�޸ĵ�λ����" + ex.Message); }

                    }
                }
                FormFqHistoryDF.Historyhome.treeList1.Refresh();
                FormFqHistoryDF.Historyhome.treeList2.Refresh();
                EqueValueBynode(treeList1.Nodes);
                treeList1.Refresh();
                MsgBox.Show("�������!");
            }
        }
        //��ѡ�����ı��ֵ��ѡ����
        private void AddorReduListBynode(TreeListNodes nodes, List<string> addlist,List<string> redulist)
        {
            if (nodes!= null)
            {
                foreach (TreeListNode node in nodes)
                {
                   
                    if (node.ParentNode!=null&&node.GetValue("check").ToString() != "0" && node.GetValue("oldcheck").ToString() == "0")
                    {

                        AddListID.Add(node.GetValue("ID").ToString());
                        if (node.GetValue("nowunits").ToString()!="")
                        {
                            addlist.Add(node.GetValue("TypeName").ToString() + "(" + node.GetValue("nowunits").ToString()+")");
                        }
                        else
	                    {
                            addlist.Add(node.GetValue("TypeName").ToString());
	                    }
                       
                    }
                    if (node.ParentNode != null && node.GetValue("check").ToString() == "0" && node.GetValue("oldcheck").ToString() != "0")
                    {
                        ReduceListID.Add(node.GetValue("ID").ToString());
                        if (node.GetValue("nowunits").ToString() != "")
                        {
                            redulist.Add(node.GetValue("TypeName").ToString() + "(" + node.GetValue("nowunits").ToString() + ")");
                        }
                         else
	                    {
                            redulist.Add(node.GetValue("TypeName").ToString());
	                    }
                    }
                    bool val1 = node.ParentNode != null && node.GetValue("check").ToString() != "0" && node.GetValue("oldcheck").ToString() == "0";
                    bool val2 = node.ParentNode != null && node.GetValue("check").ToString() == "0" && node.GetValue("oldcheck").ToString() != "0";
                    if (!val1 && !val2 && node.ParentNode != null && node.GetValue("nowunits").ToString() != node.GetValue("oldunits").ToString() && node.GetValue("check").ToString() != "0")
                    {
                        changeUnitlistID.Add(node.GetValue("ID").ToString());
                        if (node.GetValue("nowunits").ToString() != "")
                        {
                             changeUnitlist.Add(node.GetValue("TypeName").ToString() + "(" + node.GetValue("nowunits").ToString() + ")");
                        }
                        else
                        {
                            changeUnitlist.Add(node.GetValue("TypeName").ToString());
                        }
                    }
                    if (node.HasChildren)
                    {
                        AddorReduListBynode(node.Nodes, addlist, redulist);
                    }
                    
                    
                }
                
            }
        }
        //�������ɺ�check��oldcheck��Ϊ���ֵ���Ա��´θ���ʱ���
        private void EqueValueBynode(TreeListNodes nodes)
        {
            //if (nodes != null)
            //{
            //    foreach (TreeListNode node in nodes)
            //    {
            //        EqueVlaueDataTable(node.GetValue("ID").ToString(), (CheckState)node.GetValue("check"), (CheckState)node.GetValue("check"));
            //        if (node.HasChildren)
            //        {
            //            EqueValueBynode(node.Nodes);
            //        }
            //    }

            //}
            if (nodes != null)
            {
                for (int i = 0; i < nodes.Count; i++)
                {
                    TreeListNode node = nodes[i];
                    EqueVlaueDataTable(node.GetValue("ID").ToString(), (CheckState)node.GetValue("check"), (CheckState)node.GetValue("check"));
                    if (node.HasChildren)
                    {
                        EqueValueBynode(node.Nodes);
                    }
                }

            }
        }
        private void EqueVlaueDataTable( string ID, CheckState check,CheckState oldcheck)
        {
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i]["ID"].ToString()==ID)
                {
                    dataTable.Rows[i]["check"] = check;
                    dataTable.Rows[i]["oldcheck"] = oldcheck;
                    dataTable.Rows[i]["oldunits"] = dataTable.Rows[i]["nowunits"];
                }
            }
        }
        //�ر�
        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
        private void treeList1_AfterFocusNode(object sender, NodeEventArgs e)
        {
            if (Flag==""||e.Node==null)
            {
                return;
            }
            repositoryItemComboBox2.Items.Clear();
            List<string> str = new List<string>();
            if (e.Node.GetValue("Units").ToString()!="")
            {
                string tempstr = e.Node.GetValue("Units").ToString();
                str = ToList(str, tempstr);
                if (str.Count>1)
                {
                    for (int i = 0; i < str.Count; i++)
                    {
                        repositoryItemComboBox2.Items.Add(str[i]);
                    }
                    if (e.Node.GetValue("nowunits").ToString()=="")
                    {
                        e.Node["nowunits"] = str[0];
                    }
                    treeList1.Columns["nowunits"].OptionsColumn.AllowFocus = true;
                }
                else
                {
                    e.Node["nowunits"] = e.Node.GetValue("Units").ToString();
                    treeList1.Columns["nowunits"].OptionsColumn.AllowFocus = false;
                }
               
            }
            else
            {
                treeList1.Columns["nowunits"].OptionsColumn.AllowFocus = false;
            }
        }
        //��ѡ�н���ȡ��ѡ��ʱ�ı���ĵ�λ
        private void ChangeUnits(TreeListNode node)
        {
            if (node==null)
            {
                return;
            }
            repositoryItemComboBox2.Items.Clear();
            repositoryItemComboBox2.AllowFocused = false;
            List<string> str = new List<string>();
            if (node.GetValue("Units").ToString() != "")
            {
                string tempstr = node.GetValue("Units").ToString();
                str = ToList(str, tempstr);
                if (str.Count > 1)
                {
                    for (int i = 0; i < str.Count; i++)
                    {
                        repositoryItemComboBox2.Items.Add(str[i]);
                    }
                    if (node.GetValue("nowunits").ToString() == "")
                    {
                        node["nowunits"] = str[0];
                    }
                    repositoryItemComboBox2.AllowFocused = true;
                }
                else
                {
                    node["nowunits"] = node.GetValue("Units").ToString();
                }

            }
        }
        //����"#"�ѵ�λһ��һ����ȡ����
        private List<string> ToList(List<string> strlist,string tempstr)
        {
            if (!tempstr.Contains("#"))
            {
                strlist.Add(tempstr);
                return strlist;
            }
            else
            {
                string str = tempstr.Substring(0, tempstr.IndexOf("#"));
                strlist.Add(str);
                tempstr = tempstr.Substring(tempstr.IndexOf("#") + 1, tempstr.Length - tempstr.IndexOf("#") - 1);
                return ToList(strlist, tempstr);
            }
        }

       
       

       
       

    }
}