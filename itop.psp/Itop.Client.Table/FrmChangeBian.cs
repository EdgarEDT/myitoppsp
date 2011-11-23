using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Security.Policy;
using System.Collections;
using Itop.Domain.Table;
using Itop.Client.Base;
namespace Itop.Client.Table
{
    public partial class FrmChangeBian : FormBase
    {
        public FrmChangeBian()
        {
            InitializeComponent();
        }

        public Hashtable TextAttr
        {
            get {
                return textAttr;
            }
            set { textAttr = value; }
        }

        public string Title
        {
            set { textBox1.Text = value; }
            get { return textBox1.Text; }
        }

        public void SetEnable()
        {
            this.textBox1.ReadOnly = true;
        }

        public string SetFrmText
        {
            set { this.Text = value; }
        }
        private string projectid;
        public string GetProject
        {
            set { projectid = value; }
            get { return projectid; }
        }
        private string mark;
        public string Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        private Hashtable textAttr;
        private OperTable oper = new OperTable();
        //public Ps_YearRange yAnge = new Ps_YearRange();
      //  private Ps_YearRange yearRange = OperTable.GetYearRange();
        public void LoadTextBox()
        {
            Ps_YearRange yearRange = oper.GetYearRange("Col5='" + GetProject + "' and Col4='" + mark + "'");
            Point bt = new Point();
            for (int i = yearRange.StartYear; i <= yearRange.FinishYear; i++)
            {
                Point pt = GetLocation(i-yearRange.StartYear+1);
                Label label = new Label();
                label.Name = "x" + i.ToString();
                label.Text = i.ToString() + "年:";
                label.Location = pt;
                label.Size = new Size(50, 20);
                this.Controls.Add(label);
                TextBox box = new TextBox();
                box.Name = "y" + i.ToString();
                box.Text = textAttr.ContainsKey(box.Name) ? textAttr[box.Name].ToString() : "";
                box.Location = new Point(pt.X+50,pt.Y-5);
                box.Size = new Size(50, 20);
                box.TextChanged += new EventHandler(box_TextChanged);
                this.Controls.Add(box);
                bt = pt;
            }
            Button ok = new Button();
            ok.Name = "b1"; ok.Text = "确定";
            ok.DialogResult = DialogResult.OK;
            ok.Location = new Point(200, bt.Y + 40);
            ok.Click += new EventHandler(ok_Click);
            this.Controls.Add(ok);
            Button cancel = new Button();
            cancel.Name = "b2"; cancel.Text = "取消";
            cancel.DialogResult = DialogResult.Cancel;
            cancel.Location = new Point(300 + 30, bt.Y + 40);
            this.Controls.Add(cancel);
            this.Size = new Size(470,((yearRange.FinishYear - yearRange.StartYear) / 3 + 5) * 30);
        }

        void ok_Click(object sender, EventArgs e)
        {
            textAttr.Clear();
            for (int i = 0; i < Controls.Count; i++)
            {
                if (Controls[i].Name.StartsWith("y"))
                {
                    textAttr.Add(Controls[i].Name, Controls[i].Text);
                }
            }
        }

        public Point GetLocation(int c)
        {
            Point pt = new Point();
            int b = c % 3;
            if (b == 0) b = 3;
            int a = (c-1) / 3;
            pt.X = 20 + 150 * (b - 1);
            pt.Y = 30 * a +50;
            return pt;
        }
        private bool bFuHe = false;
        public bool BFuHe
        {
            set {
                bFuHe = value;
            }
        }

        void box_TextChanged(object sender, EventArgs e)
        {
            if (!IsNumber(sender.GetType().GetProperty("Text").GetValue(sender,null).ToString()))
            {
                MessageBox.Show("必须写入数字！");
                sender.GetType().GetProperty("Text").SetValue(sender, "", null);
                return;
            }
            if (!bFuHe)
            {
                string BoxName = sender.GetType().GetProperty("Name").GetValue(sender, null).ToString();
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i].Name.StartsWith("y") && int.Parse(Controls[i].Name.Substring(1)) > int.Parse(BoxName.Substring(1)))
                    {
                        ((TextBox)Controls[i]).Text = sender.GetType().GetProperty("Text").GetValue(sender, null).ToString();
                    }
                }
            }
        }

        public bool IsNumber(string text)
        {
            if (text == "")
                return true;
            double result=0.0;
            int   res=0;
            if (!double.TryParse(text,out result) && !int.TryParse(text,out res))
            {
                return false;
            }
            return true;
        }

        private void FrmChangeBian_Load(object sender, EventArgs e)
        {

            LoadTextBox();
        }
    }
}