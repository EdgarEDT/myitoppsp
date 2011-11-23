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
            //�жϾ������Ƿ���ȷ
            if (User.Password != txtOldPwd.Text)
            {
                MsgBox.Show("���������!");
                return;
            }
            //�ж��������ȷ�������Ƿ���ͬ
            if (txtNewPwd.Text != txtPwd.Text)
            {
                MsgBox.Show("�������ȷ�����벻һ��!");
                return;
            }
            User.Password = txtPwd.Text;
            UserService.Update(user);

            DialogResult = DialogResult.OK;
        }
    }
}