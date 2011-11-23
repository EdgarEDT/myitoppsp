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
        public List<int> linenums = new List<int>();           //保存一般线路的编号
        public List<int> transnums = new List<int>();          //保存变压器线路的编号
        public DefineDelform()
        {
            InitializeComponent();
        }
        public DefineDelform(PSPDEV pspDEV)           //出入图层的SvgUID
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
            //psp.Lable = "母线节点";
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
        public string Defineproject //切除方案名称
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
            //    //    MessageBox.Show("请输入切除方案名称", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("请选择母线名！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("你所选的母线不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("没有找到相应的线路，请重新输入或者尝试另外的查询方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    MessageBox.Show("请输入线路名称！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                        MessageBox.Show("没有找到相应的线路，请重新输入或者尝试另外的查询方式！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                
            }
            
        }

        //private void LastNode_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    string str = this.LastNode.Text;
        //    e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
        //    if (e.KeyChar == (char)8)   //允许输入回退键
        //    {
        //        e.Handled = false;
        //    }
        //    if (e.KeyChar == (char)46)
        //    {
        //        if (str == "")   //第一个不允许输入小数点
        //        {
        //            e.Handled = true;
        //            return;
        //        }
        //        else
        //        { //小数点不允许出现2次
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
        //    e.Handled = e.KeyChar < '0' || e.KeyChar > '9';   //允许输入数字
        //    if (e.KeyChar == (char)8)   //允许输入回退键
        //    {
        //        e.Handled = false;
        //    }
        //    if (e.KeyChar == (char)46)
        //    {
        //        if (str == "")   //第一个不允许输入小数点
        //        {
        //            e.Handled = true;
        //            return;
        //        }
        //        else
        //        { //小数点不允许出现2次
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
                MessageBox.Show("请在待选线路中选中要切断的线路");
                return;

            }
            else
            {
              foreach (int i in this.WaitLinelist.SelectedIndices)
               {
                   bool flag = false;
                  //应该在此处判断一下待选线路中已经有没有这样一条线路 然后再选择
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
           ListBox.SelectedIndexCollection sic = Selectlinelist.SelectedIndices;//得到选择的Item的下标

              if (sic.Count == 0)

                   return;

              else

              {

                   //  将选择的Item放入list中

                   List<int> list = new List<int>();

                   for (int i = 0; i < sic.Count; i++)

                   {

                       list.Add(sic[i]);

                   }

                   list.Sort();//对list进行排序（库里默认的排序结果一般指的是从下到大的排序）

                   while(list.Count != 0)//按照下标从大到小的顺序从ListBox控件里删除选择的Item

                   //如果这里采用其它顺序则可能破坏下标的有效性

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
                MessageBox.Show("请选择要切除的线路再做下面的操作", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            for (int i = 0; i < Selectlinelist.Items.Count;i++ )
            {
             psdevclass psd = (psdevclass)Selectlinelist.Items[i];
             if (psd.linetype == "05")
                {
                    linenums.Add(psd.line_num);         //将线路的编号线路线路的列表中
                }
                else if (psd.linetype == "02" || psd.linetype == "03")
                {
                    transnums.Add(psd.line_num);        //将变压器线路的编号写入变压器的容器中
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
          if (e.KeyChar == (char)32)   //允许输入回退键
          {
              e.Handled = false;
          }
        }

        private void LinenametextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)32)   //允许输入回退键
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