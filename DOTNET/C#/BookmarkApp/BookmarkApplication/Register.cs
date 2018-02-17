using BookmarkCore;
using System;
using System.Windows.Forms;

namespace BookmarkApplication
{
    public partial class Register : Form
    {
        public Register()
        {
            InitializeComponent();
        }

        private void submitBtn_Click(object sender, EventArgs e)
        {
            var userName = userNameTxt.Text;
            var pass = passTxtBox.Text;
            var email = emailTxtBox.Text;
            var location = locationTxtBox.Text;
          
            BookmarkService service = new BookmarkService();
            string otp = service.VerifyByEmail(email,userName);

            VerifyOTP verify = new VerifyOTP();
            verify.Show();

            if (userOTP.Equals(otp)) {
                User user = new User(userName,pass,email,location);
                service.RegisterUser(user);
                detailLabel.Text = "You are register With us.Please go to login page and login"+
                    "with your username and password.";
                this.Close();
            }
            else
            {
                detailLabel.Text = "Something went wrong.Again Register your self.";
                this.Refresh();
            }
        }

        public string userOTP { get; set; }
    }
}
