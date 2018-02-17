using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookmarkApplication
{
    public partial class VerifyOTP : Form
    {
        public VerifyOTP()
        {
            InitializeComponent();
        }

        private void okBtn_Click(object sender, EventArgs e)
        {
            var userOTP = otpTxt.Text;
            Register register = new Register();
            register.userOTP = userOTP;
            this.Close();
            register.Focus();
        }
    }
}
