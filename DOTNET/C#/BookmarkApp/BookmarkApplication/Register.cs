using BookmarkCore;
using System;
using System.Threading;
using System.Windows.Forms;

namespace BookmarkApplication
{
    public partial class Register : Form
    {
        private BookmarkService service = new BookmarkService();
        private string otp;
        private string userName;
        private string pass;
        private string email;
        private string location;

        public Register()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            userName = userNameTxt.Text;
            pass = passTxtBox.Text;
            email = emailTxtBox.Text;
            location = locationTxtBox.Text;
            otp = service.VerifyByEmail(email, userName);
            VerifyOTP verify = new VerifyOTP(this);
            verify.Show();
        }

        public void VerifyOTP(string userOTP)
        {
            if (userOTP.Equals(otp))
            {
                User user = new User(userName, pass, email, location);
                service.RegisterUser(user);
                detailLabel.Text = "You are register With us.Please go to login page and login" +
                    "with your username and password.";
                Thread.Sleep(2000);
                this.Show(Parent);
            }
            else
            {
                this.Refresh();
                detailLabel.Text = "Something went wrong.Again Register your self.";
            }
        }
    }
}
