using StudentCore;
using System;
using System.Windows.Forms;

namespace StudentWindowsForm
{
    public partial class MainForm : Form
    {
        private string userName;
        private string password;

        private StudentForm studentForm = new StudentForm();
        public MainForm()
        {
            InitializeComponent();
        }

        private void studentFormToolStripMenuItem_Click(object sender, EventArgs e)
        {
            studentForm.MdiParent = this;
            studentForm.Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchForm searchForm = new SearchForm();
            searchForm.Text = "Welcome @" + userName;
            searchForm.MdiParent = this;
            searchForm.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.MdiParent = this;
            loginForm.Show();
        }
    }
}
