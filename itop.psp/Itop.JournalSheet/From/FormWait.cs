using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Client.Base;
namespace Itop.JournalSheet.From
{
    public partial class FormWait : FormBase
    {
        public int count = 0;
        private bool IsStat = false;//是否启动时钟。
        public FormWait()
        {
            InitializeComponent();
        }

        private void FormWait_Load(object sender, EventArgs e)
        {
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            prcBar.Maximum = 100;
            //prcBar.Minimum = 0;
            prcBar.Step = 1;
        }
        /// <summary>

        /// Increase process bar

        /// </summary>

        /// <param name="nValue">the value increased</param>

        /// <returns></returns>

        public bool Increase(int nValue)
        {

            if (nValue > 0)
            {

                if (prcBar.Value + nValue < prcBar.Maximum)
                {

                    prcBar.Value += nValue;
                    this.Text = "已经加载"+prcBar.Value.ToString()+"%";
                    return true;

                }

                else
                {

                    prcBar.Value = prcBar.Maximum;//进度条到头

                    this.Close();

                    return false;

                }

            }

            return false;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //if (prcBar.Value < prcBar.Maximum)
            //{
            //    prcBar.Value += this.prcBar.Step;
            //    count = this.prcBar.Value;
            //}
            //else
            //{
            //    this.prcBar.Value = 0;
            //    count = 0;
            //}
            if(IsStat)
            {
                count++;
            }
            else
            {
                count = 0;
            }
        }
        /// <summary>
        /// 时间状态
        /// </summary>
        public  void TimerMode(bool IsTrue)
        {
            if (IsTrue)
            {
                timer1.Start();
                timer1.Enabled = true;
                IsStat = true;
            }
            else
            {
                timer1.Stop();
                timer1.Enabled = false;
                IsStat = false;
            }
        }
        /// <summary>
        /// 工作完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnProcessCompleted(object sender,EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 工作中执行进度更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnProcessChanged(object sender, ProgressChangedEventArgs e)
        {
            this.prcBar.Value =  e.ProgressPercentage;
            this.Text = e.ProgressPercentage.ToString() + "%";
        }
    }
}