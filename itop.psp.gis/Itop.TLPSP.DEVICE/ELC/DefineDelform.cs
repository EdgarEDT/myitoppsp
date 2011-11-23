using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Domain.Graphics;
using Itop.Client.Base;
using System.IO;
using System.Threading;
using System.Xml.XPath;
using Itop.Client.Common;
using System.Diagnostics;
namespace Itop.TLPSP.DEVICE
{
    public partial class DefineDelform : FormBase
    {
        PSPDEV pspulic = new PSPDEV();
        public List<int> linenums = new List<int>();           //����һ����·�ı��
        public List<int> transnums = new List<int>();          //�����ѹ����·�ı��
        public DefineDelform()
        {
            InitializeComponent();
        }
        public DefineDelform(PSPDEV pspDEV)           //����ͼ���SvgUID
        {
            InitializeComponent();
            pspulic = pspDEV;
            string strCon2 = null;
            string strCon1 = ",PSP_ELCDEVICE WHERE PSPDEV.SUID = PSP_ELCDEVICE.DeviceSUID AND PSP_ELCDEVICE.ProjectSUID = '" + pspulic.SvgUID + "'";
            strCon2 = " AND Type = '01'";
            string strCon = strCon1 + strCon2;
            IList listMX = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '05'";
            strCon = strCon1 + strCon2;
            IList listXL = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '02'";
            strCon = strCon1 + strCon2;
            IList listBYQ2 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
            strCon2 = " AND Type = '03'";
            strCon = strCon1 + strCon2;
            IList listBYQ3 = UCDeviceBase.DataService.GetList("SelectPSPDEVByCondition", strCon);
            
          int linecount=listXL.Count;
            for (int i = 0; i < listXL.Count; i++)
            {
                pspDEV = (PSPDEV)listXL[i];
                if (pspDEV.KSwitchStatus == "0")
                {

                   // psdevclass psd = new psdevclass(pspDEV.Number,pspDEV.FirstNode,pspDEV.LastNode, pspDEV.Name);

                    this.WaitLinelist.Items.Add(new psdevclass(i+1, pspDEV.FirstNode, pspDEV.LastNode,pspDEV.Type, pspDEV.Name));
                    
                }
            }
            int trans2count=listBYQ2.Count;
            for (int i = 0; i <listBYQ2.Count; i++)
            {
                pspDEV = (PSPDEV)listBYQ2[i];
                psdevclass psd = new psdevclass(linecount+i+1, pspDEV.FirstNode, pspDEV.LastNode,pspDEV.Type, pspDEV.Name);
                this.WaitLinelist.Items.Add(psd);
            }
            //psp.Lable = "ĸ�߽ڵ�";
            for (int i = 0; i < listBYQ3.Count; i++)
            {
                pspDEV = (PSPDEV)listBYQ3[i];
                psdevclass psdi = new psdevclass(linecount+trans2count+i+1, pspDEV.FirstNode, pspDEV.LastNode, pspDEV.Type, pspDEV.Name+"i");
                psdevclass psdj = new psdevclass(linecount + trans2count + i + 2, pspDEV.FirstNode, pspDEV.Flag, pspDEV.Type, pspDEV.Name + "j");
                psdevclass psdk = new psdevclass(linecount + trans2count + i + 3, pspDEV.LastNode, pspDEV.Flag, pspDEV.Type, pspDEV.Name + "k");
                this.WaitLinelist.Items.Add(psdi);
                this.WaitLinelist.Items.Add(psdj);
                this.WaitLinelist.Items.Add(psdk);
            }
            foreach (PSPDEV dev in listMX)
            {
                comboBoxEdit1.Properties.Items.Add(dev.Name);
                //comboBoxEdit2.Properties.Items.Add(dev.Name);
            }

        }
        private void DefineDelform_Load(object sender, EventArgs e)
        {

        }
        public string Defineproject //�г���������
        {
            get
            {
                return "a";
            }
            //set
            //{
            //    //textBox1.Text=value;
            //    //if (value==null)
            //    //{
            //    //    MessageBox.Show("�������г���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //}
            //}
        }
        public string Usename
        {
          get
          {
              return comboBoxEdit1.Text;
          }
            set
            {
                comboBoxEdit1.Text = value;
            }
        }
        private void NodeNumfind_Click(object sender, EventArgs e)
        {
            if (Nodepanel.Visible==false)
            {
                Nodepanel.Visible = true;
                LineNamepanel.Visible = false;
                return;
            }
            else
            {
                if (Usename=="")
                {
                    MessageBox.Show("��ѡ��ĸ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    WaitLinelist.ClearSelected();
                    //int firstnum = Convert.ToInt32(FirstNd.Text);
                    //int lastnum = Convert.ToInt32(LastNode.Text);
                    //pspulic.Type = "Polyline";
                    PSPDEV psp = new PSPDEV();
                    string con = " WHERE Name='" + Usename + "' AND ProjectID = '" + pspulic.ProjectID + "'" + "AND Type='01'";
                    psp = (PSPDEV)UCDeviceBase.DataService.GetObject("SelectPSPDEVByCondition", con);
                    if (psp==null)
                    {
                        MessageBox.Show("����ѡ��ĸ�߲����ڣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                    bool flag = false;
                    for (int i = 0; i < WaitLinelist.Items.Count; i++)
                    {
                        psdevclass psd = (psdevclass)WaitLinelist.Items[i];
                        if (psp.Number == psd.firstnum || psp.Number == psd.lastnum)
                        {

                            WaitLinelist.SelectedIndex = i;
                            flag = true;
                        }


                    }

                    if (!flag)
                    {
                        MessageBox.Show("û���ҵ���Ӧ����·��������������߳�������Ĳ�ѯ��ʽ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
               
            }
            
           
        }

        private void NodeName_Click(object sender, EventArgs e)
        {
            if ( LineNamepanel.Visible ==false)
            {
                Nodepanel.Visible = false;
                LineNamepanel.Visible = true;
            }
           else
            {
                if (LinenametextBox.Text == "")
                {
                    MessageBox.Show("��������·���ƣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else
                {
                    WaitLinelist.ClearSelected();
                    string linename = LinenametextBox.Text;
                    bool flag = false;
                    for (int i = 0; i < WaitLinelist.Items.Count; i++)
                    {
                        psdevclass psd = (psdevclass)WaitLinelist.Items[i];
                        if ( psd.linename.Contains(linename))
                        {

                            WaitLinelist.SelectedIndex = i;
                            flag = true;
                        }


                    }

                    if (!flag)
                    {
                        MessageBox.Show("û���ҵ���Ӧ����·��������������߳�������Ĳ�ѯ��ʽ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
            }
            
        }

        //private void LastNode_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    string str = this.LastNode.Text;
        //    e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
        //    if (e.KeyChar == (char)8)   //����������˼�
        //    {
        //        e.Handled = false;
        //    }
        //    if (e.KeyChar == (char)46)
        //    {
        //        if (str == "")   //��һ������������С����
        //        {
        //            e.Handled = true;
        //            return;
        //        }
        //        else
        //        { //С���㲻�������2��
        //            foreach (char ch in str)
        //            {
        //                if (char.IsPunctuation(ch))
        //                {
        //                    e.Handled = true;
        //                    return;
        //                }
        //            }
        //            e.Handled = false;
        //        }
        //    }
        //    if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        //}

        //private void FirstNd_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    string str = this.FirstNd.Text;
        //    e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //������������
        //    if (e.KeyChar == (char)8)   //����������˼�
        //    {
        //        e.Handled = false;
        //    }
        //    if (e.KeyChar == (char)46)
        //    {
        //        if (str == "")   //��һ������������С����
        //        {
        //            e.Handled = true;
        //            return;
        //        }
        //        else
        //        { //С���㲻�������2��
        //            foreach (char ch in str)
        //            {
        //                if (char.IsPunctuation(ch))
        //                {
        //                    e.Handled = true;
        //                    return;
        //                }
        //            }
        //            e.Handled = false;
        //        }
        //    }
        //    if (e.KeyChar == 45 && (((TextBox)sender).SelectionStart != 0 || ((TextBox)sender).Text.IndexOf("-") >= 0)) e.Handled = true;
        //}

        private void Addbutn_Click(object sender, EventArgs e)
        {
            if (this.WaitLinelist.SelectedItem==null)
            {
                MessageBox.Show("���ڴ�ѡ��·��ѡ��Ҫ�жϵ���·");
                return;

            }
            else
            {
              foreach (int i in this.WaitLinelist.SelectedIndices)
               {
                   bool flag = false;
                  //Ӧ���ڴ˴��ж�һ�´�ѡ��·���Ѿ���û������һ����· Ȼ����ѡ��
                   for (int j = 0; j < Selectlinelist.Items.Count;j++ )
                   {
                       if (WaitLinelist.Items[i].ToString() == Selectlinelist.Items[j].ToString())
                           flag = true;
                   }
                  if (!flag)
                  {
                      Selectlinelist.Items.Add(this.WaitLinelist.Items[i]);
                  }
                
               }
            }
            
            
        }

        private void Delbutn_Click(object sender, EventArgs e)
        {
           ListBox.SelectedIndexCollection sic = Selectlinelist.SelectedIndices;//�õ�ѡ���Item���±�

              if (sic.Count == 0)

                   return;

              else

              {

                   //  ��ѡ���Item����list��

                   List<int> list = new List<int>();

                   for (int i = 0; i < sic.Count; i++)

                   {

                       list.Add(sic[i]);

                   }

                   list.Sort();//��list�������򣨿���Ĭ�ϵ�������һ��ָ���Ǵ��µ��������

                   while(list.Count != 0)//�����±�Ӵ�С��˳���ListBox�ؼ���ɾ��ѡ���Item

                   //��������������˳��������ƻ��±����Ч��

                   {

                       Selectlinelist.Items.RemoveAt(list[list.Count - 1]);
                       list.RemoveAt(list.Count - 1);

                   }

              }

        }

        private void OKbutn_Click(object sender, EventArgs e)
        {
            if (Selectlinelist.Items.Count==0)
            {
                MessageBox.Show("��ѡ��Ҫ�г�����·��������Ĳ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < Selectlinelist.Items.Count;i++ )
            {
             psdevclass psd = (psdevclass)Selectlinelist.Items[i];
             if (psd.linetype == "05")
                {
                    linenums.Add(psd.line_num);         //����·�ı����·��·���б���
                }
                else if (psd.linetype == "02" || psd.linetype == "03")
                {
                    transnums.Add(psd.line_num);        //����ѹ����·�ı��д���ѹ����������
                }
                
            }
            linenums.Sort();
            transnums.Sort();
            this.DialogResult=DialogResult.OK;
        }

        private void NodeNumfind_DoubleClick(object sender, EventArgs e)
        {
           
        }

        private void comboBoxEdit1_Properties_KeyPress(object sender, KeyPressEventArgs e)
        {
          if (e.KeyChar == (char)32)   //����������˼�
          {
              e.Handled = false;
          }
        }

        private void LinenametextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)32)   //����������˼�
            {
                e.Handled = false;
            }
        }
        
        
    }
    class psdevclass
    {
        public int line_num;
        public int firstnum;
        public int lastnum;
        public string linetype;
        public string linename;
        public psdevclass(int _line_num,int _firstnum,int _lastnum,string _linetype,string _linename)
        {
            this.line_num = _line_num;
            this.firstnum = _firstnum;
            this.lastnum = _lastnum;
            this.linetype = _linetype;
            this.linename = _linename;
        }
        public override string ToString()
        {
            return linename;
        }   

    }
}