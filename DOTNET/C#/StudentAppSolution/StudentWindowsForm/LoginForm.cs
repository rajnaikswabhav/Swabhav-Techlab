using System;
using System.Collections.Generic;
using System.ComponentModel;
using StudentCore;
using System.Windows.Forms;

namespace StudentWindowsForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            string userName = userTextbox.Text;
            string password = passTextbox.Text;
            if (userName.Equals("") || password.Equals(""))
            {
                MessageBox.Show("Please Enter all details....");
            }
            else
            {
                LoginService loginService = new LoginService();
                bool isValidPerson = loginService.Login(userName, password);
                if (isValidPerson)
                {
                    this.Hide();
                    MainForm mainForm = new MainForm();
                   
                }
                else
                {
                    MessageBox.Show("Invalid User....");
                }
            }
        }
    }
}
