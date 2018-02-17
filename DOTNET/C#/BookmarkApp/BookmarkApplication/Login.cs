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
                registerLabel.Text = "Login Success";
            }
            else {
                registerLabel.Text = "";
            }
        }

        private void registerLabel_Click(object sender, EventArgs e)
        {
            Register register = new Register();
            register.MdiParent = this.ParentForm;
            register.Show();
        }
    }
}
