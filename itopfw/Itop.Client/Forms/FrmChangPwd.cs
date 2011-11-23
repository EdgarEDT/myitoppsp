using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Itop.Server.Interface;
using Itop.Common;
using Itop.Client;
using Itop.Domain.RightManager;
using Itop.Client.Base;

namespace Itop.Client.Forms
{
    public partial class FrmChangPwd : FormBase
    {
        public FrmChangPwd()
        {
            InitializeComponent();
        }
        private ISmmuserService userService;
        private ISmmuserService UserService
        {
            get {
                if (userService==null)
                    userService=  RemotingHelper.GetRemotingService<ISmmuserService>();

                return userService;
            }
        }
        Smmuser user ; 
        Smmuser User
        {
            get {
                if (user ==null)
                    user=UserService.GetOneByKey(MIS.UserNumber);
                return user; }
            }

        private void btClose_Click(object sender, EventArgs e)
        {
            
        }
       
        private void btOK_Click(object sender, EventArgs e)
        {
            //判断旧密码是否正确
            if (User.Password != txtOldPwd.Text)
            {
                MsgBox.Show("旧密码错误!");
                return;
            }
            //判断新密码和确认密码是否相同
            if (txtNewPwd.Text != txtPwd.Text)
            {
                MsgBox.Show("新密码和确认密码不一致!");
                return;
            }
            User.Password = txtPwd.Text;
            UserService.Update(user);

            DialogResult = DialogResult.OK;
        }
    }
}