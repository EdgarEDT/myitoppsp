using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Itop.Client.Base;
using Itop.Common;
using Itop.Domain;
using Itop.Server.Interface;
using Itop.Domain.RightManager;

namespace Itop.Client
{
    public partial class FrmServerLogin : FormBase
    {
        public FrmServerLogin()
        {
            InitializeComponent();
            FormView.Paint(this);
            utxtuser.image = imageList1.Images[0];
            utxtpwd.image = imageList1.Images[1];
        }
       
        //服务
        public ISmmuserService UserService
        {
            get
            {
               return Itop.Common.RemotingHelper.GetRemotingServiceSys<ISmmuserService>();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (utxtuser.tbox.Text == string.Empty)
            {
                MsgBox.Show("工号没有输入");
                utxtuser.Focus();
                return;
            }
            Smmuser user = new Smmuser();
            user.Userid = utxtuser.tbox.Text.Trim().ToLower();
            user.Password = utxtpwd.tbox.Text.Trim();
            IList<Smmuser> ulist;
            try
            {
                ulist = UserService.GetStrongList();
               
            }
            catch (Exception)
            {

                MessageBox.Show("无法连接到服务器！ 请点击配置确保服务器参数正确并确认服务器已启动");
                return;
            }

           
            bool result = false;
            foreach (Smmuser us in ulist)
            {
                if (us.Userid==user.Userid&&us.Password==user.Password)
                {
                    result = true;
                }
            }
            if (result)
            {
                if (user.Userid!="admin")
                {
                    MsgBox.Show("非管理员不能使用此项功能!");
                    return;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
                
            }
            else
            {
                MsgBox.Show("用户名或密码错误!");
            }
        }
    }
}