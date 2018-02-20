using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using BookmarkCore;
using System.Windows.Forms;

namespace BookmarkApplication
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            var userName = userTxt.Text;
            var password = passTxt.Text;

            BookmarkService service = new BookmarkService();
            if (service.CheckLogin(userName, password))
            {
                UserForm userForm = new UserForm(userName);
                userForm.MdiParent = this.ParentForm;
                userForm.Show();
                this.Hide();

            }
            else {
                this.Refresh();
                registerLabel.Text = "Login Failed...";
            }
        }

        private void signupLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Register register = new Register();
            register.MdiParent = this.ParentForm;
            this.Hide();
            register.Show();
        }
    }
}
